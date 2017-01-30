using Thylacine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Event
{
    public delegate void GuildMemberEvent(object sender, GuildMemberEventArgs args);
    public class GuildMemberEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public GuildMember Member { get; }

        internal GuildMemberEventArgs(Guild guild, GuildMember member)
        {
            this.Guild = guild;
            this.Member = member;
        }
    }
}
