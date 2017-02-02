using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Models;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    class GetGuildPruneCount : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/guilds/{GuildID}/prune";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }

        [JsonProperty("days")]
        public int Days { get; set; }

        internal GetGuildPruneCount() { }
        internal GetGuildPruneCount(Guild guild, int days)
        {
            this.GuildID = guild.ID;
            this.Days = days;
        }
    }
}
