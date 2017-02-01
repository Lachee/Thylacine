using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Models;

namespace Thylacine.Helper
{
    public class MessageBuilder
    {
        protected StringBuilder _stringBuilder;

        public string CurrentString => _stringBuilder.ToString();
        public int Length => _stringBuilder.Length;

        #region Constructors
        public MessageBuilder()
        {
            _stringBuilder = new StringBuilder(2000);
        }
        #endregion

        #region Appending
        public MessageBuilder Append(string s)
        {
            _stringBuilder.Append(s);
            return this;
        }
        public MessageBuilder Append(char c)
        {
            _stringBuilder.Append(c);
            return this;
        }
        public MessageBuilder Append(object o)
        {
            _stringBuilder.Append(o);
            return this;
        }
        #endregion

        public MessageBuilder Append(Channel o)
        {
            _stringBuilder.Append(o.MentionTag);
            return this;
        }
        public MessageBuilder Append(User u)
        {
            _stringBuilder.Append(u.MentionTag);
            return this;
        }
        public MessageBuilder Append(Role r)
        {
            _stringBuilder.Append(r.MentionTag);
            return this;
        }
        public MessageBuilder Append(Guild g)
        {
            _stringBuilder.Append(g.Name);
            return this;
        }
        public MessageBuilder Append(Emoji emoji)
        {
            _stringBuilder.Append(emoji.MentionTag);
            return this;
        }
        public MessageBuilder Append(Message message)
        {
            _stringBuilder.Append(message.Content);
            return this;
        }

        public static MessageBuilder operator +(MessageBuilder sb, string s) => sb.Append(s);
        public static MessageBuilder operator +(MessageBuilder sb, char c) => sb.Append(c);
        public static MessageBuilder operator +(MessageBuilder sb, object o) => sb.Append(o);

        public static implicit operator string(MessageBuilder sb) => sb.CurrentString;
        public override string ToString() => CurrentString;
        public string ToString(int startIndex, int length) => _stringBuilder.ToString(startIndex, length);
    }

}
