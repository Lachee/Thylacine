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
    public class PresenceUpdate
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
		public ulong GuildID { get; set; }

		[JsonProperty("user")]
        public PresenceUser User { get; set; }

        [JsonProperty("game")]
        public Game? Game { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
		
        [JsonProperty("roles"), JsonConverter(typeof(SnowflakeArrayConverter))]
        public ulong[] Roles { get; set; }
    }

    public struct Game
	{
		public enum GameType
		{
			Game = 0,
			Streaming = 1
		}

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("type")]
		public GameType Type { get; set; }

		[JsonProperty("url")]
		public string URL { get; set; }
	}

    public enum Status
    {
        Idle,
        Online,
        Offline,
        Dnd
    }
}
