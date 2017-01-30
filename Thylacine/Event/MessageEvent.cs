using Thylacine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Event
{
    public delegate void MessageEvent(object sender, MessageEventArgs args);
    public class MessageEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public Message Message { get; }

        internal MessageEventArgs(Message msg, Guild guild)
        {
            this.Message = msg;
            this.Guild = guild;
        }
    }

    public delegate void MessageDeleteEvent(object sender, MessageDeleteEventArgs args);
    public class MessageDeleteEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public ulong MessageID { get; }
        public ulong ChannelID { get; }

        internal MessageDeleteEventArgs(ulong msg, ulong channel, Guild guild)
        {
            this.MessageID = msg;
            this.ChannelID = channel;
            this.Guild = guild;
        }
    }

    public delegate void MessageBulkDeleteEvent(object sender, MessageBulkDeleteEventArgs args);
    public class MessageBulkDeleteEventArgs : EventArgs
    {
        public Guild Guild { get; }
        public ulong[] MessageIDs { get; }
        public ulong ChannelID { get; }

        internal MessageBulkDeleteEventArgs(ulong[] msg, ulong channel, Guild guild)
        {
            this.MessageIDs = msg;
            this.ChannelID = channel;
            this.Guild = guild;
        }
    }
}