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
    public class CreateReaction : IRestPayload
    {
        Method IRestPayload.Method => Method.PUT;
        string IRestPayload.Request => $"/channels/{ChannelID}/messages/{MessageID}/reactions/{Reaction}/@me";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }
        public ulong MessageID { get; set; }
        public ulong Reaction { get; set; }

        public CreateReaction() { }
        public CreateReaction(Channel channel, Emoji emoji) { this.ChannelID = channel.ID; Reaction = emoji.ID.Value; }
    }
}
