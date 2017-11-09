using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Serializers;
using RestSharp.Authenticators;
using Thylacine.Rest.Authenticator;
using Thylacine.Rest.Payloads;
using Thylacine.Models;
using RestSharp.Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Thylacine.Exceptions;

namespace Thylacine.Rest
{
    /// <summary>
    /// A RestSharp client to connect to the Discord API with. Use this to perform API calls and actions.
    /// </summary>
    public class RestSharpClient : IRestClient
    {
        private RestSharp.RestClient client;

        /// <summary>
        /// Creates a new instance of the RestSharp Client
        /// </summary>
        /// <param name="token">Token for Discord</param>
        public RestSharpClient(string token) : base(token)
        {
            this.client = new RestSharp.RestClient("https://discordapp.com/api");
            this.client.Authenticator = new DiscordAuthenticator(base.token);
        }
        
        protected override async Task<IRestResponse> Send(string resource, Method method, object payload)
        {
            RestRequest request = new RestRequest(resource, method);
			request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.AddJsonBody(payload);

			var response = await client.ExecuteTaskAsync(request);
            switch(response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.Created:
                case System.Net.HttpStatusCode.NoContent:
                    return response;

                case System.Net.HttpStatusCode.Forbidden:
                case System.Net.HttpStatusCode.BadRequest:
                    DiscordError err = JsonConvert.DeserializeObject<DiscordError>(response.Content);
                    throw new DiscordRestException(err.Code, err.Message);

                default:
                    throw new Exception("A HTTP status error has occured while sending a REST call to discord. Status Code: " + (int)response.StatusCode);
            }
        }

        
        private struct DiscordError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}
