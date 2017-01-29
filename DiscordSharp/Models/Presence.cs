using DiscordSharp.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Models
{
    public class Presence
    {
        [JsonProperty("user")]
        public PresenceUser Updates { get; set; }

        [JsonProperty("game")]
        public Game? Game { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? GuildID { get; set; }

        [JsonProperty("roles"), JsonConverter(typeof(SnowflakeArrayConverter))]
        public ulong[] Roles { get; set; }
    }

    public struct Game
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public enum Status
    {
        Idle,
        Online,
        Offline,
        Dnd
    }
}
