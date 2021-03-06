﻿using Thylacine.Models;
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
    public class DeleteGuild : IRestPayload
    {
        Method IRestPayload.Method => Method.DELETE;
        string IRestPayload.Request => $"/guilds/{GuildID}";
        object IRestPayload.Payload => this;

        public ulong GuildID { get; set; }

        public DeleteGuild() { }
        public DeleteGuild(Guild guild) { this.GuildID = guild.ID; }
    }
}
