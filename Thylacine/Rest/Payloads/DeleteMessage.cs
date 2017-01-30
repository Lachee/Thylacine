using Thylacine.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DeleteMessage : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }

        public DeleteMessage() { }
        public DeleteMessage(Message message) { }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class DeleteBulkMessages : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/bulk-delete";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }

        [JsonProperty("messages")]
        public ulong[] Snowflakes { get; set; }
    }

}
