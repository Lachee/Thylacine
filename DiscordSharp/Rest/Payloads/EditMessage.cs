using DiscordSharp.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DiscordSharp.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EditMessage : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }

        [JsonProperty("content")]
        public string Message { get; set; }

        public EditMessage() { }
        public EditMessage(Message message) { this.ChannelID = message.ID; this.MessageID = message.ID; }
    }

}
