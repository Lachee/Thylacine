using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GuildMember
    {
        public DiscordBot Discord { get; internal set; }

        [JsonProperty("user")]
        public User User { get; internal set; }

        public ulong ID { get { return User.ID; } }

        [JsonProperty("nick")]
        public string Nickname { get; internal set; }

        [JsonProperty("roles")]
        public ulong[] RoleIDs { get; internal set; }

        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; internal set; }

        [JsonProperty("deaf")]
        public bool Deaf { get; internal set; }

        [JsonProperty("mute")]
        public bool Mute { get; internal set; }

        public Presence Presence { get; private set; }
        
        public string MentionTag { get { return "<@!" + this.ID + ">"; } }

        internal void UpdatePresence(Presence p)
        {
            this.User.Update(p.Updates);
            this.Presence = p;
        }
    }
}
