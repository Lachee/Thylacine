using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Exceptions;
using Thylacine.Helper;

namespace Thylacine.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GuildMember
    {
        public Guild Guild { get; internal set; }
        public DiscordBot Discord { get { return Guild?.Discord; } }

        [JsonProperty("user")]
        public User User { get; internal set; }

        public ulong ID { get { return User.ID; } }

        [JsonProperty("nick")]
        public string Nickname { get; internal set; }
        
        [JsonProperty("roles"), JsonConverter(typeof(SnowflakeArrayConverter))]
        public ulong[] RoleIDs { get; internal set; }

        public Role[] Roles { get; internal set; }

        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; internal set; }

        [JsonProperty("deaf")]
        public bool Deaf { get; internal set; }

        [JsonProperty("mute")]
        public bool Mute { get; internal set; }

        public Presence Presence { get; private set; }
        
        public string MentionTag { get { return "<@!" + this.ID + ">"; } }

        internal void UpdatePresence(Presence p)
        {
            this.User.Update(p.Updates);
            this.Presence = p;
        }
        internal void UpdateRoles(Guild guild)
        {
            List<Role> roles = new List<Role>();
            for (int i = 0; i < RoleIDs.Length; i++)
            {
                Role r = guild.GetRole(RoleIDs[i]);
                if (r != null) roles.Add(r);
            }

            Roles = roles.ToArray();
        }

        #region REST Calls

        /// <summary>
        /// Modifies attributes of this current GuildMember
        /// </summary>
        /// <param name="modification"></param>
        public void ApplyModifications(GuildMemberModification modification)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.ModifyGuildMember(this, modification));
        }

        /// <summary>
        /// Adds a role to the guild member. Requires the MANAGE_ROLES permission. This does NOT add a new role and can only be applied to already existing roles.
        /// </summary>
        /// <param name="role"></param>
        public void AddRole(Role role)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.AddGuildMemberRole()
            {
                GuildID = Guild.ID,
                UserID = User.ID,
                RoleID = role.ID
            });
        }

        /// <summary>
        /// Removes a role from the guild member. Requires the MANAGE_ROLES permission.
        /// </summary>
        /// <param name="role"></param>
        public void RemoveRole(Role role)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.RemoveGuildMemberRole()
            {
                GuildID = Guild.ID,
                UserID = User.ID,
                RoleID = role.ID
            });
        }

        /// <summary>
        /// Removes the member from the guild. Requires KICK_MEMBERS permission.
        /// </summary>
        public void Kick()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.RemoveGuildMember(this));
        }

        /// <summary>
        /// Create a guild ban, and optionally delete previous messages sent by the banned user. Requires the 'BAN_MEMBERS' permission.
        /// </summary>
        /// <param name="PurgeDays">number of days to delete messages for (0-7)</param>
        public void Ban(int PurgeDays = 0)
        {
            if (Discord == null) throw new DiscordMissingException();
            if (PurgeDays < 0 || PurgeDays > 7) throw new ArgumentException("Cannot purge " + PurgeDays + " days. Must be within range of 0 to 7 days.");
            Discord.Rest.SendPayload(new Rest.Payloads.CreateGuildBan(this));
        }
        #endregion
    }

    public struct GuildMemberModification
    {
        /// <summary>
        /// Update the nickname. Requires MANAGE_NICKNAMES.
        /// </summary>
        [JsonProperty("nick")]
        public string Nickname { get; set; }

        /// <summary>
        /// Update the users roles. Requires MANAGE_ROLES.
        /// </summary>
        [JsonProperty("roles")]
        public Role[] Roles { get; set; }

        /// <summary>
        /// Mutes the user. Requires MUTE_MEMBERS.
        /// </summary>
        [JsonProperty("mute")]
        public bool? Mute { get; set; }

        /// <summary>
        /// Silences the user. Requires DEAFEN_MEMBERS.
        /// </summary>
        [JsonProperty("deaf")]
        public bool? Deaf { get; set; }

        /// <summary>
        /// ID of channel to move user to (if they are connected to voice). Requires MOVE_MEMBERS.
        /// </summary>
        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? ChannelID { get; set; }
    }
}
