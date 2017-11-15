﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Rest.Payloads
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IRestPayload
    {
        Method Method { get; }
        string Request { get; }
        object Payload { get; }
		QueryParam[] Params { get; }
    }

	public struct QueryParam
	{
		public string key;
		public string value;
		public bool isNull;

		public QueryParam(string key, string value)
		{
			this.key = key;
			this.value = value;
			this.isNull = value == null;
		}
		public QueryParam(string key, object value)
		{
			this.key = key;
			this.isNull = value == null;
			this.value = value.ToString();
		}
	}
}
