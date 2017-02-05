using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Gateway
{
    /// <summary>
    /// The arguments of a Dispatch event.
    /// </summary>
    public class DispatchEventArgs : EventArgs { public string Type { get; } public JToken Payload { get; } public DispatchEventArgs(string type, JToken payload) { this.Type = type; this.Payload = payload; } }

    /// <summary>
    /// The event that is called when a Dispatch is received by a Gateway object.
    /// </summary>
    /// <param name="sender">The gateway that sent the event</param>
    /// <param name="args">The arguments of the dispatch. This includes a JSON Payload.</param>
    public delegate void DispatchEvent(object sender, DispatchEventArgs args);


    /// <summary>
    /// The base Gateway class. This object connects to Discord's websockets to recieve events.
    /// </summary>
    public abstract class IGateway
    {
        /// <summary>
        /// The current connection endpoint to Discord
        /// </summary>
        public GatewayEndpoint Endpoint { get; }

        /// <summary>
        /// Is the gateway currently connected?
        /// </summary>
        public bool Connected { get; protected set; } = false;

        /// <summary>
        /// Fired when a Dispatch is received from the websocket
        /// </summary>
        public event DispatchEvent OnDispatchEvent;

        /// <summary>
        /// Connects to the Discords websocket
        /// </summary>
        public abstract void Connect();

        /// <summary>
        /// The current authorization token of the bot.
        /// </summary>
        protected string token;

        /// <summary>
        /// Base constructor of the gateway object. It assigns the endpoint and the token.
        /// </summary>
        /// <param name="endpoint">Endpoint of the Discord websocket</param>
        /// <param name="token">Authorization token of the bot</param>
        public IGateway(GatewayEndpoint endpoint, string token) { this.Endpoint = endpoint; this.token = token; }

        /// <summary>
        /// Invokes the Dispatch event.
        /// </summary>
        /// <param name="sender">The sender of the dispatch event</param>
        /// <param name="args">The arguments of the dispatch event</param>
        protected void InvokeDispatch(object sender, DispatchEventArgs args)
        {
            OnDispatchEvent?.Invoke(sender, args);
        }
    }
}
