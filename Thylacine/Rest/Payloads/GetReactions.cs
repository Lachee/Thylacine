
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
    class GetReactions : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}/reactions/{Reaction}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }
        public ulong Reaction { get; set; }

        internal GetReactions() { }
        internal GetReactions(Message message) {
            this.ChannelID = message.ChannelID;
            this.MessageID = message.ID;
        }  
    }
}
