using Thylacine.Gateway;
using Thylacine.Helper;
using Thylacine.Event;
using Thylacine.Models;
using Thylacine.Models.Event;
using Thylacine.Rest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// The root namespace. The basis of everything.
/// </summary>
namespace Thylacine
{
    /// <summary>
    /// A Discord Bot instance. Use this to connect to discord and send messages.
    /// </summary>
    public class Discord
    {
        /// <summary>
        /// The current REST Client
        /// </summary>
        public IRestClient Rest { get; set; }

        /// <summary>
        /// The current Gateway Client
        /// </summary>
        public IGateway Gateway { get; set; }

        private Dictionary<ulong, Guild> _guilds = new Dictionary<ulong, Guild>();

        /// <summary>
        /// A list of all private channels the bot is subscribed to.
        /// </summary>
        /// <remarks>The DM Channels are not fully implemented yet and are subjected to change. They do not have the same functionalites as normal channels.</remarks>
        public List<DMChannel> PrivateChannels { get; private set; } = new List<DMChannel>();

        /// <summary>
        /// The current bot user
        /// </summary>
        public User User { get; private set; }

        private string token;

        #region Events
        public event MessageEvent OnMessageCreate;
        public event MessageEvent OnMessageUpdate;
        public event Event.MessageDeleteEvent OnMessageRemove;
        public event MessageBulkDeleteEvent OnMessageBulkRemove;

        public event DiscordReadyEvent OnDiscordReady;

        public event Event.GuildEvent OnGuildCreate;
        public event Event.GuildEvent OnGuildUpdate;
        public event Event.GuildEvent OnGuildRemove;
        public event Event.GuildEvent OnEmojisUpdate;
        public event Event.GuildEvent OnIntergrationsUpdate;
        public event Event.GuildEvent OnMembersChunk;

        public event DMCreateEvent OnDMCreate;
        public event DMCreateEvent OnDMRemove;

        //public event ChannelEvent OnChannelCreate;
        //public event ChannelEvent OnChannelUpdate;
        //public event ChannelEvent OnChannelRemove;

		//Moved to GUILD
        //public event GuildMemberEvent OnMemberCreate;
        //public event GuildMemberEvent OnMemberUpdate;
        //public event GuildMemberEvent OnMemberRemove;

        public event GuildBanEvent OnMemberBanned;
        public event GuildBanEvent OnMemberUnbanned;

        public event RoleEvent OnRoleCreate;
        public event RoleEvent OnRoleUpdate;
        public event RoleEvent OnRoleRemove;

        public event UserEvent OnUserUpdate;

        public event PresenceEvent OnPresenceUpdate;
        public event TypingEvent OnTyping;
        #endregion

        /// <summary>
        /// The current session of the bot
        /// </summary>
        public string Session { get; private set; }

        /// <summary>
        /// Creates a new bot instance with login.
        /// </summary>
        /// <param name="token">The token supplied by Discord for your target bot</param>
        public Discord(string token)
        {
            this.token = token;
        }

        /// <summary>
        /// Connects the bot to Discord.
        /// </summary>
        public async void Connect()
        {
            this.Rest = new RestSharpClient(token);
            GatewayEndpoint endpoint = await this.Rest.SendPayload<GatewayEndpoint>(new Rest.Payloads.GatewayRequest());

            this.Gateway = new GatewaySocket(endpoint, token);
            this.Gateway.OnDispatchEvent += OnDispatchEvent;
            this.Gateway.Connect();
        }

        #region Handlers
        private void HandleReadyEvent(ReadyEvent e)
        {
            //Set our user
            User = e.User;

            //Set our session
            Session = e.Session;

            //Log Relationships because wtf are they?
            //Console.WriteLine(((JToken)e.Relationships).ToString());

            //Add the guilds
            _guilds.Clear();
            foreach (Guild g in e.Guilds)
            {
                g.Discord = this;
                _guilds.Add(g.ID, g);
            }

            //Set the private channels
            PrivateChannels = new List<DMChannel>(e.PrivateChannels);
            
            //Done
            OnDiscordReady?.Invoke(this, new DiscordReadyEventArgs(User, Session));
        }

        private void HandleGuildCreateEvent(Guild guild)
        {
            //Add the guild
            guild.Discord = this;
            _guilds[guild.ID] = guild;
            _guilds[guild.ID].SyncronizePresence();

            OnGuildCreate?.Invoke(this, new GuildEventArgs(_guilds[guild.ID]));
        }
        private void HandleGuildUpdateEvent(Guild guild)
        {
            _guilds[guild.ID].UpdateGuild(guild, this);
            OnGuildUpdate?.Invoke(this, new GuildEventArgs(_guilds[guild.ID]));
        }
        private void HandleGuildDeleteEvent(UnavailableGuild guild)
        {
            Guild g = _guilds[guild.ID];
            _guilds.Remove(guild.ID);
            OnGuildRemove?.Invoke(this, new GuildEventArgs(g));

            //Console.WriteLine("Finished Guild Removal");
        }

        private void HandleGuildMemberAddEvent(GuildMemeberAdd member)
        {
            Guild g = _guilds[member.GuildID];
			g.UpdateMember(member);			
        }
        private void HandleGuildMemberUpdateEvent(GuildMemeberUpdate e)
        {
            Guild g = _guilds[e.GuildID];

            GuildMember gm = g.GetMember(e.User.ID);
            g.UpdateMember(gm, e,Roles, e.Nickname, e.User);            
        }
        private void HandleGuildMemberRemoveEvent(GuildMemberRemove e)
        {
            Guild g = _guilds[e.GuildID];
            g.RemoveMember(e.User.ID);
            //Console.WriteLine("Finished removing memeber");
        }
        private void HandleGuildMemberChunkEvent(GuildMembersChunk c)
        {
            Guild g = GetGuild(c.GuildID);
            g.GuildMembers = c.Members;
            OnMembersChunk?.Invoke(this, new GuildEventArgs(g));
        }

        private void HandleGuildRoleUCEvent(GuildRoleEvent e, bool isCreate)
        {
            Guild g = _guilds[e.GuildID];
            g.UpdateRole(e.Role);

            if (isCreate)
                OnRoleCreate?.Invoke(this, new RoleEventArgs(g, e.Role));
            else
                OnRoleUpdate?.Invoke(this, new RoleEventArgs(g, e.Role));

            //Console.WriteLine("Finished updating/creating role");
        }
        
        private void HandleGuildRoleDeleteEvent(GuildRoleDelete e)
        {
            Role r = _guilds[e.GuildID].GetRole(e.Role);
            _guilds[e.GuildID].RemoveRole(e.Role);
            OnRoleRemove?.Invoke(this, new RoleEventArgs(_guilds[e.GuildID], r));

            //Console.WriteLine("Finished Role Deletion");
        }
        private void HandlePresenceUpdateEvent(Presence p)
        {
            Guild g = null;

            if (p.GuildID.HasValue)
            {
                g = _guilds[p.GuildID.Value];
            }
            else
            {
                foreach (Guild guild in _guilds.Values)
                {
                    if (guild.HasMember(p.Updates.ID))
                    {
                        g = guild;
                        break;
                    }
                }

                if (g == null)
                {
                    Console.WriteLine("Cannot update presence! Null guild!");
                    return;
                }
            }


            g.UpdateMemberPresence(p);
            OnPresenceUpdate?.Invoke(this, new PresenceEventArgs(g, g.GetMember(p.Updates.ID), p));
        }

        private void HandleChannelCreateEvent(Channel c)
        {
            //Prepare the guild it belongs to
            Guild g = _guilds[c.GuildID];

            //Update the channels guild
            c.Guild = g;

            //Update the guilds channel
            g.UpdateChannel(c);
        }
        private void HandleChannelUpdateEvent(Channel c)
        {
            //Prepare the guild it belongs to
            Guild g = _guilds[c.GuildID];
            c.Guild = g;

            g.UpdateChannel(c);
        }
        private void HandleDMDeleteEvent(DMChannel c)
        {
            var dm = PrivateChannels.Where(d => d.ID == c.ID).First();
            PrivateChannels.Remove(dm);

            OnDMRemove?.Invoke(this, new DMCreateEventArgs(dm));
        }
        private void HandleChannelDeleteEvent(Channel c)
        {
            Guild g = _guilds[c.GuildID];
            g.RemoveChannel(c);
        }
       
        private void HandleEmojiUpdate(GuildEmojiUpdate e)
        {
            Guild g = GetGuild(e.GuildID);
            g.Emojis = e.Emojis;
            OnEmojisUpdate?.Invoke(this, new GuildEventArgs(g));
        }
        
        private void HandleUserUpdate(User user)
        {
            foreach (Guild g in _guilds.Values)
                g.UpdateUser(user);

            OnUserUpdate?.Invoke(this, new UserEventArgs(user));
        }
        #endregion

        private void OnDispatchEvent(object sender, DispatchEventArgs args)
        {
            //Console.WriteLine("Event: {0}", args.Type);
            switch (args.Type)
            {                
                case "READY":
                    HandleReadyEvent(args.Payload.ToObject<ReadyEvent>());
                    break;

                case "RESUMED":
                    Console.WriteLine("Resumed Connection");
                    break;

                #region Channel
                case "CHANNEL_CREATE":
                    {
                        if (args.Payload["recipient"] != null)
                        {
                            DMChannel dm = args.Payload.ToObject<DMChannel>();
                            PrivateChannels.Add(dm);

                            OnDMCreate?.Invoke(this, new DMCreateEventArgs(dm));
                        }
                        else
                        {
                            HandleChannelCreateEvent(args.Payload.ToObject<Channel>());
                        }
                    }
                    break;


                case "CHANNEL_UPDATE":
                    {
                        HandleChannelUpdateEvent(args.Payload.ToObject<Channel>());
                    }
                    break;

                case "CHANNEL_DELETE":
                    {
                        if (args.Payload["recipient"] != null)
                        {
                            HandleDMDeleteEvent(args.Payload.ToObject<DMChannel>());
                        }
                        else
                        {
                            HandleChannelDeleteEvent(args.Payload.ToObject<Channel>());
                        }
                    }
                    break;
                #endregion
                
                #region Guild Events
                case "GUILD_UPDATE":
                    HandleGuildUpdateEvent(args.Payload.ToObject<Guild>());
                    break;
                case "GUILD_CREATE":
                    HandleGuildCreateEvent(args.Payload.ToObject<Guild>());
                    break;

                case "GUILD_DELETE":
                    HandleGuildDeleteEvent(args.Payload.ToObject<UnavailableGuild>());
                    break;

                case "GUILD_BAN_ADD":
                    {
                        User u = args.Payload.ToObject<User>();
                        Guild g = GetGuild(u.GuildID.Value);
                        OnMemberBanned?.Invoke(this, new GuildBanEventArgs(g, u));
                        
                        //TODO: Handle Guild Ban Add better
                        Console.WriteLine("TODO: Handle Guild Ban Add better");
                    }
                    break;

                case "GUILD_BAN_REMOVE":
                    {
                        User u = args.Payload.ToObject<User>();
                        Guild g = GetGuild(u.GuildID.Value);
                        OnMemberUnbanned?.Invoke(this, new GuildBanEventArgs(g, u));

                        //TODO: Handle Guild Ban Add better
                        Console.WriteLine("TODO: Handle Guild Ban Remove better");
                    }
                    break;

                case "GUILD_EMOJIS_UPDATE":
                    HandleEmojiUpdate(args.Payload.ToObject<GuildEmojiUpdate>());                    
                    break;

                case "GUILD_INTEGRATIONS_UPDATE":
                    {
                        Guild g = GetGuild(args.Payload.ToObject<Models.Event.GuildEvent>().GuildID);
                        OnIntergrationsUpdate?.Invoke(this, new GuildEventArgs(g));
                    }
                    break;

                case "GUILD_MEMBER_ADD":
                    HandleGuildMemberAddEvent(args.Payload.ToObject<GuildMemeberAdd>());
                    break;

                case "GUILD_MEMBER_REMOVE":
                    HandleGuildMemberRemoveEvent(args.Payload.ToObject<GuildMemberRemove>());
                    break;

                case "GUILD_MEMBER_UPDATE":
                    HandleGuildMemberUpdateEvent(args.Payload.ToObject<GuildMemeberUpdate>());
                    break;

                case "GUILD_MEMBERS_CHUNK":
                    HandleGuildMemberChunkEvent(args.Payload.ToObject<GuildMembersChunk>());
                    break;

                case "GUILD_ROLE_UPDATE":
                    HandleGuildRoleUCEvent(args.Payload.ToObject<GuildRoleEvent>(), false);
                    break;

                case "GUILD_ROLE_CREATE":
                    HandleGuildRoleUCEvent(args.Payload.ToObject<GuildRoleEvent>(), true);
                    break;

                case "GUILD_ROLE_DELETE":
                    HandleGuildRoleDeleteEvent(args.Payload.ToObject<GuildRoleDelete>());
                    break;
                #endregion

                #region Message
                case "MESSAGE_CREATE":
                    {
                        Message msg = args.Payload.ToObject<Message>();
                        msg.Discord = this;

                        Guild guild = GetChannelGuild(msg.ChannelID);
                        OnMessageCreate?.Invoke(this, new MessageEventArgs(msg, guild));
                    }
                    break;
                case "MESSAGE_UPDATE":
                    {
                        Message msg = args.Payload.ToObject<Message>();
                        msg.Discord = this;
                        
                        Guild guild = GetChannelGuild(msg.ChannelID);
                        OnMessageUpdate?.Invoke(this, new MessageEventArgs(msg, guild));
                    }
                    break;

                case "MESSAGE_DELETE":
                    {
                        var delete = args.Payload.ToObject<Models.Event.MessageDeleteEvent>();
                        Guild guild = GetChannelGuild(delete.ChannelID);
                        OnMessageRemove?.Invoke(this, new MessageDeleteEventArgs(delete.ID, delete.ChannelID, guild));
                    }
                    break;

                case "MESSAGE_DELETE_BULK":
                    {
                        MessageDeleteBulk bulk = args.Payload.ToObject<MessageDeleteBulk>();
                        Guild guild = GetChannelGuild(bulk.ChannelID);
                        OnMessageBulkRemove?.Invoke(this, new MessageBulkDeleteEventArgs(bulk.IDs, bulk.ChannelID, guild));
                    }
                    break;
                #endregion

                #region Presence
                case "PRESENCE_UPDATE":
                    HandlePresenceUpdateEvent(args.Payload.ToObject<Presence>());
                    break;

                case "TYPING_START":
                    {
                        TypingStartEvent typing = args.Payload.ToObject<TypingStartEvent>();

                        Channel c = GetChannel(typing.ChannelID);
                        Guild g = c.Guild;
                        User u = g.GetMember(typing.UserID).User;

                        OnTyping?.Invoke(this, new TypingEventArgs(g, c, u, typing.Timestamp));
                    }
                    break;
                #endregion

                #region User Update
                case "USER_SETTINGS_UPDATE":
                    {
                        //TODO: User Settings Update
                        Console.WriteLine("User Settings have updated. Do not know structure of settings!");
                        Console.WriteLine(args.Payload.ToString());
                    }
                    break;

                case "USER_UPDATE":
                    HandleUserUpdate(args.Payload.ToObject<User>());
                    break;
                #endregion

                #region Voice Updates
                case "VOICE_STATE_UPDATE":
                    {
                        VoiceState state = args.Payload.ToObject<VoiceState>();
                        //TODO: Handle VOICE_STATE_UPDATE Event
                    }
                    break;

                case "VOICE_SERVER_UPDATE":
                    {
                        VoiceServerUpdateEvent vsue = args.Payload.ToObject<VoiceServerUpdateEvent>();
                        //TODO: Handle VOICE_SERVER_UPDATE Event
                    }
                    break;
                #endregion

                default:
                    Console.WriteLine("Unhandled Event: " + args.Type);
                    break;
            }
        }

        #region Guilds
        /// <summary>
        /// Gets a Guild the bot is apart of
        /// </summary>
        /// <param name="guildID">The ID of the guild you are trying to get.</param>
        /// <returns></returns>
        public Guild GetGuild(ulong guildID)
        {
            Guild g;
            if (!_guilds.TryGetValue(guildID, out g)) return null;
            return g;
        }

        /// <summary>
        /// Gets the guilds the bot is apart of
        /// </summary>
        /// <returns></returns>
        public Guild[] GetGuilds() { return _guilds.Values.ToArray(); }

        /// <summary>
        /// Gets the guild a channel belongs to
        /// </summary>
        /// <param name="channelID">The ID of the channel you are looking for.</param>
        /// <returns></returns>
        public Guild GetChannelGuild(ulong channelID)
        {
            foreach(Guild g in _guilds.Values)
            {
                if (channelID == g.ID) return g;
                if (g.HasChannel(channelID)) return g;
            }

            return null;
        }

        /// <summary>
        /// Gets a list of guilds a user belongs to
        /// </summary>
        /// <param name="userID">The ID of the user you are looking for.</param>
        /// <returns></returns>
        public List<Guild> GetUserGuilds(ulong userID)
        {
            List<Guild> guilds = new List<Guild>();
            foreach (Guild g in _guilds.Values)
            {
                if (g.HasMember(userID)) guilds.Add(g);
            }

            return guilds;
        }
        #endregion

        #region channels
        /// <summary>
        /// Gets a channel object. Scans through all the guilds.
        /// </summary>
        /// <param name="channelID">The channel ID you are looking for.</param>
        /// <returns></returns>
        public Channel GetChannel(ulong channelID)
        {
            foreach (Guild g in _guilds.Values)
            {
                if (g.HasChannel(channelID))
                    return g.GetChannel(channelID);
            }

            return null;
        }
        #endregion

        #region Webhooks
        /// <summary>
        ///  Returns the new webhook object for the given id.
        /// </summary>
        /// <param name="webhookID">The webhook ID you are looking for.</param>
        /// <returns></returns>
        public async Task<Webhook> FetchWebhook(ulong webhookID) { return await FetchWebhook(webhookID, null);  }

        /// <summary>
        ///  Returns the new webhook object for the given id and token.
        /// </summary>
        /// <param name="webhookID">The webhook ID you are looking for.</param>
        /// <param name="token">The token of the webhook.</param>
        /// <returns></returns>
        public async Task<Webhook> FetchWebhook(ulong webhookID, string token)
        {
            Webhook hook = await Rest.SendPayload<Webhook>(new Rest.Payloads.GetWebhook() { WebhookID = webhookID, Token = token });
            hook.Discord = this;
            return hook;
        }

        #endregion

        #region Users
        /// <summary>
        /// Fetches a user of given ID
        /// </summary>
        /// <param name="userID">The user ID</param>
        /// <returns>A new user object</returns>
        public Task<User> FetchUser(ulong userID)
        {
            return Rest.SendPayload<User>(new Rest.Payloads.GetUser() { UserID = userID });
        }

        /// <summary>
        /// Modifies the current account settings of the bot.
        /// </summary>
        /// <param name="username">Username to set the bot too</param>
        public void ModifyUser(string username) { ModifyUser(username, null); }

        /// <summary>
        /// Modifies the current account settings of the bot.
        /// </summary>
        /// <param name="avatar">Avatar to set the bot too.</param>
        public void ModifyUser(Avatar avatar) { ModifyUser(null, avatar); }

        /// <summary>
        /// Modifies the current account settings of the bot.
        /// </summary>
        /// <param name="username">Username to set the bot too</param>
        /// <param name="avatar">Avatar to set the bot too.</param>
        public void ModifyUser(string username, Avatar avatar)
        {
            Rest.SendPayload(new Rest.Payloads.ModifyUser() { UserID = User.ID, Name = username, Avatar = avatar });
        }
       
		public List<User> GetUsers()
		{
			List<User> users = new List<User>();
			foreach (Guild g in _guilds.Values)
				users.AddRange(g.GetMembers().Select(gm => gm.Value.User));

			return users;
		}
		#endregion
    }
}
