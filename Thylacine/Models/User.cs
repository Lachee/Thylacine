using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        public string MentionTag { get { return "<@" + this.ID + ">"; } }

        #region Json Properties
        [JsonProperty("username")]
        public string Username { get; internal set; }

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        /// <summary>
        /// The current guild of the user. This is only present in Ban events.
        /// </summary>
        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? GuildID { get; internal set; }

        [JsonProperty("discriminator")]
        public string Discriminator { get; internal set; }

        [JsonProperty("avatar")]
        public string AvatarHash { get; internal set; }

        [JsonProperty("bot")]
        public bool IsBot { get; internal set; }

        [JsonProperty("mfa_enabled")]
        public bool MFAEnabled { get; internal set; }

        [JsonProperty("verified")]
        public bool Verified { get; internal set; }

        [JsonProperty("email")]
        public string Email { get; internal set; }
        #endregion
        
        //Dont know why I am doing this over just setting it
        internal void Update(PresenceUser pu)
        {
            this.Username = pu.Username ?? this.Username;
            this.Discriminator = pu.Discriminator ?? this.Discriminator;
            this.AvatarHash = pu.Avatar ?? this.AvatarHash;

            this.IsBot = pu.Bot ?? this.IsBot;
            this.MFAEnabled = pu.MFAEnabled ?? this.MFAEnabled;
            this.Verified = pu.Verified ?? this.Verified;
            this.Email = pu.Email ?? this.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj is User && ((User)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
    
    [JsonObject(MemberSerialization.OptIn)]
    public class PresenceUser
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        [JsonProperty("username")]
        public string Username { get; internal set; }
        
        [JsonProperty("discriminator")]
        public string Discriminator { get; internal set; }

        [JsonProperty("avatar")]
        public string Avatar { get; internal set; }

        [JsonProperty("bot")]
        public bool? Bot { get; internal set; }

        [JsonProperty("mfa_enabled")]
        public bool? MFAEnabled { get; internal set; }

        [JsonProperty("verified")]
        public bool? Verified { get; internal set; }

        [JsonProperty("email")]
        public string Email { get; internal set; }
    }
}
