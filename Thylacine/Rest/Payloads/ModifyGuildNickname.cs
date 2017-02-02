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
    class ModifyGuildNickname : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/guilds/{GuildID}/members/@me/nick";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }

        [JsonProperty("nick")]
        public string Nickname { get; set; }
    }
}
