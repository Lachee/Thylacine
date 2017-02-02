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
    class ModifyGuild : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/guilds/{GuildID}";
        object IRestPayload.Payload => this.Modification;

        public ulong GuildID { get; set; }
        public GuildModification Modification { get; set; }
        
        internal ModifyGuild() {}
        internal ModifyGuild(Guild guild, GuildModification mod) { GuildID = guild.ID; Modification = mod; }
    }
}
