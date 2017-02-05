using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Exceptions;

namespace Thylacine.Models
{
    /// <summary>
    /// A Guild is a representation of the Servers within Discord. A bot can be connected to several guilds at once to communicated with different groups of people.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Guild
    {
        /// <summary>
        /// A reference back to the discord object that created this guild.
        /// </summary>
        public DiscordBot Discord { get; internal set; }

        #region Json Properties
        /// <summary>
        /// The ID of the guild
        /// </summary>
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        /// <summary>
        /// The name of the guild
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The hash of the icon for the guild. This is used in fetching the image via URL.
        /// </summary>
        [JsonProperty("icon")]
        public string IconHash { get; internal set; }

        /// <summary>
        /// The hash of the splash for the guild. This is used in fetching the image via URL.
        /// </summary>
        [JsonProperty("splash")]
        public string SplashHash { get; internal set; }

        /// <summary>
        /// The ID of the user who owns the guild.
        /// </summary>
        [JsonProperty("owner_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong OwnerID { get; internal set; }

        /// <summary>
        /// The region the guild is based in.
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; internal set; }

        /// <summary>
        /// The ID of the AFK Channel.
        /// </summary>
        [JsonProperty("afk_channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong AFKChannelID { get; internal set; }

        /// <summary>
        /// The time (in seconds) of inactivity before the user is considered to be AFK.
        /// </summary>
        [JsonProperty("afk_timeout")]
        public int AFKTimeout { get; internal set; }

        /// <summary>
        /// Are embeds allowed in the guild?
        /// </summary>
        [JsonProperty("embed_enabled")]
        public bool EmbedEnabled { get; internal set; }

        /// <summary>
        /// The ID of the channel that embeds go to.
        /// </summary>
        [JsonProperty("embed_channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong EmbedChannelID { get; internal set; }

        /// <summary>
        /// The level of verification required to be apart of this guild.
        /// </summary>
        [JsonProperty("verification_level")]
        public int VerificationLevel { get; internal set; }

        /// <summary>
        /// The default level of notifications new users receive.
        /// </summary>
        [JsonProperty("default_message_notifications")]
        public int DefaultMessageNotifications { get; internal set; }

        /// <summary>
        /// A list of roles available on this guild.
        /// </summary>
        /// <remarks>This is implemented with a dictionary backend. It is recommended to fetch the dictionary with <see cref="GetRoles()"/>.</remarks>
        /// <seealso cref="GetRoles()"/>
        [JsonProperty("roles")]
        public Role[] Roles
        {
            get
            {
                return _roles.Select(k => k.Value).ToArray();
            }
            set
            {
                _roles = new Dictionary<ulong, Role>();
                foreach (var r in value)
                {
                    r.Guild = this;
                    _roles.Add(r.ID, r);
                }
            }
        }

        /// <summary>
        /// A list of custom Emojis that the guild uses.
        /// </summary>
        [JsonProperty("emojis")]
        public Emoji[] Emojis { get; internal set; }

        /// <summary>
        /// A list of features that the guild uses.
        /// </summary>
        [JsonProperty("features")]
        public string[] Features { get; internal set; }

        /// <summary>
        /// The default 2 factor authication level.
        /// </summary>
        [JsonProperty("mfa_level")]
        public int MFALevel { get; internal set; }

        /// <summary>
        /// The current count of members in the guild.
        /// </summary>
        [JsonProperty("member_count")]
        public int MemberCount { get; internal set; }

        /// <summary>
        /// The timestamp at which the bot goined the guild.
        /// </summary>
        //TODO: Make this a Timestamp object
        [JsonProperty("joined_at")]
        public object JoinedAt { get; internal set; }

        /// <summary>
        /// Is the guild considered to large to download all the users from?
        /// </summary>
        //TODO: Implement FetchUsers
        [JsonProperty("large")]
        public bool IsLarge { get; internal set; }

        /// <summary>
        /// Is the guild currently unavailable?
        /// </summary>
        [JsonProperty("unavailable")]
        public bool Unavailable { get; internal set; }

        /// <summary>
        /// A list of voice states for users.
        /// </summary>
        /// <remarks>This isn't optimised with dictionaries and can be slow to lookup.</remarks>
        [JsonProperty("voice_states")]
        public VoiceState[] VoiceStates { get; internal set; }

        /// <summary>
        /// A list of members that are apart of this guild.
        /// </summary>
        /// <remarks>This is implemented with a dictionary backend. It is recommended to fetch the dictionary with <see cref="GetMembers()"/>.</remarks>
        /// <seealso cref="GetMembers()"/>
        [JsonProperty("members")]
        public GuildMember[] GuildMembers
        {
            get
            {
                return _guildmembers.Select(k => k.Value).ToArray();
            }
            internal set
            {
                _guildmembers = new Dictionary<ulong, GuildMember>();
                foreach (var m in value) _guildmembers.Add(m.User.ID, m);
            }
        }

        /// <summary>
        /// A list of channels that are within the guild.
        /// </summary>
        /// <remarks>This is implemented with a dictionary backend. It is recommended to fetch the dictionary with <see cref="GetChannels()"/>.</remarks>
        /// <seealso cref="GetChannels()"/>
        [JsonProperty("channels")]
        public Channel[] Channels
        {
            get
            {
                return _channels.Select(k => k.Value).ToArray();
            }
            internal set
            {
                _channels = new Dictionary<ulong, Channel>();
                foreach (var c in value)
                {
                    c.Guild = this;
                    _channels.Add(c.ID, c);
                }
            }
        }
        
        /// <summary>
        /// A list of user presences.
        /// </summary>
        /// <remarks>Presences are automatically applied and updated to <see cref="GuildMember"/> objects. It is recommended to not access this directly.</remarks>
        /// <seealso cref="GuildMember"/>
        [JsonProperty("presences")]
        public Presence[] Presences { get; internal set; }
        #endregion

        private Dictionary<ulong, GuildMember> _guildmembers = new Dictionary<ulong, GuildMember>();
        private Dictionary<ulong, Role> _roles = new Dictionary<ulong, Role>();
        private Dictionary<ulong, Channel> _channels = new Dictionary<ulong, Channel>();

        /// <summary>Does the inital mapping of the presences</summary>
        internal void SyncronizePresence() { foreach (Presence p in Presences) UpdateMemberPresence(p); }

        #region Channel Management
        internal void UpdateChannel(Channel c) { _channels[c.ID] = c; }
        internal void RemoveChannel(Channel c) { _channels.Remove(c.ID); }

        /// <summary>
        /// Returns a channel
        /// </summary>
        /// <param name="id">ID of the channel</param>
        /// <returns>Channel object</returns>
        public Channel GetChannel(ulong id) { return _channels[id]; }

        /// <summary>
        /// Get a list of all the channels within this guild
        /// </summary>
        /// <returns> Returns a dictionary of id => channel pairs that are of every channel</returns>
        public Dictionary<ulong, Channel> GetChannels() { return _channels; }

        /// <summary>
        /// Does the current guild have a specified channel ID?
        /// </summary>
        /// <param name="id">ID to look for</param>
        /// <returns>true of the guild has the channel</returns>
        public bool HasChannel(ulong id) { return _channels.ContainsKey(id); }

        /// <summary>
        /// Creates a new text channel in the guild. Requires the 'MANAGE_CHANNELS' permission. 
        /// </summary>
        /// <param name="name">Name of the channel. Between 2 and 100 characters long.</param>
        /// <param name="permissions">The channel's permission overwrites.</param>
        /// <returns>Returns the new channel that was created</returns>
        public Channel CreateTextChannel(string name, Overwrite[] permissions)
        {
            if (name.Length < 2 || name.Length > 100) throw new ArgumentException("Channel name must be greater than 2 characters and less than 100. A length of " + name.Length + " was given.");
            return CreateChannel(name, ChannelType.Text, null, null, permissions);
        }

        /// <summary>
        /// Creates a new voice channel in the guild. Requires the 'MANAGE_CHANNELS' permission. 
        /// </summary>
        /// <param name="name">Name of the channel. Between 2 and 100 characters long.</param>
        /// <param name="bitrate">The bitrate of the voice channel</param>
        /// <param name="userlimit">The user limit of the voice channel</param>
        /// <param name="permissions">The channel's permission overwrites.</param>
        /// <returns>Returns the new channel that was created</returns>
        public Channel CreateVoiceChannel(string name, int bitrate, int userlimit, Overwrite[] permissions)
        {
            if (name.Length < 2 || name.Length > 100) throw new ArgumentException("Channel name must be greater than 2 characters and less than 100. A length of " + name.Length + " was given.");
            return CreateChannel(name, ChannelType.Voice, bitrate, userlimit, permissions);
        }
        internal Channel CreateChannel(string name, ChannelType type, int? bitrate, int? userlimit, Overwrite[] permissions)
        {
            if (name.Length < 2 || name.Length > 100) throw new ArgumentException("Channel name must be greater than 2 characters and less than 100. A length of " + name.Length + " was given.");
            Channel c = Discord.Rest.SendPayload<Channel>(new Rest.Payloads.CreateChannel()
            {
                GuildID = ID,
                Name = name,
                Type = type,
                Bitrate = bitrate,
                UserLimit = userlimit,
                Permissions = permissions
            });

            c.Guild = this;
            return c;
        }
        #endregion

        #region Role Management        
        internal void UpdateRole(Role r)
        {
            _roles[r.ID] = r;
            _roles[r.ID].Guild = this;
        }
        internal void RemoveRole(ulong r)
        {
            _roles.Remove(r);
        }

        /// <summary>
        /// Gets a role
        /// </summary>
        /// <param name="id">ID of the role</param>
        /// <returns>The role object</returns>
        public Role GetRole(ulong id) { return _roles[id]; }

        /// <summary>
        /// Gets a list of all roles
        /// </summary>
        /// <returns> Returns a dictionary of id => role pairs that are of every role</returns>
        public Dictionary<ulong, Role> GetRoles() { return _roles; }

        /// <summary>
        /// Does the guild have a specified role?
        /// </summary>
        /// <param name="id">ID of the role</param>
        /// <returns>true if role was found</returns>
        public bool HasRole(ulong id) { return _roles.ContainsKey(id); }

        /// <summary>
        /// Create a new role for the guild. Requires the 'MANAGE_ROLES' permission. ID is ignored.
        /// </summary>
        /// <param name="role">The role to create</param>
        /// <returns>Returns the new role object</returns>
        public Role CreateRole(Role role)
        {
            if (Discord == null) throw new DiscordMissingException();
            Role r = Discord.Rest.SendPayload<Role>(new Rest.Payloads.CreateGuildRole(this, role));
            UpdateRole(r);

            return r;
        }
        
        #endregion

        #region Member Management
        internal void UpdateMember(GuildMember m)
        {
            _guildmembers[m.User.ID] = m;
            _guildmembers[m.User.ID].Guild = this;
            _guildmembers[m.User.ID].User.GuildID = this.ID;
            _guildmembers[m.User.ID].UpdateRoles(this);
        }
        internal void RemoveMember(ulong id)
        {
            _guildmembers.Remove(id);
        }

        /// <summary>Update the presence of a user in this guild</summary>
        internal void UpdateMemberPresence(Presence p)
        {
            //Updates the presence of the user
            if (p.GuildID != this.ID) return;

            //Get the member
            GuildMember member;
            if (!_guildmembers.TryGetValue(p.Updates.ID, out member)) return;

            //Update them
            member.Guild = this;
            member.UpdatePresence(p);
        }
        internal void UpdateUser(User u)
        {
            if (!HasMember(u.ID)) return;
            _guildmembers[u.ID].User = u;
        }

        /// <summary>
        /// Gets a member within the guild
        /// </summary>
        /// <param name="id">The User ID of the member.</param>
        /// <returns>Returns target member</returns>
        public GuildMember GetMember(ulong id) { return _guildmembers[id]; }

        /// <summary>
        /// Gets a list of members that are apart of the guild.
        /// </summary>
        /// <returns> Returns a dictionary of id => member pairs that are of every member</returns>
        public Dictionary<ulong, GuildMember> GetMembers() { return _guildmembers; }

        /// <summary>
        /// Checks id the a member is apart of this guild
        /// </summary>
        /// <param name="id">The User ID of the member.</param>
        /// <returns>Returns true if the member is apart of the guild</returns>
        public bool HasMember(ulong id) { return _guildmembers.ContainsKey(id); }


        internal struct PruneResult
        {
            [JsonProperty("pruned")]
            public int Count { get; set; }
        }

        /// <summary>
        /// Returns the number indicating how many members would be removed in a prune operation.
        /// </summary>
        /// <param name="days">Number of days to count prune for (1 or more)</param>
        /// <returns>Returns the count of members that will be removed</returns>
        public int FetchPruneCount(int days)
        {
            if (Discord == null) throw new DiscordMissingException();
            if (days < 1) throw new ArgumentException("Days must be 1 or more.");

            PruneResult result = Discord.Rest.SendPayload<PruneResult>(new Rest.Payloads.GetGuildPruneCount(this, days));
            return result.Count;
        }

        /// <summary>
        /// Begin a prune operation. Requires the 'KICK_MEMBERS' permission.
        /// </summary>
        /// <param name="days">Number of days to count prune for (1 or more)</param>
        /// <returns> Returns number of members that were removed in the prune operation.</returns>
        public int Prune(int days)
        {
            if (Discord == null) throw new DiscordMissingException();
            if (days < 1) throw new ArgumentException("Days must be 1 or more.");

            PruneResult result = Discord.Rest.SendPayload<PruneResult>(new Rest.Payloads.BeginGuildPrune(this, days));
            return result.Count;
        }

        #endregion

        #region Ban Management

        /// <summary>
        /// Returns a list of user objects that are banned from this guild. Requires the 'BAN_MEMBERS' permission.
        /// </summary>
        /// <returns></returns>
        public List<User> FetchBans()
        {
            if (Discord == null) throw new DiscordMissingException();
            return Discord.Rest.SendPayload<List<User>>(new Rest.Payloads.GetGuildBans(this));
        }

        /// <summary>
        /// Remove the ban for a user. Requires the 'BAN_MEMBERS' permissions.
        /// </summary>
        /// <param name="user"></param>
        public void RemoveBan(User user)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.RemoveGuildBan() { GuildID = ID, UserID = user.ID });
        }

        #endregion

        #region Misc

        /// <summary>
        /// Returns a list of invites for the guild. Requires the 'MANAGE_GUILD' permission.
        /// </summary>
        /// <returns></returns>
        public List<Invite> FetchInvites()
        {
            if (Discord == null) throw new DiscordMissingException();
            return Discord.Rest.SendPayload<List<Invite>>(new Rest.Payloads.GetGuildInvites(this));
        }

        /// <summary>
        /// Fetches webhooks for this guild.
        /// </summary>
        /// <returns></returns>
        public List<Webhook> FetchWebhooks()
        {
            if (Discord == null) throw new DiscordMissingException();
            List<Webhook> hooks = Discord.Rest.SendPayload<List<Webhook>>(new Rest.Payloads.GetWebhooks() { ScopeID = this.ID, Scope = "guilds" });
            foreach (Webhook h in hooks) h.Discord = Discord;
            return hooks;
        }

        /// <summary>
        /// Modifies the guilds settings
        /// </summary>
        public void ApplyModifications(GuildModification modification)
        {
            if (Discord == null) throw new DiscordMissingException();
            Guild g = Discord.Rest.SendPayload<Guild>(new Rest.Payloads.ModifyGuild(this, modification));
            UpdateGuild(g, Discord);
        }

        /// <summary>
        /// Modifies the nickname of the current user in the guild. Requires CHANGE_NICKNAME permission.
        /// </summary>
        /// <param name="nickname"></param>
        public void SetNickname(string nickname)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.ModifyGuildNickname() { GuildID = ID, Nickname = nickname });
        }

        /// <summary>
        /// Delete a guild permanently. User must be owner.
        /// </summary>
        public void DeleteGuild()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteGuild(this));
        }
        
        internal void UpdateGuild(Guild g, DiscordBot discord)
        {
            this.Discord = discord;
            this.Name = g.Name;
            this.IconHash = g.IconHash;
            this.SplashHash = g.SplashHash;
            this.OwnerID = g.OwnerID;
            this.Region = g.Region;
            this.AFKChannelID = g.AFKChannelID;
            this.AFKTimeout = g.AFKTimeout;
            this.EmbedEnabled = g.EmbedEnabled;
            this.EmbedChannelID = g.EmbedChannelID;
            this.VerificationLevel = g.VerificationLevel;
            this.DefaultMessageNotifications = g.DefaultMessageNotifications;
            this.Roles = g.Roles;
            this.Emojis = g.Emojis;
            this.Features = g.Features;
            this.MFALevel = g.MFALevel;
            this.MemberCount = g.MemberCount;

            SyncronizePresence();
        }
        #endregion
        
    }
    public class UnavailableGuild
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; set; }

        [JsonProperty("unavailable")]
        public bool Unavailable { get; set; }

    }

    public class UserGuild
    {

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string IconHash { get; set; }

        [JsonProperty("owner")]
        public bool IsOwner { get; set; }

        [JsonProperty("permissions")]
        public Permission Permissions { get; set; }
    }

    public struct GuildModification {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
        
        [JsonProperty("verification_level")]
        public int? VerificationLevel { get; set; }
        [JsonProperty("default_message_notifications")]
        public int? DefaultMessageNotifications { get; set; }

        [JsonProperty("afk_channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? AFKChannelID { get; set; }
        [JsonProperty("afk_timeout")]
        public int? AFKTimeout { get; set; }

        [JsonProperty("owner_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? OwnerID { get; set; }

        [JsonProperty("icon")]
        public Avatar Icon { get; set; }

        [JsonProperty("splash")]
        public Avatar Splash { get; set; }
    }

}

