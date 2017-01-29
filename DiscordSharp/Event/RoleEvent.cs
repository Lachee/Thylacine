using DiscordSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Event
{
    public delegate void RoleEvent(object sender, RoleEventArgs args);
    public class RoleEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public Role Role { get; }
        internal RoleEventArgs(Guild g, Role r) { this.Guild = g; this.Role = r; }
    }
}
