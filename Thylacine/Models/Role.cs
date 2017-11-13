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
    public class Role
    {
		/// <summary>
		/// The guild the role belongs too.
		/// </summary>
        public Guild Guild { get; internal set; }

		/// <summary>
		/// The tag used to mention the role. This tag is not always guaranteed to mention the role as <see cref="Mentionable"/> can disallow mentions.
		/// </summary>
		public string MentionTag { get { return "<@&" + this.ID + ">"; } }

		/// <summary>
		/// The unique ID of the role. 
		/// </summary>
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; private set; }

		/// <summary>
		/// The name of the role.
		/// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

		/// <summary>
		/// The RGB colour of the role.
		/// </summary>
        [JsonProperty("color"), JsonConverter(typeof(ColorConverter))]
        public Color Color { get; private set; }

		/// <summary>
		/// The pinned state of the role. If true, members in this role will be displayed seperately.
		/// </summary>
        [JsonProperty("hoist")]
        public bool Hoist { get; private set; }

		/// <summary>
		/// The position of the role in the role list
		/// </summary>
        [JsonProperty("position")]
        public int Position { get; private set; }

		/// <summary>
		/// The permissions of the role
		/// </summary>
        [JsonProperty("permissions")]
        public Permission Permissions { get; private set; }

		/// <summary>
		/// Whether this role is managed by an intergration or not.
		/// </summary>
        [JsonProperty("managed")]
        public bool Managed { get; private set; }

		/// <summary>
		/// The mentionable state of the role. If true, anyone can mention this role.
		/// </summary>
        [JsonProperty("mentionable")]
        public bool Mentionable { get; private set; }

		/// <summary>
		/// Sets the <see cref="Name"/> of the role and applies it to Discord. Use <see cref="SetProperties(string, Color?, Permission?, bool?, bool?)"/> for bulk updates.
		/// </summary>
		/// <param name="name">The new name of the role</param>
		public void SetName(string name) => SetProperties(name: name);
		/// <summary>
		/// Sets the <see cref="Color"/> of the role and applies it to Discord. Use <see cref="SetProperties(string, Color?, Permission?, bool?, bool?)"/> for bulk updates.
		/// </summary>
		/// <param name="color">The color of the role displayed in Discord.</param>
		public void SetColor(Color color) => SetProperties(color: color);
		/// <summary>
		/// Sets the <see cref="Permissions"/> of the role and applies it to Discord. Use <see cref="SetProperties(string, Color?, Permission?, bool?, bool?)"/> for bulk updates.
		/// </summary>
		/// <param name="permissions">The <see cref="Permission"/> the role gives to members.</param>
		public void SetPermissions(Permission permissions) => SetProperties(permissions: permissions);
		/// <summary>
		/// Sets the <see cref="Hoist"/> state of the role. The hoist is the grouping of roles in the userlist. Use <see cref="SetProperties(string, Color?, Permission?, bool?, bool?)"/> for bulk updates.
		/// </summary>
		/// <param name="hoist">The pinned state of the role in the user list</param>
		public void SetHoist(bool hoist) => SetProperties(hoist: hoist);
		/// <summary>
		/// Sets the <see cref="Mentionable"/> state of the role and applies it to Discord. Use <see cref="SetProperties(string, Color?, Permission?, bool?, bool?)"/> for bulk updates.
		/// </summary>
		/// <param name="mentionable">If mentionable state of the role. If true, anyone can mention the role.</param>
		public void SetMentionable(bool mentionable) => SetProperties(mentionable: mentionable);
		
		/// <summary>
		/// Sets the properties of the roles and applies them to discord. All fields are optional.
		/// <para>This is generally more efficent than calling many <see cref="SetName(string)"/> and the like when updating all at once.</para>
		/// </summary>
		/// <param name="name">Set the name of the role</param>
		/// <param name="color">Set the color of the role</param>
		/// <param name="permissions">Set the permission of the role</param>
		/// <param name="hoist">Set the hoist of the role</param>
		/// <param name="mentionable">Set the mentionable of the role</param>
		public void SetProperties(string name = null, Color? color = null, Permission? permissions = null, bool? hoist = null, bool? mentionable = null)
		{
			this.Name = name ?? this.Name;
			this.Color = color ?? this.Color;
			this.Permissions = permissions ?? this.Permissions;
			this.Hoist = hoist ?? this.Hoist;
			this.Mentionable = mentionable ?? this.Mentionable;

			Apply();
		}

		internal async void Apply()
		{
			if (Guild == null) throw new GuildMissingException();
			await Guild.Discord.Rest.SendPayload<Role>(new Rest.Payloads.ModifyGuildRole(this));
		}

		/// <summary>
		/// Delete a guild role. Requires the 'MANAGE_ROLES' permission. 
		/// </summary>
		public void Delete()
        {
           // if (Discord == null) throw new DiscordMissingException();
            //Discord.Rest.SendPayload(new Rest.Payloads.DeleteGuildRole(this));
        }

		public static Permission ComputePermission(params Role[] roles)
		{
			if (roles == null || roles.Length == 0) throw new ArgumentOutOfRangeException();

			Permission permission = roles[0].Permissions;
			for (int i = 1; i < roles.Length; i++)
				permission |= roles[i].Permissions;

			return permission;
		}
    }
}
