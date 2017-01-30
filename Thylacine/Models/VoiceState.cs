using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    public class VoiceState
    {
        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? GuildID { get; set; }

        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? ChannelID { get; set; }

        [JsonProperty("user_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong UserID { get; set; }

        [JsonProperty("session_id")]
        public string SessionID { get; set; }

        [JsonProperty("deaf")]
        public bool Deaf { get; set; }

        [JsonProperty("mute")]
        public bool Mute { get; set; }

        [JsonProperty("self_death")]
        public bool SelfDeath { get; set; }

        [JsonProperty("self_mute")]
        public bool SelfMute { get; set; }

        [JsonProperty("suppress")]
        public bool Suppress { get; set; }
    }
}
