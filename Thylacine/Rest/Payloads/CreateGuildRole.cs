using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Models;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    class CreateGuildRole : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/guilds/{GuildID}/roles";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("permissions")]
        public Permission Permissions { get; set; }
        [JsonProperty("color")]
        public int Color { get; set; }
        [JsonProperty("hoist")]
        public bool Hoist { get; set; }
        [JsonProperty("mentionable")]
        public bool Mentionable { get; set; }

        internal CreateGuildRole() { }
        internal CreateGuildRole(Guild guild, Role role)
        {
            this.GuildID = guild.ID;
            this.Name = role.Name;
            this.Permissions = role.Permission;
            this.Color = role.Color;
            this.Hoist = role.Hoist;
            this.Mentionable = role.Mentionable;
        }
    }
}
