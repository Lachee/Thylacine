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
    class GetMessages : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }

        [JsonProperty("before")]
        public ulong? Before { get; set; }

        [JsonProperty("around")]
        public ulong? Around { get; set; }

        [JsonProperty("after")]
        public ulong? After { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class GetMessage : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        object IRestPayload.Payload => this;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}";

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class GetPinnedMessages : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        object IRestPayload.Payload => this;
        string IRestPayload.Request => $"/channels/{ChannelID}/pins";

        public ulong ChannelID { get; set; }
    }
}
