using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Exceptions;

namespace Thylacine.Models
{
    public class Role
    {
        public Guild Guild { get; internal set; }
        public Discord Discord { get { return Guild?.Discord; } }

        public string MentionTag { get { return "<@&" + this.ID + ">"; } }

        [JsonProperty("id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong ID { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("hoist")]
        public bool Hoist { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("permissions")]
        public Permission Permission { get; set; }

        [JsonProperty("managed")]
        public bool Managed { get; set; }

        [JsonProperty("mentionable")]
        public bool Mentionable { get; set; }



        /// <summary>
        /// Modify a guild role. Requires the 'MANAGE_ROLES' permission.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public void ApplyModifications()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload<Role>(new Rest.Payloads.ModifyGuildRole(this.Guild, this));
            Guild.UpdateRole(this);
        }

        /// <summary>
        /// Delete a guild role. Requires the 'MANAGE_ROLES' permission. 
        /// </summary>
        public void Delete()
        {
            if (Discord == null) throw new DiscordMissingException();
            Discord.Rest.SendPayload(new Rest.Payloads.DeleteGuildRole(this));
        }
    }
}
