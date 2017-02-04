using Newtonsoft.Json;
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
    /// The base class of a REST Client
    /// </summary>
    public abstract class IRestClient
    {
        /// <summary>
        /// Sends a rest resource.
        /// </summary>
        /// <param name="resource">Endpoint</param>
        /// <param name="method">HTTP Method</param>
        /// <param name="payload">Payload</param>
        /// <returns></returns>
        protected abstract IRestResponse Send(string resource, Method method, object payload);

        /// <summary>
        /// Sends a IRestPayload to discord.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual void SendPayload(IRestPayload payload)
        {
            Send(payload.Request, payload.Method, payload.Payload);
        }

        /// <summary>
        /// Sends a IRestPayload to discord.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payload"></param>
        /// <returns></returns>
        public virtual T SendPayload<T>(IRestPayload payload) where T : new()
        {
            IRestResponse response = Send(payload.Request, payload.Method, payload.Payload);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
