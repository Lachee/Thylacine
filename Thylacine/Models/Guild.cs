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
    [JsonObject(MemberSerialization.OptIn)]
    public class Guild
    {
        public DiscordBot Discord { get; internal set; }

        #region Json Properties
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("icon")]
        public string IconHash { get; internal set; }

        [JsonProperty("splash")]
        public string SplashHash { get; internal set; }

        [JsonProperty("owner_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong OwnerID { get; internal set; }

        [JsonProperty("region")]
        public string Region { get; internal set; }

        [JsonProperty("afk_channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong AFKChannelID { get; internal set; }

        [JsonProperty("afk_timeout")]
        public int AFKTimeout { get; internal set; }

        [JsonProperty("embed_enabled")]
        public bool EmbedEnabled { get; internal set; }

        [JsonProperty("embed_channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong EmbedChannelID { get; internal set; }

        [JsonProperty("verification_level")]
        public int VerificationLevel { get; internal set; }

        [JsonProperty("default_message_notifications")]
        public int DefaultMessageNotifications { get; internal set; }

        [JsonProperty("roles")]
        public Role[] Roles
        {
            get
            {
                return _roles.Select(k => k.Value).ToArray();
            }
            internal set
            {
                _roles = new Dictionary<ulong, Role>();
                foreach (var r in value)
                {
                    r.Guild = this;
                    _roles.Add(r.ID, r);
                }
            }
        }

        [JsonProperty("emojis")]
        public Emoji[] Emojis { get; internal set; }

        [JsonProperty("features")]
        public string[] Features { get; internal set; }

        [JsonProperty("mfa_level")]
        public int MFALevel { get; internal set; }

        [JsonProperty("member_count")]
        public int MemberCount { get; internal set; }

        [JsonProperty("joined_at")]
        public object JoinedAt { get; internal set; }

        [JsonProperty("large")]
        public bool IsLarge { get; internal set; }

        [JsonProperty("unavailable")]
        public bool Unavailable { get; internal set; }

        [JsonProperty("voice_states")]
        public VoiceState[] VoiceStates { get; internal set; }

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

        public Channel GetChannel(ulong id) { return _channels[id]; }
        public bool HasChannel(ulong id) { return _channels.ContainsKey(id); }

        /// <summary>
        /// Creates a new text channel in the guild. Requires the 'MANAGE_CHANNELS' permission. 
        /// </summary>
        /// <param name="name">Name of the channel. Between 2 and 100 characters long.</param>
        /// <param name="permissions">The channel's permission overwrites.</param>
        /// <returns></returns>
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
        /// <returns></returns>
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

        public Role GetRole(ulong id) { return _roles[id]; }
        public bool HasRole(ulong id) { return _roles.ContainsKey(id); }

        /// <summary>
        /// Create a new role for the guild. Requires the 'MANAGE_ROLES' permission. ID is ignored.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public Role CreateRole(Role r)
        {
            if (Discord == null) throw new DiscordMissingException();
            Role role = Discord.Rest.SendPayload<Role>(new Rest.Payloads.CreateGuildRole(this, r));
            UpdateRole(role);

            return role;
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

        public GuildMember GetMember(ulong id) { return _guildmembers[id]; }
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
        /// <returns></returns>
        public int FetchPruneCount(int days)
        {
            if (Discord == null) throw new DiscordMissingException();
            if (days < 1) throw new ArgumentException("Days must be 1 or more.");

            PruneResult result = Discord.Rest.SendPayload<PruneResult>(new Rest.Payloads.GetGuildPruneCount(this, days));
            return result.Count;
        }

        /// <summary>
        /// Begin a prune operation. Requires the 'KICK_MEMBERS' permission. Returns number of members that were removed in the prune operation.
        /// </summary>
        /// <param name="days">Number of days to count prune for (1 or more)</param>
        /// <returns></returns>
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

