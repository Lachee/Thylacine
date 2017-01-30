using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models.Event
{
    #region Member Events
    public class GuildMemeberAdd : GuildMember
    {
        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildID { get; set; }
    }

    public class GuildMemberRemove : GuildEvent
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class GuildMemeberUpdate : GuildEvent
    {
        [JsonProperty("roles"), JsonConverter(typeof(SnowflakeArrayConverter))]
        public ulong[] Roles { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("nick")]
        public string Nickname { get; set; }
    }
    
    public class GuildMembersChunk : GuildEvent
    {
        [JsonProperty("members")]
        public GuildMember[] Members { get; set; }
    }
    #endregion
}
