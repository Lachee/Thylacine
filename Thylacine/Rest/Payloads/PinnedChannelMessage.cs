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
    public class PinChannelMessage : IRestPayload
    {
        Method IRestPayload.Method => Method.PUT;
        string IRestPayload.Request => $"/channels/{ChannelID}/pins/{MessageID}";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class UnpinChannelMessage : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}/pins/{MessageID}";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }
    }
}
