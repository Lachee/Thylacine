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
        /// <summary>
        /// The current <see cref="Guild"/> the member is apart of.
        /// </summary>
        public Guild Guild { get; internal set; }
        /// <summary>
        /// The current <see cref="Thylacine.Discord"/> that the member was created with. 
        /// </summary>
        public Discord Discord { get { return Guild?.Discord; } }

		/// <summary>
		/// The <see cref="User.ID"/> of the guild member.
		/// </summary>
		/// <seealso cref="User.ID"/>
		public ulong ID { get { return User.ID; } }

		/// <summary>
		/// Gets the channel the user is in
		/// </summary>
		public Channel Channel => Guild.GetChannel(_channel);
		[JsonProperty("channel")] private ulong _channel;

		/// <summary>
		/// The user of the guild member
		/// </summary>
		public User User => _user;
		[JsonProperty("user")] private User _user;

		/// <summary>
		/// The nickname used within the guild
		/// </summary>		
		public string Nickname => _nickname;
		[JsonProperty("nick")] private string _nickname;
		
        /// <summary>
        /// A list of roles the user has.
        /// </summary>
        public Role[] Roles { get; private set; }
		[JsonProperty("roles"), JsonConverter(typeof(SnowflakeArrayConverter))]
		private ulong[] _roleids;

		/// <summary>
		/// The time the member goined the guild
		/// </summary>
		//TODO: MAke this a timestamp converter
		public DateTime JoinedAt => _joinedAt;
		[JsonProperty("joined_at")] private DateTime _joinedAt;

		/// <summary>
		/// Is the member deafened in the guild?
		/// </summary>
		public bool Deaf => _deaf;
		[JsonProperty("deaf")] private bool _deaf;

		/// <summary>
		/// Is the member muted in the guild?
		/// </summary>
		public bool Mute => _mute;
		[JsonProperty("mute")] private bool _mute;

        /// <summary>
        /// The member's current presence.
        /// </summary>
        public Presence Presence { get; private set; }
        
        /// <summary>
        /// The tag used to mention the member in chat.
        /// </summary>
        public string MentionTag { get { return "<@!" + this.ID + ">"; } }

        internal void UpdatePresence(Presence p)
        {
            this.User.Update(p.Updates);
            this.Presence = p;
        }
        internal void UpdateRoles(Guild guild)
        {
            List<Role> roles = new List<Role>();
            for (int i = 0; i < _roleids.Length; i++)
            {
                Role r = guild.GetRole(_roleids[i]);
                if (r != null) roles.Add(r);
            }

            Roles = roles.ToArray();
        }

		#region Internal Updates
		internal void UpdateModification(GuildMemberModification modifiction)
		{
			this._nickname = modifiction.Nickname ?? this._nickname;
			this._mute = modifiction.Mute ?? this._mute;
			this._deaf = modifiction.Deaf ?? this._deaf;
			this._channel = modifiction.ChannelID ?? this._channel;
		}


		#endregion

		#region REST Calls

		public void SetNickname(string name)
		{
			ApplyModifications(new GuildMemberModification()
			{
				Nickname = name
			});
		}

        /// <summary>
        /// Modifies attributes of this current GuildMember
        /// </summary>
        /// <param name="modification">The modification to apply</param>
        public void ApplyModifications(GuildMemberModification modification)
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.ModifyGuildMember(this, modification));
        }

        /// <summary>
        /// Adds a role to the guild member. Requires the MANAGE_ROLES permission. This does NOT add a new role and can only be applied to already existing roles.
        /// </summary>
        /// <param name="role">The role to add. This must be a existing role from the <see cref="Guild"/></param>
        /// <seealso cref="Guild"/>
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
        /// <param name="role">Removes a specified role from the member. This must be a existing role from the <see cref="Guild"/></param>
        /// <seealso cref="Guild"/>
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

    /// <summary>
    /// A object used to modify <see cref="GuildMember"/>s. All fields are optional and do not need to be assigned.
    /// </summary>
    /// <seealso cref="GuildMember"/>
    public class GuildMemberModification
    {
        /// <summary>
        /// Update the nickname. Requires MANAGE_NICKNAMES.
        /// </summary>
        [JsonProperty("nick")]
        public string Nickname { get; set; } = null;

		/// <summary>
		/// Update the users roles. Requires MANAGE_ROLES.
		/// </summary>
		[JsonProperty("roles")]
		public Role[] Roles { get; set; } = null;

		/// <summary>
		/// Mutes the user. Requires MUTE_MEMBERS.
		/// </summary>
		[JsonProperty("mute")]
		public bool? Mute { get; set; } = null;

        /// <summary>
        /// Silences the user. Requires DEAFEN_MEMBERS.
        /// </summary>
        [JsonProperty("deaf")]
        public bool? Deaf { get; set; } = null;

		/// <summary>
		/// ID of channel to move user to (if they are connected to voice). Requires MOVE_MEMBERS.
		/// </summary>
		[JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? ChannelID { get; set; } = null;
	}
}
