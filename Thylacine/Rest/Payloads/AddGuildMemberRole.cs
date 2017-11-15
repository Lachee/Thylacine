
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
    class AddGuildMemberRole : IRestPayload
    {
        Method IRestPayload.Method => Method.PUT;
        string IRestPayload.Request => $"/guilds/{GuildID}/members/{UserID}/roles/{RoleID}";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong GuildID { get; set; }
        public ulong UserID { get; set; }
        public ulong RoleID { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class RemoveGuildMemberRole : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/guilds/{GuildID}/members/{UserID}/roles/{RoleID}";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong GuildID { get; set; }
        public ulong UserID { get; set; }
        public ulong RoleID { get; set; }
    }
}
