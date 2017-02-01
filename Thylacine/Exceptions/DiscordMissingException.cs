using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Exceptions
{
    public class DiscordMissingException : Exception
    {
        internal DiscordMissingException() : this("Cannot perform action as this object does not have a Discord client associated with it") { }
        internal DiscordMissingException(string message) : base(message) { }
    }
}
