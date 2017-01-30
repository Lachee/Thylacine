using Thylacine.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    public class Overwrite
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public OverwriteType Type { get; set; }

        [JsonProperty("allow")]
        public Permission Allow { get; set; }

        [JsonProperty("deny")]
        public Permission Deny { get; set; }
    }

    public enum OverwriteType
    {
        Role,
        Member
    }
}
