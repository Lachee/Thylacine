using Thylacine.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;


namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EditChannelPermissions : IRestPayload
    {
        Method IRestPayload.Method => Method.PUT;
        string IRestPayload.Request => $"/channels/{ChannelID}/permissions/{OverwriteID}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong OverwriteID { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public OverwriteType Type { get; set; }

        [JsonProperty("allow")]
        public Permission Allow { get; set; }

        [JsonProperty("deny")]
        public Permission Deny { get; set; }
    }
}
