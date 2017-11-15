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
    class BeginGuildPrune : IRestPayload
    {
        Method IRestPayload.Method => Method.POST;
        string IRestPayload.Request => $"/guilds/{GuildID}/prune";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong GuildID { get; set; }

        [JsonProperty("days")]
        public int Days { get; set; }

        internal BeginGuildPrune() { }
        internal BeginGuildPrune(Guild guild, int days)
        {
            this.GuildID = guild.ID;
            this.Days = days;
        }
    }
}
