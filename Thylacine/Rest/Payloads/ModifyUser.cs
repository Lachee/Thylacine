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
    class ModifyUser : IRestPayload
    {
        Method IRestPayload.Method => Method.PATCH;
        string IRestPayload.Request => $"/users/{UserID}";
        object IRestPayload.Payload => this;
		QueryParam[] IRestPayload.Params => null;

		public ulong UserID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }
        
    }
}
