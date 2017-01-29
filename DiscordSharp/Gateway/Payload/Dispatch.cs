using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Gateway.Payload
{
    [JsonObject(MemberSerialization.OptIn)]
    interface IPayload
    {
        OpCode OpCode { get; }
        uint? Sequence { get; }
        string Event { get; }
        object Payload { get; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class Dispatch
    {
        [JsonProperty("op")]
        public OpCode OpCode { get; set; }

        [JsonProperty("s")]
        public uint? Sequence { get; set; }

        [JsonProperty("t")]
        public string Event { get; set; }

        [JsonProperty("d")]
        public object Payload { get; set; }

        public Dispatch() { }
        public Dispatch(IPayload payload)
        {
            this.OpCode = payload.OpCode;
            this.Sequence = payload.Sequence;
            this.Event = payload.Event;
            this.Payload = payload.Payload;
        }
    }
}
