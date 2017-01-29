using DiscordSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Event
{
    public delegate void GuildEvent(object sender, GuildEventArgs args);
    public class GuildEventArgs : EventArgs
    {
        public Guild Guild { get; }
        internal GuildEventArgs(Guild g) { this.Guild = g; }
    }

    public delegate void GuildBanEvent(object sender, GuildBanEventArgs args);
    public class GuildBanEventArgs
    {
        public Guild Guild { get; }
        public User User { get; }
        internal GuildBanEventArgs(Guild g, User u) { this.Guild = g;  this.User = u; }
    }
}
