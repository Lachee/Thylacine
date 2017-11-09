using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.WinForm.Models
{
	class GuildMember
	{
		protected Thylacine.Models.GuildMember _guildMember;
		public string Nickname { get { return _guildMember.Nickname; } set { _guildMember.SetNickname(value); } }
	}
}
