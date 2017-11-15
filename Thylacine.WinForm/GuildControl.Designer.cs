namespace Thylacine.WinForm
{
	partial class GuildControl
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Test");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("sdads");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("fdafdasfasd");
			this.textMessage = new System.Windows.Forms.TextBox();
			this.buttonMessageSend = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupUsers = new System.Windows.Forms.GroupBox();
			this.checkbox_hideoffline = new System.Windows.Forms.CheckBox();
			this.listview = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonBrowseMessages = new System.Windows.Forms.Button();
			this.memberview = new Thylacine.WinForm.MemberControl();
			this.channellist = new Thylacine.WinForm.ChannelList();
			this.groupBox1.SuspendLayout();
			this.groupUsers.SuspendLayout();
			this.SuspendLayout();
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(6, 49);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(246, 338);
			this.textMessage.TabIndex = 1;
			// 
			// buttonMessageSend
			// 
			this.buttonMessageSend.Location = new System.Drawing.Point(163, 19);
			this.buttonMessageSend.Name = "buttonMessageSend";
			this.buttonMessageSend.Size = new System.Drawing.Size(89, 23);
			this.buttonMessageSend.TabIndex = 2;
			this.buttonMessageSend.Text = "Send";
			this.buttonMessageSend.UseVisualStyleBackColor = true;
			this.buttonMessageSend.Click += new System.EventHandler(this.buttonMessageSend_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textMessage);
			this.groupBox1.Controls.Add(this.channellist);
			this.groupBox1.Controls.Add(this.buttonMessageSend);
			this.groupBox1.Location = new System.Drawing.Point(10, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(258, 393);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Send Message";
			// 
			// groupUsers
			// 
			this.groupUsers.Controls.Add(this.checkbox_hideoffline);
			this.groupUsers.Controls.Add(this.listview);
			this.groupUsers.Location = new System.Drawing.Point(274, 3);
			this.groupUsers.Name = "groupUsers";
			this.groupUsers.Size = new System.Drawing.Size(348, 393);
			this.groupUsers.TabIndex = 6;
			this.groupUsers.TabStop = false;
			this.groupUsers.Text = "Users";
			// 
			// checkbox_hideoffline
			// 
			this.checkbox_hideoffline.AutoSize = true;
			this.checkbox_hideoffline.Checked = true;
			this.checkbox_hideoffline.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkbox_hideoffline.Location = new System.Drawing.Point(7, 17);
			this.checkbox_hideoffline.Name = "checkbox_hideoffline";
			this.checkbox_hideoffline.Size = new System.Drawing.Size(81, 17);
			this.checkbox_hideoffline.TabIndex = 8;
			this.checkbox_hideoffline.Text = "Hide Offline";
			this.checkbox_hideoffline.UseVisualStyleBackColor = true;
			this.checkbox_hideoffline.CheckStateChanged += new System.EventHandler(this.checkbox_hideoffline_CheckStateChanged);
			// 
			// listview
			// 
			this.listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listview.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.listview.Location = new System.Drawing.Point(3, 40);
			this.listview.Name = "listview";
			this.listview.Size = new System.Drawing.Size(342, 350);
			this.listview.TabIndex = 7;
			this.listview.UseCompatibleStateImageBehavior = false;
			this.listview.View = System.Windows.Forms.View.List;
			this.listview.SelectedIndexChanged += new System.EventHandler(this.listview_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Username";
			this.columnHeader1.Width = 167;
			// 
			// buttonBrowseMessages
			// 
			this.buttonBrowseMessages.Location = new System.Drawing.Point(628, 251);
			this.buttonBrowseMessages.Name = "buttonBrowseMessages";
			this.buttonBrowseMessages.Size = new System.Drawing.Size(145, 23);
			this.buttonBrowseMessages.TabIndex = 8;
			this.buttonBrowseMessages.Text = "Browse Messages...";
			this.buttonBrowseMessages.UseVisualStyleBackColor = true;
			this.buttonBrowseMessages.Click += new System.EventHandler(this.buttonBrowseMessages_Click);
			// 
			// memberview
			// 
			this.memberview.Location = new System.Drawing.Point(625, 3);
			this.memberview.Member = null;
			this.memberview.Name = "memberview";
			this.memberview.RegisterGuildEvents = true;
			this.memberview.Size = new System.Drawing.Size(365, 242);
			this.memberview.TabIndex = 7;
			// 
			// channellist
			// 
			this.channellist.BackColor = System.Drawing.Color.Transparent;
			this.channellist.Guild = null;
			this.channellist.Location = new System.Drawing.Point(6, 19);
			this.channellist.MaximumSize = new System.Drawing.Size(10000, 1000);
			this.channellist.MinimumSize = new System.Drawing.Size(21, 21);
			this.channellist.Name = "channellist";
			this.channellist.PrefixTextChannels = "#";
			this.channellist.PrefixVoiceChannels = "🔊";
			this.channellist.Selected = null;
			this.channellist.ShowTextChannels = true;
			this.channellist.ShowVoiceChannels = false;
			this.channellist.Size = new System.Drawing.Size(151, 32);
			this.channellist.TabIndex = 8;
			// 
			// GuildControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonBrowseMessages);
			this.Controls.Add(this.memberview);
			this.Controls.Add(this.groupUsers);
			this.Controls.Add(this.groupBox1);
			this.Name = "GuildControl";
			this.Size = new System.Drawing.Size(993, 501);
			this.Load += new System.EventHandler(this.GuildControl_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupUsers.ResumeLayout(false);
			this.groupUsers.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Button buttonMessageSend;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupUsers;
		private System.Windows.Forms.ListView listview;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private ChannelList channellist;
		private MemberControl memberview;
		private System.Windows.Forms.CheckBox checkbox_hideoffline;
		private System.Windows.Forms.Button buttonBrowseMessages;
	}
}
