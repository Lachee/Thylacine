using Thylacine.Gateway.Payload;
using Thylacine.Models.Event;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using WebSocketSharp;

namespace Thylacine.Gateway
{
    public class GatewaySocket : IGateway
    {

        private WebSocket socket;

        private string session;
        private uint sequence;

        private Timer heartbeat;
        private int travelingbeats = 0;

        /// <summary>
        /// Creates a new WebSocketSharp gateway object to connect to discord.
        /// </summary>
        /// <param name="endpoint">Endpoint of the Discord websocket</param>
        /// <param name="token">Authorization token of the bot</param>
        public GatewaySocket(GatewayEndpoint endpoint, string token) : base(endpoint, token) { }

        public override void Connect()
        {
            //Setup the heartbeat
            this.heartbeat = new Timer();
            this.heartbeat.AutoReset = true;
            this.heartbeat.Elapsed += delegate (object sender, ElapsedEventArgs e) { SendHeartbeat();  };

            //Connect the socket
            socket = new WebSocket(base.Endpoint.Url + "/?encoding=json&v=5");
            socket.Compression = CompressionMethod.None;

            socket.OnClose += SocketOnClose;
            socket.OnMessage += SocketOnMessage;
            
            socket.Connect();
            base.Connected = true;
        }

        private void SocketOnMessage(object sender, MessageEventArgs e)
        {
            if (!e.IsText) return;
            //try
            //{
                //Convert to a Gateway Packet
                Dispatch dispatch = JsonConvert.DeserializeObject<Dispatch>(e.Data);

                //Get the sequence
                if (dispatch.Sequence.HasValue)
                    sequence = dispatch.Sequence.Value;

                //Prepare the payload
                JToken payload = dispatch.Payload as JToken;

                //Get the opcode
                var op = (OpCode?)dispatch.OpCode;
                switch(op)
                {
                    case OpCode.Dispatch:
                        
                        //Console.WriteLine("DISPATCH: " + dispatch.Event);
                        if (dispatch.Event == "READY")
                        {
                            ReadyEvent readyEvent = payload.ToObject<ReadyEvent>();
                            session = readyEvent.Session;
                        }

                        base.InvokeDispatch(this, new DispatchEventArgs(dispatch.Event, payload));
                        break;

                    case OpCode.Heartbeat:
                        break;

                    case OpCode.Reconnect:
                        Console.WriteLine("Reconnection");
                        break;

                    case OpCode.InvalidSession:
                        Console.WriteLine("Invalid Session!");
                        break;

                    case OpCode.Hello:
                        
                        //Set the heartbeat
                        var heartbeat = payload.ToObject<HelloPayload>();
                        SetHeartbeat(heartbeat.HeartbeatInterval);

                        //Authenticate with the server.
                        Authenticate();
                        break;

                    case OpCode.HeartbeatACK:
                        travelingbeats--;
                        break;
                        
                    default:
                        if (op != null)
                            Console.WriteLine($"Unhandled Opcode: {op}");
                        else
                            Console.WriteLine($"Received message with no opcode");
                        break;
                }

            //}catch(Exception err)
            //{
            //    Console.WriteLine("Exception occured while reciving a message: " + err.Message);
            //}

        }
        
        private void SocketOnClose(object sender, CloseEventArgs e)
        {
            base.Connected = false;
        }


        #region Heartbeat
        private void SetHeartbeat(int interval)
        {
            heartbeat.Stop();
            heartbeat.Interval = interval;
            heartbeat.Start();

            travelingbeats = 0;

            //Console.WriteLine("Heartbeat Set to " + interval);
        }
        private void SendHeartbeat()
        {
            //Console.WriteLine("<3 Heartbeat. ");
            travelingbeats++;
            SendDispatch(new Dispatch()
            {
                OpCode = OpCode.Heartbeat,
                Payload = sequence
            });
        }
        #endregion

        private void Authenticate()
        {
            //Prepare the payload
            IdentifyPayload payload = new IdentifyPayload()
            {
                Token = base.token,
                Compress = false,
                LargeThreshold = 150,
                Shard = new int[] { 0, 1 },
                Properties = new Dictionary<string, string>()
                {
                    ["$device"] = "DiscordSharp"
                }
            };

            //Sendit away
            //Console.WriteLine("Sending Authication");
            SendDispatch(new Dispatch(payload));
        }

        private void SendDispatch(Dispatch dispatch)
        {
            string json = JsonConvert.SerializeObject(dispatch, Formatting.None);
            socket.Send(json);
        }
    }
}
