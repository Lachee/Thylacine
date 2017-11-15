
using Thylacine.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    class CreateChannel : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/guilds/{GuildID}/channels";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong GuildID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public ChannelType Type { get; set; }

        [JsonProperty("bitrate")]
        public int? Bitrate { get; set; }

        [JsonProperty("user_limit")]
        public int? UserLimit { get; set; }

        [JsonProperty("permission_overwrites")]
        public Overwrite[] Permissions { get; set; }
    }
}
