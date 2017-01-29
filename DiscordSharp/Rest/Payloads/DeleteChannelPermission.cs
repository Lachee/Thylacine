using DiscordSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;


namespace DiscordSharp.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DeleteChannelPermission : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}/permissions/{OverwriteID}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong OverwriteID { get; set; }
    }
}
