using Thylacine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Event
{
    public delegate void PresenceEvent(object sender, PresenceEventArgs args);
    public class PresenceEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public GuildMember Member { get; }
        public Presence Presence { get; }

        internal PresenceEventArgs(Guild g, GuildMember m, Presence p)
        {
            this.Guild = g;
            this.Member = m;
            this.Presence = p;
        }
    }

    public delegate void TypingEvent(object sender, TypingEventArgs args);
    public class TypingEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public Channel Channel { get; }
        public User User { get; }
        public DateTime StartTime { get; }

        internal TypingEventArgs(Guild g, Channel c, User u, DateTime t)
        {
            this.Guild = g;
            this.Channel = c;
            this.User = u;
            this.StartTime = t;
        }
    }
}
