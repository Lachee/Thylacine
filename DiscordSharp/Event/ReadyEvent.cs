using DiscordSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Event
{
    public delegate void DiscordReadyEvent(object sender, DiscordReadyEventArgs args);
    public class DiscordReadyEventArgs : EventArgs
    {
        public User Bot { get; }
        public string Session { get; }

        internal DiscordReadyEventArgs(User user, string session)
        {
            this.Bot = user;
            this.Session = session;
        }
    }
}
