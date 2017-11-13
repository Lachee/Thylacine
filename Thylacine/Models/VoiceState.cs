using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
	/// <summary>
	/// Voice State of a user.
	/// </summary>
    public class VoiceState
    {
		/// <summary>
		/// The guild the voice state belongs too. This maybe potentially null as GuildID isn't always available.
		/// </summary>
		public Guild Guild
		{
			get { return _guild; }
			internal set
			{
				_guild = value;
				Channel = _channelID.HasValue ? _guild.GetChannel(_channelID.Value) : null;
				GuildMember = _guild.GetMember(_userID);
			}
		}
		private Guild _guild;

		/// <summary>
		/// The channel the user is in/entered. Null otherwise.
		/// </summary>
		public Channel Channel { get; internal set; }

		/// <summary>
		/// The guild member the voice state relates too.
		/// </summary>
		public GuildMember GuildMember { get; internal set; }

		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
		private ulong? _guildID;

		/// <summary>
		/// The ID of the channel in question.
		/// </summary>
		[JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeConverter))]
		private ulong? _channelID;

		/// <summary>
		/// The ID of the user
		/// </summary>
		[JsonProperty("user_id"), JsonConverter(typeof(SnowflakeConverter))]
		private ulong _userID;

		/// <summary>
		/// The ID of the voice session
		/// </summary>
        [JsonProperty("session_id")]
        public string SessionID { get; set; }

		/// <summary>
		/// Is the user deafen?
		/// </summary>
        [JsonProperty("deaf")]
        public bool Deaf { get; set; }

		/// <summary>
		/// Is the user mute?
		/// </summary>
        [JsonProperty("mute")]
        public bool Mute { get; set; }

		/// <summary>
		/// Has the user put themselves on deafen?
		/// </summary>
        [JsonProperty("self_death")]
        public bool SelfDeath { get; set; }

		/// <summary>
		/// Has the user put themselves on mute?
		/// </summary>
        [JsonProperty("self_mute")]
        public bool SelfMute { get; set; }

		/// <summary>
		/// Has the bot suppressed this users voice activity?
		/// </summary>
        [JsonProperty("suppress")]
        public bool Suppress { get; set; }		

		/// <summary>
		/// Links a Guild to the VoiceStats
		/// </summary>
		/// <param name="discord">The Discord Bot</param>
		/// <returns></returns>
		internal bool AssociateGuild(Discord discord)
		{
			if (!_guildID.HasValue) return false;

			var assocGuild = discord.GetGuild(_guildID.Value);
			if (assocGuild == null) return false;
			
			Guild = assocGuild;
			return true;
		}
    }
}
