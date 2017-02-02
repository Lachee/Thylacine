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
    class ModifyGuildMember : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/guilds/{GuildID}/members/{UserID}";
        object IRestPayload.Payload => this.Modification;

        public ulong GuildID { get; set; }
        public ulong UserID { get; set; }
        public GuildMemberModification Modification { get; set; }
        
        internal ModifyGuildMember() {}
        internal ModifyGuildMember(GuildMember member, GuildMemberModification mod) {
            GuildID = member.Guild.ID;
            UserID = member.User.ID;
            Modification = mod;
        }
    }
}
