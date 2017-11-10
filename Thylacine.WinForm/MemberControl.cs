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

			isDeafen.Checked = Member.VoiceState.Deaf;
			isMute.Checked = Member.VoiceState.Mute;
			isSelfDeafen.Checked = Member.VoiceState.SelfDeath;
			isSelfMute.Checked = Member.VoiceState.SelfMute;
		}

		private void btnNicknameApply_Click(object sender, EventArgs e)
		{
			Member.SetNickname(textbox_nickname.Text);
		}

		private void onMuteStateChange(object sender, EventArgs e) => ApplyModifications();
		private void voicelist_change(object sender, EventArgs e) => ApplyModifications();

		void ApplyModifications()
		{
			Member.ApplyModifications(new GuildMemberModification()
			{
				Deaf = isDeafen.Checked,
				Mute = isMute.Checked,
				ChannelID = voicelist.Selected.ID
			});
		}

		private void btnInspect_Click(object sender, EventArgs e)
		{
			MessageBox.Show("TODO: Make this show a property editor of everything to do with the member.");
		}

	}
}
