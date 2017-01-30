using Thylacine.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;


namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TriggerTypingIndicator : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/channels/{ChannelID}/typing";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
    }
}
