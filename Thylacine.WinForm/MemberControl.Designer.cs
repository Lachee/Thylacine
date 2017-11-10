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
			this.ismute = new System.Windows.Forms.CheckBox();
			this.isDeafen = new System.Windows.Forms.CheckBox();
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
			this.textbox_username.Location = new System.Drawing.Point(70, 7);
			this.textbox_username.Name = "textbox_username";
			this.textbox_username.ReadOnly = true;
			this.textbox_username.Size = new System.Drawing.Size(262, 20);
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
			this.textbox_nickname.Location = new System.Drawing.Point(70, 37);
			this.textbox_nickname.Name = "textbox_nickname";
			this.textbox_nickname.Size = new System.Drawing.Size(262, 20);
			this.textbox_nickname.TabIndex = 3;
			// 
			// btnNicknameApply
			// 
			this.btnNicknameApply.Location = new System.Drawing.Point(338, 35);
			this.btnNicknameApply.Name = "btnNicknameApply";
			this.btnNicknameApply.Size = new System.Drawing.Size(50, 23);
			this.btnNicknameApply.TabIndex = 4;
			this.btnNicknameApply.Text = "Apply";
			this.btnNicknameApply.UseVisualStyleBackColor = true;
			this.btnNicknameApply.Click += new System.EventHandler(this.btnNicknameApply_Click);
			// 
			// ismute
			// 
			this.ismute.AutoSize = true;
			this.ismute.Location = new System.Drawing.Point(70, 64);
			this.ismute.Name = "ismute";
			this.ismute.Size = new System.Drawing.Size(50, 17);
			this.ismute.TabIndex = 5;
			this.ismute.Text = "Mute";
			this.ismute.UseVisualStyleBackColor = true;
			// 
			// isDeafen
			// 
			this.isDeafen.AutoSize = true;
			this.isDeafen.Location = new System.Drawing.Point(70, 87);
			this.isDeafen.Name = "isDeafen";
			this.isDeafen.Size = new System.Drawing.Size(61, 17);
			this.isDeafen.TabIndex = 5;
			this.isDeafen.Text = "Deafen";
			this.isDeafen.UseVisualStyleBackColor = true;
			// 
			// MemberControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.isDeafen);
			this.Controls.Add(this.ismute);
			this.Controls.Add(this.btnNicknameApply);
			this.Controls.Add(this.textbox_nickname);
			this.Controls.Add(this.textbox_username);
			this.Controls.Add(this.label_nickname);
			this.Controls.Add(this.label_username);
			this.Name = "MemberControl";
			this.Size = new System.Drawing.Size(445, 232);
			this.Load += new System.EventHandler(this.MemberControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_username;
		private System.Windows.Forms.TextBox textbox_username;
		private System.Windows.Forms.Label label_nickname;
		private System.Windows.Forms.TextBox textbox_nickname;
		private System.Windows.Forms.Button btnNicknameApply;
		private System.Windows.Forms.CheckBox ismute;
		private System.Windows.Forms.CheckBox isDeafen;
	}
}
