using Thylacine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Event
{
    public delegate void VoiceStateEvent(object sender, VoiceStateArgs args);
    public class VoiceStateArgs : EventArgs
    {
		public Guild Guild { get; }
		public VoiceState State { get; }
		public GuildMember GuildMember { get; }
		internal VoiceStateArgs(Guild g, VoiceState s, GuildMember m) { this.Guild = g; this.State = s; this.GuildMember = m; }
    }
}
