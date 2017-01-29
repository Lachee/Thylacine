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
    public class ModifyChannel : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/channels/{ChannelID}";
        object IRestPayload.Payload => this;

        public ulong ChannelID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("bitrate")]
        public int? Bitrate { get; set; }

        [JsonProperty("user_limit")]
        public int? UserLimit { get; set; }

        public ModifyChannel() { }
        public ModifyChannel(Channel channel)
        {
            this.ChannelID = channel.ID;
            this.Name = channel.Name;
            this.Position = channel.Position;

            this.Topic = null;
            this.Bitrate = null;
            this.UserLimit = null;

            if (channel is TextChannel)
            {
                Topic = ((TextChannel)channel).Topic;
            }
            else if (channel is VoiceChannel)
            {
                VoiceChannel vc = (VoiceChannel)channel;
                this.Bitrate = vc.Bitrate;
                this.UserLimit = vc.UserLimit;
            }
        }
    }
}
