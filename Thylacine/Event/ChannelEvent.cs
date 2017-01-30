using Thylacine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Event
{
    public delegate void DMCreateEvent(object sender, DMCreateEventArgs args);
    public class DMCreateEventArgs : EventArgs
    {
        public DMChannel Channel { get; }
        internal DMCreateEventArgs(DMChannel g) { this.Channel = g; }
    }

    public delegate void ChannelEvent(object sender, ChannelEventArgs args);
    public class ChannelEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public Channel Channel { get; }
        internal ChannelEventArgs(Channel channel, Guild guild) { this.Channel = channel; this.Guild = guild; }
    }
}
