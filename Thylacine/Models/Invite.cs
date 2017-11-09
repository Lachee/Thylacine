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
    [JsonObject(MemberSerialization.OptIn)]
    public class Invite
    {
        [JsonProperty("code")]
        public string Code { get; internal set; }

        [JsonProperty("guild")]
        public InviteGuild Guild { get; internal set; }

        [JsonProperty("channel")]
        public InviteChannel Channel { get; internal set; }

        [JsonProperty("inviter")]
        public User Inviter { get; internal set; }

        [JsonProperty("uses")]
        public int Uses { get; internal set; }

        [JsonProperty("max_uses")]
        public int MaxUsers { get; internal set; }

        [JsonProperty("max_age")]
        public int MaxAge { get; internal set; }

        [JsonProperty("temporary")]
        public bool Temporary { get; internal set; }

        [JsonProperty("created_at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; internal set; }

        [JsonProperty("revoked")]
        public bool Revoked { get; internal set; }

		public override string ToString()
		{
			return string.Format("{0}( https://discord.gg/{1} )", Guild.Name, Code);
		}

	}

    public class InviteGuild
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("splash")]
        public string Splash { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class InviteChannel
    {
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public ChannelType Type { get; set; }
    }
}
