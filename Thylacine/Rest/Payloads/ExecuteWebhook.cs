
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
    class ExecuteWebhook : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/webhooks/{WebhookID}/{WebhookToken}";
        object IRestPayload.Payload => this;

        public ulong WebhookID { get; }
        public string WebhookToken { get; }

        [JsonProperty("content")]
        public string Message { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarURL { get; set; }

        [JsonProperty("tts")]
        public bool TTS { get; set; }

        [JsonProperty("embed")]
        public Embed Embed { get; set; }

        internal ExecuteWebhook(Webhook h) { this.WebhookToken = h.Token; this.WebhookID = h.ID; }
    }
}
