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
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Test");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("sdads");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("fdafdasfasd");
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusUsersOnline = new System.Windows.Forms.ToolStripStatusLabel();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.buttonMessageSend = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupUsers = new System.Windows.Forms.GroupBox();
			this.listview = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.channellist = new Thylacine.WinForm.ChannelList();
			this.memberview = new Thylacine.WinForm.MemberControl();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupUsers.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusUsersOnline});
			this.statusStrip1.Location = new System.Drawing.Point(0, 479);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(875, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusUsersOnline
			// 
			this.statusUsersOnline.Name = "statusUsersOnline";
			this.statusUsersOnline.Size = new System.Drawing.Size(85, 17);
			this.statusUsersOnline.Text = "Users Online: 0";
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(6, 46);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(246, 341);
			this.textMessage.TabIndex = 1;
			// 
			// buttonMessageSend
			// 
			this.buttonMessageSend.Location = new System.Drawing.Point(163, 17);
			this.buttonMessageSend.Name = "buttonMessageSend";
			this.buttonMessageSend.Size = new System.Drawing.Size(89, 23);
			this.buttonMessageSend.TabIndex = 2;
			this.buttonMessageSend.Text = "Send";
			this.buttonMessageSend.UseVisualStyleBackColor = true;
			this.buttonMessageSend.Click += new System.EventHandler(this.buttonMessageSend_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.channellist);
			this.groupBox1.Controls.Add(this.buttonMessageSend);
			this.groupBox1.Controls.Add(this.textMessage);
			this.groupBox1.Location = new System.Drawing.Point(10, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(258, 393);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Send Message";
			// 
			// groupUsers
			// 
			this.groupUsers.Controls.Add(this.listview);
			this.groupUsers.Location = new System.Drawing.Point(274, 3);
			this.groupUsers.Name = "groupUsers";
			this.groupUsers.Size = new System.Drawing.Size(177, 393);
			this.groupUsers.TabIndex = 6;
			this.groupUsers.TabStop = false;
			this.groupUsers.Text = "Users";
			// 
			// listview
			// 
			this.listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listview.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
			this.listview.Location = new System.Drawing.Point(3, 16);
			this.listview.Name = "listview";
			this.listview.Size = new System.Drawing.Size(171, 374);
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
			// channellist
			// 
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
			this.channellist.Size = new System.Drawing.Size(151, 21);
			this.channellist.TabIndex = 8;
			// 
			// memberview
			// 
			this.memberview.Location = new System.Drawing.Point(457, 19);
			this.memberview.Name = "memberview";
			this.memberview.Size = new System.Drawing.Size(321, 152);
			this.memberview.TabIndex = 7;
			// 
			// GuildControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.memberview);
			this.Controls.Add(this.groupUsers);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "GuildControl";
			this.Size = new System.Drawing.Size(875, 501);
			this.Load += new System.EventHandler(this.GuildControl_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupUsers.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusUsersOnline;
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Button buttonMessageSend;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupUsers;
		private System.Windows.Forms.ListView listview;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private ChannelList channellist;
		private MemberControl memberview;
	}
}
