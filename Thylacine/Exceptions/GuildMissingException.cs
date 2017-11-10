using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Exceptions
{
    public class GuildMissingException : Exception
    {
        internal GuildMissingException() : this("Cannot perform action as this object does not have a Guild associated with it") { }
        internal GuildMissingException(string message) : base(message) { }
    }
}
