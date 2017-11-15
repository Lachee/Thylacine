
using Thylacine.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    class GetChannelInvites : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/channels/{ChannelID}/invites";
        object IRestPayload.Payload => null;
		QueryParam[] IRestPayload.Params => null;

		public ulong ChannelID { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class GetGuildInvites : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/guilds/{GuildID}/invites";
        object IRestPayload.Payload => null;
		QueryParam[] IRestPayload.Params => null;

		public ulong GuildID { get; set; }

        internal GetGuildInvites() { }
        internal GetGuildInvites(Guild g) { GuildID = g.ID; }
    }
}
