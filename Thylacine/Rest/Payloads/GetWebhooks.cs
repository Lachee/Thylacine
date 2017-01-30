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
    class GetWebhooks : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/{Scope}/{ScopeID}/webhooks";
        object IRestPayload.Payload => this;

        public ulong ScopeID { get; set; }
        public string Scope { get; set; } = "channels";
    }

    [JsonObject(MemberSerialization.OptIn)]
    class GetWebhook : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        object IRestPayload.Payload => this;
        string IRestPayload.Request
        {
            get {  return "/webhooks/" + WebhookID + (string.IsNullOrEmpty(Token) ? "" : "/" + Token); }
        }

        public ulong WebhookID { get; set; }      
        public string Token { get; set; } = null;

        
    }
}
