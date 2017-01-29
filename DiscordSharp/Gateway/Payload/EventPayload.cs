using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Gateway.Payload
{
    [JsonObject(MemberSerialization.OptIn)]
    class EventPayload : IPayload
    {
        OpCode IPayload.OpCode => OpCode.Dispatch;
        string IPayload.Event => this.Event;
        object IPayload.Payload => this.Payload;
        uint? IPayload.Sequence => null;

        public string Event { get; set; }
        public object Payload { get; set; }
    }
}
