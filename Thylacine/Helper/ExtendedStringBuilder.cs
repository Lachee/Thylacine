using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Helper
{
    //Source: http://codereview.stackexchange.com/questions/126089/an-extension-to-the-stringbuilder/126091
    public class ExtendedStringBuilder
    {
        protected StringBuilder _stringBuilder;

        public string CurrentString => _stringBuilder.ToString();
        public int Length => _stringBuilder.Length;

        #region Constructors
        public ExtendedStringBuilder()
        {
            _stringBuilder = new StringBuilder();
        }
        public ExtendedStringBuilder(int capacity)
        {
            _stringBuilder = new StringBuilder(capacity);
        }
        #endregion

        #region Appending
        public ExtendedStringBuilder Append(string s)
        {
            _stringBuilder.Append(s);
            return this;
        }
        public ExtendedStringBuilder Append(char c)
        {
            _stringBuilder.Append(c);
            return this;
        }
        public ExtendedStringBuilder Append(object o)
        {
            _stringBuilder.Append(o);
            return this;
        }
        #endregion

        public static ExtendedStringBuilder operator +(ExtendedStringBuilder sb, string s) => sb.Append(s);
        public static ExtendedStringBuilder operator +(ExtendedStringBuilder sb, char c) => sb.Append(c);
        public static ExtendedStringBuilder operator +(ExtendedStringBuilder sb, object o) => sb.Append(o);

        public static implicit operator string(ExtendedStringBuilder sb) => sb.CurrentString;
        public override string ToString() => CurrentString;
        public string ToString(int startIndex, int length) => _stringBuilder.ToString(startIndex, length);
    }

}
