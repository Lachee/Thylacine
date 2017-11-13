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
		#region Properties
		private GuildMember _member;
		public GuildMember Member
		{
			get { return _member; }
			set
			{
				this._member = value;
				SyncFields();
			}
		}

		/// <summary>
		/// Does the Member Control listen to member update events and live updates its contents?
		/// </summary>
		public bool RegisterGuildEvents { get; set; }
		private Guild _guild = null;
		#endregion

		#region Initialization
		public MemberControl()
		{
			InitializeComponent();
		}

		private void MemberControl_Load(object sender, EventArgs e)
		{
		}
		#endregion


		#region Control Events
		private void btnNicknameApply_Click(object sender, EventArgs e)
		{
			Member.SetNickname(textbox_nickname.Text);
		}

		private void buttonApplyVoice_Click(object sender, EventArgs e)
		{
			Member.ApplyModifications(new GuildMemberModification()
			{
				Deaf = isDeafen.Checked,
				Mute = isMute.Checked,
				ChannelID = voicelist.Selected?.ID
			});
		}

		private void btnInspect_Click(object sender, EventArgs e)
		{
			MessageBox.Show("TODO: Make this show a property editor of everything to do with the member.");
		}


		private void OnMemberUpdate(object sender, Event.GuildMemberEventArgs args) => this.Invoke(new MethodInvoker(delegate () { SyncFields(); }));
		private void OnPresenceUpdate(object sender, Event.PresenceEventArgs args) => this.Invoke(new MethodInvoker(delegate () { SyncFields(); }));
		private void OnVoiceStateUpdate(object sender, Event.VoiceStateArgs args) => this.Invoke(new MethodInvoker(delegate () { SyncFields(); }));


		#endregion

		/// <summary>
		/// Syncronises the fields with the stored member.
		/// </summary>
		public void SyncFields()
		{
			UpdateButtonStates();

			//Register events if needed
			if (Member?.Guild != _guild && RegisterGuildEvents)
			{
				//Unsub from the old guild
				UnregisterEvents();

				//Update the guild and update the events
				_guild = Member?.Guild;
				RegisterEvents();
			}

			if (Member == null)
			{
				ClearFields();
				return;
			}

			_guild = Member.Guild;

			textbox_username.Text = Member.User.Username;
			textbox_nickname.Text = Member.Nickname;
			textbox_id.Text = Member.ID.ToString();
			textbox_mention.Text = Member.MentionTag;

			label_presence.Text = Member.Presence != null ?
				Member.Presence.Status.ToString() + Member.Presence.FormatGame(" - Playing", " - Streaming") :
				"N/A";

			this.voicelist.Guild = Member.Guild;
			voicelist.Selected = Member.VoiceState.Channel;

			isDeafen.Checked = Member.VoiceState.Deaf;
			isMute.Checked = Member.VoiceState.Mute;
			isSelfDeafen.Checked = Member.VoiceState.SelfDeath;
			isSelfMute.Checked = Member.VoiceState.SelfMute;

			this.Update();
		}
		
		private void UpdateButtonStates()
		{
			groupInformation.Enabled = Member != null;
			groupVoice.Enabled = Member != null && Member.VoiceState.Channel != null;
		}

		/// <summary>
		/// Clear all the fields to their default value.
		/// </summary>
		public void ClearFields()
		{
			UpdateButtonStates();

			textbox_username.Text = "";
			textbox_nickname.Text = "";

			voicelist.Selected = null;

			isDeafen.Checked = false;
			isMute.Checked = false;
			isSelfDeafen.Checked = false;
			isSelfMute.Checked = false;
		}


		private void RegisterEvents()
		{
			if (_guild == null) return;
			_guild.OnPresenceUpdate += OnPresenceUpdate;
			_guild.OnMemberUpdate += OnMemberUpdate;
			_guild.OnVoiceStateUpdate += OnVoiceStateUpdate;
		}
		private void UnregisterEvents()
		{
			if (_guild == null) return;
			_guild.OnPresenceUpdate -= OnPresenceUpdate;
			_guild.OnMemberUpdate -= OnMemberUpdate;
			_guild.OnVoiceStateUpdate -= OnVoiceStateUpdate;
		}

		private void groupInformation_Enter(object sender, EventArgs e)
		{

		}
	}
}
