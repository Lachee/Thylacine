using DiscordSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Event
{
    public delegate void UserEvent(object sender, UserEventArgs args);
    public class UserEventArgs : EventArgs
    {
        public User User { get; }
        internal UserEventArgs(User u) { this.User = u; }
    }
}
