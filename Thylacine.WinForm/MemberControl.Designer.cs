namespace Thylacine.WinForm
{
	partial class MemberControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label_username = new System.Windows.Forms.Label();
			this.textbox_username = new System.Windows.Forms.TextBox();
			this.label_nickname = new System.Windows.Forms.Label();
			this.textbox_nickname = new System.Windows.Forms.TextBox();
			this.btnNicknameApply = new System.Windows.Forms.Button();
			this.isMute = new System.Windows.Forms.CheckBox();
			this.isDeafen = new System.Windows.Forms.CheckBox();
			this.groupVoice = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.isSelfMute = new System.Windows.Forms.CheckBox();
			this.label_channel = new System.Windows.Forms.Label();
			this.voicelist = new Thylacine.WinForm.ChannelList();
			this.isSelfDeafen = new System.Windows.Forms.CheckBox();
			this.buttonApplyVoice = new System.Windows.Forms.Button();
			this.btnInspect = new System.Windows.Forms.Button();
			this.groupInformation = new System.Windows.Forms.GroupBox();
			this.textbox_mention = new System.Windows.Forms.TextBox();
			this.textbox_id = new System.Windows.Forms.TextBox();
			this.label_presence = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label_id = new System.Windows.Forms.Label();
			this.groupVoice.SuspendLayout();
			this.groupInformation.SuspendLayout();
			this.SuspendLayout();
			// 
			// label_username
			// 
			this.label_username.AutoSize = true;
			this.label_username.Location = new System.Drawing.Point(6, 45);
			this.label_username.Name = "label_username";
			this.label_username.Size = new System.Drawing.Size(58, 13);
			this.label_username.TabIndex = 2;
			this.label_username.Text = "Username:";
			// 
			// textbox_username
			// 
			this.textbox_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox_username.Location = new System.Drawing.Point(64, 42);
			this.textbox_username.Name = "textbox_username";
			this.textbox_username.ReadOnly = true;
			this.textbox_username.Size = new System.Drawing.Size(186, 20);
			this.textbox_username.TabIndex = 3;
			// 
			// label_nickname
			// 
			this.label_nickname.AutoSize = true;
			this.label_nickname.Location = new System.Drawing.Point(6, 71);
			this.label_nickname.Name = "label_nickname";
			this.label_nickname.Size = new System.Drawing.Size(61, 13);
			this.label_nickname.TabIndex = 2;
			this.label_nickname.Text = "Nickname: ";
			// 
			// textbox_nickname
			// 
			this.textbox_nickname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox_nickname.Location = new System.Drawing.Point(64, 68);
			this.textbox_nickname.Name = "textbox_nickname";
			this.textbox_nickname.Size = new System.Drawing.Size(186, 20);
			this.textbox_nickname.TabIndex = 3;
			// 
			// btnNicknameApply
			// 
			this.btnNicknameApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNicknameApply.Location = new System.Drawing.Point(253, 66);
			this.btnNicknameApply.Name = "btnNicknameApply";
			this.btnNicknameApply.Size = new System.Drawing.Size(50, 23);
			this.btnNicknameApply.TabIndex = 4;
			this.btnNicknameApply.Text = "Apply";
			this.btnNicknameApply.UseVisualStyleBackColor = true;
			this.btnNicknameApply.Click += new System.EventHandler(this.btnNicknameApply_Click);
			// 
			// isMute
			// 
			this.isMute.AutoSize = true;
			this.isMute.Location = new System.Drawing.Point(60, 40);
			this.isMute.Name = "isMute";
			this.isMute.Size = new System.Drawing.Size(50, 17);
			this.isMute.TabIndex = 5;
			this.isMute.Text = "Mute";
			this.isMute.UseVisualStyleBackColor = true;
			// 
			// isDeafen
			// 
			this.isDeafen.AutoSize = true;
			this.isDeafen.Location = new System.Drawing.Point(60, 63);
			this.isDeafen.Name = "isDeafen";
			this.isDeafen.Size = new System.Drawing.Size(61, 17);
			this.isDeafen.TabIndex = 5;
			this.isDeafen.Text = "Deafen";
			this.isDeafen.UseVisualStyleBackColor = true;
			// 
			// groupVoice
			// 
			this.groupVoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupVoice.Controls.Add(this.label1);
			this.groupVoice.Controls.Add(this.isSelfMute);
			this.groupVoice.Controls.Add(this.label_channel);
			this.groupVoice.Controls.Add(this.isMute);
			this.groupVoice.Controls.Add(this.voicelist);
			this.groupVoice.Controls.Add(this.isSelfDeafen);
			this.groupVoice.Controls.Add(this.buttonApplyVoice);
			this.groupVoice.Controls.Add(this.isDeafen);
			this.groupVoice.Location = new System.Drawing.Point(6, 146);
			this.groupVoice.Name = "groupVoice";
			this.groupVoice.Size = new System.Drawing.Size(309, 88);
			this.groupVoice.TabIndex = 7;
			this.groupVoice.TabStop = false;
			this.groupVoice.Text = "Voice";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "State:";
			// 
			// isSelfMute
			// 
			this.isSelfMute.AutoSize = true;
			this.isSelfMute.Enabled = false;
			this.isSelfMute.Location = new System.Drawing.Point(127, 40);
			this.isSelfMute.Name = "isSelfMute";
			this.isSelfMute.Size = new System.Drawing.Size(71, 17);
			this.isSelfMute.TabIndex = 5;
			this.isSelfMute.Text = "Self Mute";
			this.isSelfMute.UseVisualStyleBackColor = true;
			// 
			// label_channel
			// 
			this.label_channel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label_channel.AutoSize = true;
			this.label_channel.Location = new System.Drawing.Point(6, 16);
			this.label_channel.Name = "label_channel";
			this.label_channel.Size = new System.Drawing.Size(49, 13);
			this.label_channel.TabIndex = 7;
			this.label_channel.Text = "Channel:";
			// 
			// voicelist
			// 
			this.voicelist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.voicelist.BackColor = System.Drawing.Color.Transparent;
			this.voicelist.Guild = null;
			this.voicelist.Location = new System.Drawing.Point(60, 14);
			this.voicelist.MaximumSize = new System.Drawing.Size(10000, 1000);
			this.voicelist.MinimumSize = new System.Drawing.Size(21, 21);
			this.voicelist.Name = "voicelist";
			this.voicelist.PrefixTextChannels = "#";
			this.voicelist.PrefixVoiceChannels = "🔊";
			this.voicelist.Selected = null;
			this.voicelist.ShowTextChannels = false;
			this.voicelist.ShowVoiceChannels = true;
			this.voicelist.Size = new System.Drawing.Size(243, 27);
			this.voicelist.TabIndex = 6;
			// 
			// isSelfDeafen
			// 
			this.isSelfDeafen.AutoSize = true;
			this.isSelfDeafen.Enabled = false;
			this.isSelfDeafen.Location = new System.Drawing.Point(127, 63);
			this.isSelfDeafen.Name = "isSelfDeafen";
			this.isSelfDeafen.Size = new System.Drawing.Size(82, 17);
			this.isSelfDeafen.TabIndex = 5;
			this.isSelfDeafen.Text = "Self Deafen";
			this.isSelfDeafen.UseVisualStyleBackColor = true;
			// 
			// buttonApplyVoice
			// 
			this.buttonApplyVoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonApplyVoice.Location = new System.Drawing.Point(253, 59);
			this.buttonApplyVoice.Name = "buttonApplyVoice";
			this.buttonApplyVoice.Size = new System.Drawing.Size(50, 23);
			this.buttonApplyVoice.TabIndex = 4;
			this.buttonApplyVoice.Text = "Apply";
			this.buttonApplyVoice.UseVisualStyleBackColor = true;
			this.buttonApplyVoice.Click += new System.EventHandler(this.buttonApplyVoice_Click);
			// 
			// btnInspect
			// 
			this.btnInspect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInspect.Location = new System.Drawing.Point(281, 16);
			this.btnInspect.Name = "btnInspect";
			this.btnInspect.Size = new System.Drawing.Size(22, 23);
			this.btnInspect.TabIndex = 8;
			this.btnInspect.Text = "?";
			this.btnInspect.UseVisualStyleBackColor = true;
			this.btnInspect.Click += new System.EventHandler(this.btnInspect_Click);
			// 
			// groupInformation
			// 
			this.groupInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupInformation.Controls.Add(this.textbox_mention);
			this.groupInformation.Controls.Add(this.btnInspect);
			this.groupInformation.Controls.Add(this.textbox_id);
			this.groupInformation.Controls.Add(this.textbox_username);
			this.groupInformation.Controls.Add(this.label_presence);
			this.groupInformation.Controls.Add(this.label2);
			this.groupInformation.Controls.Add(this.label_id);
			this.groupInformation.Controls.Add(this.label_username);
			this.groupInformation.Controls.Add(this.textbox_nickname);
			this.groupInformation.Controls.Add(this.btnNicknameApply);
			this.groupInformation.Controls.Add(this.label_nickname);
			this.groupInformation.Location = new System.Drawing.Point(6, 3);
			this.groupInformation.Name = "groupInformation";
			this.groupInformation.Size = new System.Drawing.Size(309, 137);
			this.groupInformation.TabIndex = 9;
			this.groupInformation.TabStop = false;
			this.groupInformation.Text = "Information";
			this.groupInformation.Enter += new System.EventHandler(this.groupInformation_Enter);
			// 
			// textbox_mention
			// 
			this.textbox_mention.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox_mention.Location = new System.Drawing.Point(64, 94);
			this.textbox_mention.Name = "textbox_mention";
			this.textbox_mention.ReadOnly = true;
			this.textbox_mention.Size = new System.Drawing.Size(186, 20);
			this.textbox_mention.TabIndex = 3;
			// 
			// textbox_id
			// 
			this.textbox_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox_id.Location = new System.Drawing.Point(64, 16);
			this.textbox_id.Name = "textbox_id";
			this.textbox_id.ReadOnly = true;
			this.textbox_id.Size = new System.Drawing.Size(186, 20);
			this.textbox_id.TabIndex = 3;
			// 
			// label_presence
			// 
			this.label_presence.AutoSize = true;
			this.label_presence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_presence.Location = new System.Drawing.Point(61, 117);
			this.label_presence.Name = "label_presence";
			this.label_presence.Size = new System.Drawing.Size(16, 13);
			this.label_presence.TabIndex = 2;
			this.label_presence.Text = "...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 97);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Mention:";
			// 
			// label_id
			// 
			this.label_id.AutoSize = true;
			this.label_id.Location = new System.Drawing.Point(38, 18);
			this.label_id.Name = "label_id";
			this.label_id.Size = new System.Drawing.Size(21, 13);
			this.label_id.TabIndex = 2;
			this.label_id.Text = "ID:";
			// 
			// MemberControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupInformation);
			this.Controls.Add(this.groupVoice);
			this.Name = "MemberControl";
			this.Size = new System.Drawing.Size(322, 240);
			this.Load += new System.EventHandler(this.MemberControl_Load);
			this.groupVoice.ResumeLayout(false);
			this.groupVoice.PerformLayout();
			this.groupInformation.ResumeLayout(false);
			this.groupInformation.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label_username;
		private System.Windows.Forms.TextBox textbox_username;
		private System.Windows.Forms.Label label_nickname;
		private System.Windows.Forms.TextBox textbox_nickname;
		private System.Windows.Forms.Button btnNicknameApply;
		private System.Windows.Forms.CheckBox isMute;
		private System.Windows.Forms.CheckBox isDeafen;
		private ChannelList voicelist;
		private System.Windows.Forms.GroupBox groupVoice;
		private System.Windows.Forms.Label label_channel;
		private System.Windows.Forms.CheckBox isSelfMute;
		private System.Windows.Forms.CheckBox isSelfDeafen;
		private System.Windows.Forms.Button btnInspect;
		private System.Windows.Forms.Button buttonApplyVoice;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupInformation;
		private System.Windows.Forms.TextBox textbox_mention;
		private System.Windows.Forms.TextBox textbox_id;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label_id;
		private System.Windows.Forms.Label label_presence;
	}
}
