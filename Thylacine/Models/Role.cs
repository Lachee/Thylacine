using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    public class Role
    {
        public string MentionTag { get { return "<@&" + this.ID + ">"; } }

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("hoist")]
        public bool Hoist { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("permissions")]
        public Permission Permission { get; set; }

        [JsonProperty("managed")]
        public bool Managed { get; set; }

        [JsonProperty("mentionable")]
        public bool Mentionable { get; set; }
    }
}
