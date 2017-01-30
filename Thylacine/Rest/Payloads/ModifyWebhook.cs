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
    class ModifyWebhook : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/webhooks/{WebhookID}";
        object IRestPayload.Payload => this;

        public ulong WebhookID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }

        internal ModifyWebhook() {}
        internal ModifyWebhook(Webhook hook) { WebhookID = hook.ID; }

    }
}
