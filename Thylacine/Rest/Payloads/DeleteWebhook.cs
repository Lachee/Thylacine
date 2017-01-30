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
    public class DeleteWebhook : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{WebhookID}";
        object IRestPayload.Payload => this;

        public ulong WebhookID { get; set; }

        public DeleteWebhook() { }
        public DeleteWebhook(Webhook channel) { this.WebhookID = channel.ID; }
    }
}
