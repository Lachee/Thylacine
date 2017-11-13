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
    public class Presence
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
		public ulong GuildID { get; set; }

		[JsonProperty("user")]
        public PresenceUser User { get; set; }

        [JsonProperty("game")]
        public Game? Game { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
		
        [JsonProperty("roles"), JsonConverter(typeof(SnowflakeArrayConverter))]
        public ulong[] Roles { get; set; }

		public string FormatGame(string playing = "Playing", string streaming = "Streaming")
		{
			if (!Game.HasValue) return "";
			switch(Game.Value.Type)
			{
				default:
				case Models.Game.GameType.Game:
					return playing + " " + Game.Value.Name;

				case Models.Game.GameType.Streaming:
					return streaming + " " + Game.Value.Name + " (" + Game.Value.URL + ") ";
			}
		}
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
