using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thylacine.Models;

namespace Thylacine.WinForm
{
	public partial class MemberControl : UserControl
	{
		public GuildMember Member => _member;
		private GuildMember _member;

		public MemberControl()
		{
			InitializeComponent();
			_member = null;

			SyncFields();
		}

		public void SetMember(GuildMember member)
		{
			this._member = member;
			SyncFields();
		}

		private void MemberControl_Load(object sender, EventArgs e)
		{
		}

		public void SyncFields()
		{
			if (Member == null) return;

			textbox_username.Text = Member.User.Username;
			textbox_nickname.Text = Member.Nickname;
			
			this.voicelist.Guild = Member.Guild;
			voicelist.Selected = Member.VoiceState.Channel;
		}

		private void btnNicknameApply_Click(object sender, EventArgs e)
		{
			Member.SetNickname(textbox_nickname.Text);
		}
	}
}
