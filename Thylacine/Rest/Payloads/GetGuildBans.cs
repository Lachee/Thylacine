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
    class GetGuildBans : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/guilds/{GuildID}/bans";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }
        public GetGuildBans() { }
        public GetGuildBans(Guild g) { GuildID = g.ID; }
    }
}
