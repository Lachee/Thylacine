﻿using Newtonsoft.Json;
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
			
		public VoiceState VoiceState { get; private set; }

		/// <summary>
		/// The user of the guild member
		/// </summary>
		public User User => _user;
		[JsonProperty("user")] private User _user;

		/// <summary>
		/// The nickname used within the guild
		/// </summary>		
		public string Nickname { get { return _nickname; } internal set { _nickname = value; } }
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
		
		[JsonProperty("deaf")] private bool _deaf;		
		[JsonProperty("mute")] private bool _mute;

        /// <summary>
        /// The member's current presence.
        /// </summary>
        public Presence Presence { get; private set; }
        
        /// <summary>
        /// The tag used to mention the member in chat.
        /// </summary>
        public string MentionTag { get { return "<@!" + this.ID + ">"; } }

		#region Internal Updates
		[System.Obsolete("Not Used")]
		internal void UpdateModification(GuildMemberModification modifiction)
		{
			this._nickname = modifiction.Nickname ?? this._nickname;

			this.VoiceState.Mute = modifiction.Mute ?? this.VoiceState.Mute;
			this.VoiceState.Deaf = modifiction.Deaf ?? this.VoiceState.Deaf;
			this.VoiceState.Channel = modifiction.ChannelID.HasValue ? Guild.GetChannel(modifiction.ChannelID.Value) : this.VoiceState.Channel;
		}
		
		///<summary>Associates the Role ID's from the JSON to actual Role Objects.</summary>
		internal void AssociateRoles(ulong[] roles = null)
		{
			//Apply the new roles
			if (roles != null) _roleids = roles;

			//We have to have a guild to get roles for.
			if (Guild == null) throw new Exceptions.GuildMissingException();

			//Prepare the role array
			Roles = new Role[_roleids.Length];

			//Iterate over each role ID, updating its actual role reference
			for (int i = 0; i < Roles.Length; i++)
				Roles[i] = Guild.GetRole(_roleids[i]);
		}

		///<summary>Creates an initial voicestate</summary>
		internal void InitializeVoiceState()
		{
			this.VoiceState = new VoiceState()
			{
				Guild = this.Guild,
				Channel = null,
				GuildMember = this,
				Deaf = this._deaf,
				Mute = this._mute
			};
		}

		///<summary>Updates the presence of the member.</summary>
		internal void UpdatePresence(Presence p)
		{
			this.Presence = p;
			this.AssociateRoles(p.Roles);
		}

		///<summary>Updates the voice state of the user.</summary>
		internal void UpdateVoiceState(VoiceState s)
		{
			this.VoiceState = s;
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
		
		/// <summary>
		/// Calculates the base permission of the member.
		/// </summary>
		/// <returns>The base permission the user has in the guild.
		/// </returns>
		public Permission ComputePermissions()
		{
			if (Guild == null) throw new GuildMissingException();
			if (Guild.OwnerID == this.ID) return Permission.ALL;

			//Get the everyone role
			Role everyone = Guild.EveryoneRole;
			Permission permissions = everyone.Permissions;

			//Iterate over each additional role, adding the permissions on
			foreach (Role r in this.Roles)
				permissions |= r.Permissions;

			//If we have administrator, we get all the roles
			if (permissions.HasFlag(Permission.Adminstrator))
				return Permission.ALL;

			//Return the final roles.
			return permissions;
		}
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
