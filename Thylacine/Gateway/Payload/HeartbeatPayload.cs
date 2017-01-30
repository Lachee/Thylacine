using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Gateway.Payload
{
    class HeartbeatPayload : IPayload
    {
        OpCode IPayload.OpCode => OpCode.Heartbeat;
        string IPayload.Event => null;
        object IPayload.Payload => null;
        uint? IPayload.Sequence => null;
    }
}
