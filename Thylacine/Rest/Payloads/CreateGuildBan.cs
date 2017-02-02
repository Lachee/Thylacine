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
    class CreateGuildBan : IRestPayload
    {
        Method IRestPayload.Method => Method.PUT;
        string IRestPayload.Request => $"/guilds/{GuildID}/bans/{UserID}";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }
        public ulong UserID { get; set; }

        internal CreateGuildBan() { }
        internal CreateGuildBan(GuildMember member)
        {
            this.GuildID = member.Guild.ID;
            this.UserID = member.User.ID;
        }
    }
}
