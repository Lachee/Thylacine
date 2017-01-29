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
    public class DeleteChannel : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/channels/{ChannelID}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }

        public DeleteChannel() { }
        public DeleteChannel(Channel channel) { this.ChannelID = channel.ID; }
    }
}
