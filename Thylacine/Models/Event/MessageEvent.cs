using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models.Event
{
    public class MessageDeleteEvent
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; set; }

        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ChannelID { get; set; }
    }

    public class MessageDeleteBulk
    {
        [JsonProperty("ids"), JsonConverter(typeof(SnowflakeArrayConverter))]
        public ulong[] IDs { get; set; }

        [JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ChannelID { get; set; }
    }
}
