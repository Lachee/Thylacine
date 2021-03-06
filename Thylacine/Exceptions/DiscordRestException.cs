﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Exceptions
{
    public class DiscordRestException : Exception
    {
        public int Code { get; }
        internal DiscordRestException(int code, string message) : base(message)
        {
            this.Code = code;
        }
    }

    /*
    public class DiscordPermissionException : Exception
    {
        public int Code { get; }
        internal DiscordPermissionException(int code, string message) : base(message)
        {
            this.Code = code;
        }
    }
    */
}
