using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models.Event
{
    public class TypingStartEvent
    {
        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ChannelID { get; set; }

        [JsonProperty("user_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong UserID { get; set; }

        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
