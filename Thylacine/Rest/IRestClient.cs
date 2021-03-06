﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Rest.Payloads;

namespace Thylacine.Rest
{
    /// <summary>
    /// A abstract class for Rest Clients. This is the basis of all Rest client objects and is prime functionallity is to connect to the Discord API.
    /// </summary>
    public abstract class IRestClient
    {
        /// <summary>
        /// Authorization token used by discord. Readonly and cannot be changed.
        /// </summary>
        protected readonly string token;

        /// <summary>
        /// Creates a new instance of the client with specified token.
        /// </summary>
        /// <param name="token">The authorization token used by discord. </param>
        public IRestClient(string token) { this.token = token; }

        /// <summary>
        /// Sends a rest resource.
        /// </summary>
        /// <param name="resource">A endpoint to Discord's API</param>
        /// <param name="method">The HTTP method to use.</param>
        /// <param name="payload">The json serilizable payload.</param>
        /// <returns>Returns a response object that contains the content and any errors.</returns>
        protected abstract IRestResponse Send(string resource, Method method, object payload);

        /// <summary>
        /// Sends a IRestPayload to discord.
        /// </summary>
        /// <param name="payload">The payload to send to discord.</param>
        public virtual void SendPayload(IRestPayload payload)
        {
            Send(payload.Request, payload.Method, payload.Payload);
        }

        /// <summary>
        /// Sends a IRestPayload to discord.
        /// </summary>
        /// <typeparam name="T">The return type from Discord that is expected</typeparam>
        /// <param name="payload">The payload to send to discord.</param>
        /// <returns>Returns a new instance of target type.</returns>
        public virtual T SendPayload<T>(IRestPayload payload) where T : new()
        {
            IRestResponse response = Send(payload.Request, payload.Method, payload.Payload);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
