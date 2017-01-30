using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    public class DMChannel
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; set; }
        
        [JsonProperty("last_message_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong LastMessageID { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("recipient")]
        public User Recipient { get; set; }

    }
}
