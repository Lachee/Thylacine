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
using System.Collections;

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


        public event UserEvent OnUserUpdate;
		public event UserEvent OnUserCreate;

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
            _guilds[guild.ID] = guild;
			_guilds[guild.ID].Initialize(this);

			//Highjack all its users
			foreach (GuildMember g in guild.GetMembers().Values)
				AddUser(g.User);

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
			//Get the guild
            Guild g = _guilds[member.GuildID];

			//Highjack the user and add it to our list for our own evil purposes.
			AddUser(member.User);

			//Add the member
			g.AddMember(member);			
        }
        private void HandleGuildMemberUpdateEvent(GuildMemeberUpdate e)
        {
			//Get the guild and update them
            Guild g = _guilds[e.GuildID];
			g.UpdateMember(e.User, e.Roles, e.Nickname);     
        }
        private void HandleGuildMemberRemoveEvent(GuildMemberRemove e)
        {
            Guild g = _guilds[e.GuildID];
			g.RemoveMember(e.User.ID);
        }
        private void HandleGuildMemberChunkEvent(GuildMembersChunk c)
        {
			throw new NotImplementedException();
            //Guild g = GetGuild(c.GuildID);
            //g.GuildMembers = c.Members;
            //OnMembersChunk?.Invoke(this, new GuildEventArgs(g));
        }

        private void HandleGuildRoleUCEvent(GuildRoleEvent e, bool isCreate)
        {
            Guild g = _guilds[e.GuildID];
            g.UpdateRole(e.Role);

           // if (isCreate)
           //    OnRoleCreate?.Invoke(this, new RoleEventArgs(g, e.Role));
            //else
           //     OnRoleUpdate?.Invoke(this, new RoleEventArgs(g, e.Role));

            //Console.WriteLine("Finished updating/creating role");
        }
        
        private void HandleGuildRoleDeleteEvent(GuildRoleDelete e)
        {
            Role r = _guilds[e.GuildID].GetRole(e.Role);
            _guilds[e.GuildID].RemoveRole(e.Role);
           // OnRoleRemove?.Invoke(this, new RoleEventArgs(_guilds[e.GuildID], r));

            //Console.WriteLine("Finished Role Deletion");
        }
        private void HandlePresenceUpdateEvent(Presence p)
        {

			//Update the user
			UpdateUserPresence(p);

			//Update the presence of the guild user
			Guild g = _guilds[p.GuildID];
			g.UpdatePresence(p);

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
						state.AssociateGuild(this);
						state.Guild.UpdateVoiceState(state);
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

		private Dictionary<ulong, User> _users = new Dictionary<ulong, User>();

		#region Sets / Events

		private void HandleUserUpdate(User user)
		{
			_users[user.ID] = user;
			OnUserUpdate?.Invoke(this, new UserEventArgs(user));
		}

		private void UpdateUserPresence(Presence presence)
		{
			if (_users.ContainsKey(presence.User.ID))
			{
				_users[presence.User.ID].UpdatePresence(presence);
				OnUserUpdate?.Invoke(this, new UserEventArgs(_users[presence.User.ID]));
			}

		}
		private void AddUser(User user)
		{
			//User already exists in the list
			if (_users.ContainsKey(user.ID))
				return;

			//Create the user
			_users.Add(user.ID, user);
			OnUserCreate?.Invoke(this, new UserEventArgs(user));
		}

		private Task<User> FetchUser(ulong userID)
        {
            return Rest.SendPayload<User>(new Rest.Payloads.GetUser() { UserID = userID });
        }
		#endregion

		#region Gets
		/// <summary>
		/// Gets a User the bot can see.
		/// </summary>
		/// <param name="id">The ID of the User</param>
		/// <returns></returns>
		public User GetUser(ulong id)
		{
			User u;
			if (_users.TryGetValue(id, out u)) return u;
			return null;
		}

		/// <summary>
		/// Gets a list of users the bot can see.
		/// </summary>
		/// <returns></returns>
		public User[] GetUsers()
		{
			return _users.Values.ToArray();
		}

		/// <summary>
		/// Gets an enumerator of users the bot can see
		/// </summary>
		/// <returns></returns>
		public Dictionary<ulong, User>.Enumerator GetUsersEnumerator()
		{
			return _users.GetEnumerator();
		}
		#endregion

		#endregion

		#region Bot User

		/// <summary>
		/// The current bot user
		/// </summary>
		public User User { get; private set; }

		/// <summary>
		/// Modifies the current account settings of the bot.
		/// </summary>
		/// <param name="username">Username to set the bot too</param>
		public void ModifyBotUser(string username) { ModifyBotUser(username, null); }

		/// <summary>
		/// Modifies the current account settings of the bot.
		/// </summary>
		/// <param name="avatar">Avatar to set the bot too.</param>
		public void ModifyBotUser(Avatar avatar) { ModifyBotUser(null, avatar); }

		/// <summary>
		/// Modifies the current account settings of the bot.
		/// </summary>
		/// <param name="username">Username to set the bot too</param>
		/// <param name="avatar">Avatar to set the bot too.</param>
		public void ModifyBotUser(string username, Avatar avatar)
		{
			Rest.SendPayload(new Rest.Payloads.ModifyUser() { UserID = User.ID, Name = username, Avatar = avatar });
		}

		/// <summary>
		/// Gets a link that is used to invite the current bot to a guild.
		/// </summary>
		/// <param name="permissions">The permissions to allow the bot to have</param>
		/// <returns>A valid URL used to direct users.</returns>
		public string GetBotInvite(Permission permissions)
		{
			return string.Format("https://discordapp.com/api/oauth2/authorize?client_id={0}&scope={1}&permissions={2}",
				 User.ID,
				 User.IsBot ? "bot" : "user",
				 (int)permissions
			 );
		}

		#endregion
	}
}
