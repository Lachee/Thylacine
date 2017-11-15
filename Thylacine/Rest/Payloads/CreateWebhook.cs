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
    public class CreateWebhook : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/channels/{ChannelID}/webhooks";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong ChannelID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }
    }
}
