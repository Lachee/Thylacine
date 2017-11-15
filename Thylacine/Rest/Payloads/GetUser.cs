
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
    class GetUser : IRestPayload
    {
        Method IRestPayload.Method => Method.GET;
        string IRestPayload.Request => $"/users/{UserID}";
		object IRestPayload.Payload => null;
		QueryParam[] IRestPayload.Params => null;

		public ulong UserID { get; set; }
    }
}
