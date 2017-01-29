using DiscordSharp.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Models.Event
{
    public class GuildRoleEvent : GuildEvent
    {
        [JsonProperty("role")]
        public Role Role { get; set; }
    }

    public class GuildRoleDelete : GuildEvent
    {
        [JsonProperty("role_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong Role { get; set; }
    }    
}
