using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    public class Reaction
    {
        /// <summary>
        /// Times this emoji has been used to react
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Has the current user reacted?
        /// </summary>
        [JsonProperty("me")]
        public bool HaveReacted { get; set; }

        /// <summary>
        /// The current emoji
        /// </summary>
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
    }

    public struct Emoji
    {
        public string MentionTag { get { return "<:" + this.Name + ":" + this.ID + ">"; } }

        /// <summary>
        /// ID of emoji. Null if its not a external emoji
        /// </summary>
        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong? ID { get; set; }

        /// <summary>
        /// The name of the emoji
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
