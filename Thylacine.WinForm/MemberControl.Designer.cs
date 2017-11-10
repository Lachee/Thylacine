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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label_channel = new System.Windows.Forms.Label();
			this.isSelfMute = new System.Windows.Forms.CheckBox();
			this.voicelist = new Thylacine.WinForm.ChannelList();
			this.isSelfDeafen = new System.Windows.Forms.CheckBox();
			this.btnInspect = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label_username
			// 
			this.label_username.AutoSize = true;
			this.label_username.Location = new System.Drawing.Point(3, 10);
			this.label_username.Name = "label_username";
			this.label_username.Size = new System.Drawing.Size(58, 13);
			this.label_username.TabIndex = 2;
			this.label_username.Text = "Username:";
			// 
			// textbox_username
			// 
			this.textbox_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox_username.Location = new System.Drawing.Point(70, 7);
			this.textbox_username.Name = "textbox_username";
			this.textbox_username.ReadOnly = true;
			this.textbox_username.Size = new System.Drawing.Size(185, 20);
			this.textbox_username.TabIndex = 3;
			// 
			// label_nickname
			// 
			this.label_nickname.AutoSize = true;
			this.label_nickname.Location = new System.Drawing.Point(3, 40);
			this.label_nickname.Name = "label_nickname";
			this.label_nickname.Size = new System.Drawing.Size(61, 13);
			this.label_nickname.TabIndex = 2;
			this.label_nickname.Text = "Nickname: ";
			// 
			// textbox_nickname
			// 
			this.textbox_nickname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox_nickname.Location = new System.Drawing.Point(70, 37);
			this.textbox_nickname.Name = "textbox_nickname";
			this.textbox_nickname.Size = new System.Drawing.Size(185, 20);
			this.textbox_nickname.TabIndex = 3;
			// 
			// btnNicknameApply
			// 
			this.btnNicknameApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNicknameApply.Location = new System.Drawing.Point(261, 35);
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
			this.isMute.Location = new System.Drawing.Point(10, 19);
			this.isMute.Name = "isMute";
			this.isMute.Size = new System.Drawing.Size(50, 17);
			this.isMute.TabIndex = 5;
			this.isMute.Text = "Mute";
			this.isMute.UseVisualStyleBackColor = true;
			this.isMute.CheckedChanged += new System.EventHandler(this.onMuteStateChange);
			// 
			// isDeafen
			// 
			this.isDeafen.AutoSize = true;
			this.isDeafen.Location = new System.Drawing.Point(66, 19);
			this.isDeafen.Name = "isDeafen";
			this.isDeafen.Size = new System.Drawing.Size(61, 17);
			this.isDeafen.TabIndex = 5;
			this.isDeafen.Text = "Deafen";
			this.isDeafen.UseVisualStyleBackColor = true;
			this.isDeafen.CheckedChanged += new System.EventHandler(this.onMuteStateChange);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label_channel);
			this.groupBox1.Controls.Add(this.isSelfMute);
			this.groupBox1.Controls.Add(this.isMute);
			this.groupBox1.Controls.Add(this.voicelist);
			this.groupBox1.Controls.Add(this.isSelfDeafen);
			this.groupBox1.Controls.Add(this.isDeafen);
			this.groupBox1.Location = new System.Drawing.Point(6, 59);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(308, 88);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Voice";
			// 
			// label_channel
			// 
			this.label_channel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label_channel.AutoSize = true;
			this.label_channel.Location = new System.Drawing.Point(7, 41);
			this.label_channel.Name = "label_channel";
			this.label_channel.Size = new System.Drawing.Size(49, 13);
			this.label_channel.TabIndex = 7;
			this.label_channel.Text = "Channel:";
			// 
			// isSelfMute
			// 
			this.isSelfMute.AutoSize = true;
			this.isSelfMute.Enabled = false;
			this.isSelfMute.Location = new System.Drawing.Point(133, 19);
			this.isSelfMute.Name = "isSelfMute";
			this.isSelfMute.Size = new System.Drawing.Size(71, 17);
			this.isSelfMute.TabIndex = 5;
			this.isSelfMute.Text = "Self Mute";
			this.isSelfMute.UseVisualStyleBackColor = true;
			// 
			// voicelist
			// 
			this.voicelist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.voicelist.Guild = null;
			this.voicelist.Location = new System.Drawing.Point(6, 60);
			this.voicelist.MaximumSize = new System.Drawing.Size(10000, 1000);
			this.voicelist.MinimumSize = new System.Drawing.Size(21, 21);
			this.voicelist.Name = "voicelist";
			this.voicelist.PrefixTextChannels = "#";
			this.voicelist.PrefixVoiceChannels = "🔊";
			this.voicelist.Selected = null;
			this.voicelist.ShowTextChannels = false;
			this.voicelist.ShowVoiceChannels = true;
			this.voicelist.Size = new System.Drawing.Size(295, 27);
			this.voicelist.TabIndex = 6;
			this.voicelist.SelectedIndexChanged += new System.EventHandler(this.voicelist_change);
			// 
			// isSelfDeafen
			// 
			this.isSelfDeafen.AutoSize = true;
			this.isSelfDeafen.Enabled = false;
			this.isSelfDeafen.Location = new System.Drawing.Point(210, 19);
			this.isSelfDeafen.Name = "isSelfDeafen";
			this.isSelfDeafen.Size = new System.Drawing.Size(82, 17);
			this.isSelfDeafen.TabIndex = 5;
			this.isSelfDeafen.Text = "Self Deafen";
			this.isSelfDeafen.UseVisualStyleBackColor = true;
			// 
			// btnInspect
			// 
			this.btnInspect.Location = new System.Drawing.Point(261, 7);
			this.btnInspect.Name = "btnInspect";
			this.btnInspect.Size = new System.Drawing.Size(50, 23);
			this.btnInspect.TabIndex = 8;
			this.btnInspect.Text = "?";
			this.btnInspect.UseVisualStyleBackColor = true;
			this.btnInspect.Click += new System.EventHandler(this.btnInspect_Click);
			// 
			// MemberControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnInspect);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnNicknameApply);
			this.Controls.Add(this.textbox_nickname);
			this.Controls.Add(this.textbox_username);
			this.Controls.Add(this.label_nickname);
			this.Controls.Add(this.label_username);
			this.Name = "MemberControl";
			this.Size = new System.Drawing.Size(321, 153);
			this.Load += new System.EventHandler(this.MemberControl_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label_channel;
		private System.Windows.Forms.CheckBox isSelfMute;
		private System.Windows.Forms.CheckBox isSelfDeafen;
		private System.Windows.Forms.Button btnInspect;
	}
}
