using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Contains all the objects used by Discord and Thylacine.
/// </summary>
namespace Thylacine.Models
{
    /// <summary>
    /// A object representing a User from Discord.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        /// <summary>
        /// The tag to use for mentioning the user
        /// </summary>
        public string MentionTag { get { return "<@" + this.ID + ">"; } }

        #region Json Properties
        /// <summary>
        /// The users name
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; internal set; }

        /// <summary>
        /// The users ID
        /// </summary>
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        /// <summary>
        /// The current guild of the user. This is only present in Ban events.
        /// </summary>
        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? GuildID { get; internal set; }

        /// <summary>
        /// The discriminator used by the user
        /// </summary>
        [JsonProperty("discriminator")]
        public string Discriminator { get; internal set; }

        /// <summary>
        /// The hash of the users avatar. This is used in links.
        /// </summary>
        [JsonProperty("avatar")]
        public string AvatarHash { get; internal set; }

        /// <summary>
        /// Is the user a bot?
        /// </summary>
        [JsonProperty("bot")]
        public bool IsBot { get; internal set; }

        /// <summary>
        /// Is 2 factor authentication enabled?
        /// </summary>
        [JsonProperty("mfa_enabled")]
        public bool MFAEnabled { get; internal set; }

        /// <summary>
        /// Is the user verified?
        /// </summary>
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; internal set; }
		#endregion
        
        internal void UpdatePresence(PresenceUpdate presence)
        {
			var pu = presence.User;
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
    
    /// <summary>
    /// A User Object found in presence updates. Similar to a normal <see cref="User"/>, however all its fields (except <see cref="PresenceUser.ID"/>) are optional.
    /// </summary>
    /// <seealso cref="User"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class PresenceUser
    {
        /// <summary>
        /// The ID of the user. This is the only non nullable object within this object.
        /// </summary>
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        /// <summary>
        /// The new username of the user. 
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; internal set; }
        
        /// <summary>
        /// The new discriminator of the user.
        /// </summary>
        [JsonProperty("discriminator")]
        public string Discriminator { get; internal set; }

        /// <summary>
        /// The new avatar hash of the user
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; internal set; }

        /// <summary>
        /// Has the user become a bot?
        /// </summary>
        [JsonProperty("bot")]
        public bool? Bot { get; internal set; }

        /// <summary>
        /// The new 2 factor authientication level.
        /// </summary>
        [JsonProperty("mfa_enabled")]
        public bool? MFAEnabled { get; internal set; }

        /// <summary>
        /// Has the user become verified?
        /// </summary>
        [JsonProperty("verified")]
        public bool? Verified { get; internal set; }

        /// <summary>
        /// The users new email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; internal set; }
    }
}
