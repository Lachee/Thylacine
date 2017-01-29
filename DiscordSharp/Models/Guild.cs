using DiscordSharp.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Guild
    {
        public DiscordBot Discord { get; set; }

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
                foreach (var r in value) _roles.Add(r.ID, r);
            }
        }

            [JsonProperty("emojis")]
        public Emoji[] Emojis { get; internal set; }

        [JsonProperty("features")]
        public string[] Features { get; internal set; }

        [JsonProperty("mfa_level")]
        public int MFALevel { get; internal set; }

        [JsonProperty("member_count")]
        public int MemberCount { get; internal set;  }

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
        internal void SyncronizePresence()  { foreach (Presence p in Presences) UpdateMemberPresence(p);  }
     
        #region Channel Management
        internal void UpdateChannel(Channel c) { _channels[c.ID] = c; }
        internal void RemoveChannel(Channel c) { _channels.Remove(c.ID); }

        public Channel GetChannel(ulong id) { return _channels[id]; }
        public bool HasChannel(ulong id) { return _channels.ContainsKey(id); }
        #endregion

        #region Role Management        
        internal void UpdateRole(Role r)
        {
            _roles[r.ID] = r;
        }
        internal void RemoveRole(ulong r)
        {
            _roles.Remove(r);
        }
        
        public Role GetRole(ulong id) { return _roles[id]; }
        public bool HasRole(ulong id) { return _roles.ContainsKey(id); }
        #endregion

        #region Member Management
        internal void UpdateMember(GuildMember m)
        {
            _guildmembers[m.User.ID] = m;
            _guildmembers[m.User.ID].Discord = this.Discord;
            _guildmembers[m.User.ID].User.GuildID = this.ID;
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
            member.Discord = this.Discord;
            member.UpdatePresence(p);
        }
        internal void UpdateUser(User u)
        {
            if (!HasMember(u.ID)) return;
            _guildmembers[u.ID].User = u;
        }

        public GuildMember GetMember(ulong id) { return _guildmembers[id]; }
        public bool HasMember(ulong id) { return _guildmembers.ContainsKey(id); }
        #endregion

        #region Misc

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
}

