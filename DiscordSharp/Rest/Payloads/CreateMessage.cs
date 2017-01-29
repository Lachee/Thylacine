
using DiscordSharp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    class CreateMessagePayload : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }

        [JsonProperty("content")]
        public string Message { get; set; }

        [JsonProperty("tts")]
        public bool TTS { get; set; }

        [JsonProperty("embed")]
        public Embed Embed { get; set; }
    }
}
