using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Helper;
using Thylacine.Models;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    class ModifyGuildRole : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/guilds/{Role.Guild.ID}/roles/{Role.ID}";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public Role Role { get; set; }

		[JsonProperty("name")]
		public string Name => Role.Name;

		[JsonProperty("permissions")]
		public Permission Permissions => Role.Permissions;

		[JsonProperty("color"), JsonConverter(typeof(ColorConverter))]
		public Color? Color => Role.Color;

		[JsonProperty("hoist")]
		public bool Hoist => Role.Hoist;

		[JsonProperty("mentionable")]
		public bool Mentionable => Role.Mentionable;

        private ModifyGuildRole() { }
        internal ModifyGuildRole(Role role)
        {
			Role = role;
        }
    }
}
