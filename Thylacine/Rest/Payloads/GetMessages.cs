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
        object IRestPayload.Payload => null;
		QueryParam[] IRestPayload.Params => new QueryParam[]
		{
			new QueryParam("before", Before),
			new QueryParam("around", Around),
			new QueryParam("after", After),
			new QueryParam("limit", Limit)
		};

		public ulong ChannelID { get; set; }
		
        public ulong? Before { get; set; }
        public ulong? Around { get; set; }
        public ulong? After { get; set; }
        public int Limit { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class GetMessage : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        object IRestPayload.Payload => null;
		QueryParam[] IRestPayload.Params => null;
		string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}";

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class GetPinnedMessages : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;
		string IRestPayload.Request => $"/channels/{ChannelID}/pins";

        public ulong ChannelID { get; set; }
    }
}
