
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
    class CreateInvite : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/channels/{ChannelID}/invites";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong ChannelID { get; set; }

        [JsonProperty("max_age")]
        public int? MaxAge { get; set; }

        [JsonProperty("max_uses")]
        public int? MaxUses { get; set; }

        [JsonProperty("temporary")]
        public bool? Temporary { get; set; }

        [JsonProperty("unique")]
        public bool? Unique { get; set; }

    }
}
