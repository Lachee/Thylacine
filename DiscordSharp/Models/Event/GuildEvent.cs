using DiscordSharp.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Models.Event
{
    public class GuildEvent
    {
        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildID { get; set; }
    }
    
    public class GuildDelete
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildID { get; set; }
        
        [JsonProperty("unavailable")]
        public bool Unavailable { get; set; }
    }

    public class GuildBan : GuildEvent
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class GuildEmojiUpdate : GuildEvent
    {
        [JsonProperty("emojis")]
        public Emoji[] Emojis { get; set; }
    }  
}
