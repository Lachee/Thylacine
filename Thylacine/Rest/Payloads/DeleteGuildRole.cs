using Thylacine.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DeleteGuildRole : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/guilds/{GuildID}/roles/{RoleID}";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }
        public ulong RoleID { get; set; }

        public DeleteGuildRole() { }
        public DeleteGuildRole(Role role) { this.GuildID = role.Guild.ID; this.RoleID = role.ID; }
    }
}
