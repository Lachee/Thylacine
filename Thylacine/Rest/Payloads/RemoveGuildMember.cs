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
    class RemoveGuildMember : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/guilds/{GuildID}/members/{UserID}";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }
        public ulong UserID { get; set; }

        internal RemoveGuildMember() { }
        internal RemoveGuildMember(GuildMember member)
        {
            this.GuildID = member.Guild.ID;
            this.UserID = member.User.ID;
        }
    }
}
