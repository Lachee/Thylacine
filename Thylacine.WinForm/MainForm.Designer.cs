namespace Thylacine.WinForm
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabs = new System.Windows.Forms.TabControl();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.status = new System.Windows.Forms.StatusStrip();
			this.buttonGetInvite = new System.Windows.Forms.ToolStripSplitButton();
			this.statusGuild = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusUsers = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabPageHelp = new System.Windows.Forms.TabPage();
			this.labelInvite1 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonInviteOnly = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.debugBreakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabs.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.status.SuspendLayout();
			this.tabPageHelp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabPageHelp);
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(998, 556);
			this.tabs.TabIndex = 0;
			this.tabs.Visible = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(93, 48);
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// status
			// 
			this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonGetInvite,
            this.statusGuild,
            this.statusUsers});
			this.status.Location = new System.Drawing.Point(0, 559);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(998, 22);
			this.status.TabIndex = 1;
			this.status.Text = "statusStrip1";
			// 
			// buttonGetInvite
			// 
			this.buttonGetInvite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.buttonGetInvite.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugBreakToolStripMenuItem});
			this.buttonGetInvite.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetInvite.Image")));
			this.buttonGetInvite.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonGetInvite.Name = "buttonGetInvite";
			this.buttonGetInvite.Size = new System.Drawing.Size(73, 20);
			this.buttonGetInvite.Text = "Invite Bot";
			this.buttonGetInvite.ButtonClick += new System.EventHandler(this.buttonGetInvite_Click);
			// 
			// statusGuild
			// 
			this.statusGuild.Name = "statusGuild";
			this.statusGuild.Size = new System.Drawing.Size(52, 17);
			this.statusGuild.Text = "Guilds: 2";
			// 
			// statusUsers
			// 
			this.statusUsers.Name = "statusUsers";
			this.statusUsers.Size = new System.Drawing.Size(47, 17);
			this.statusUsers.Text = "Users: 0";
			// 
			// tabPageHelp
			// 
			this.tabPageHelp.Controls.Add(this.buttonInviteOnly);
			this.tabPageHelp.Controls.Add(this.label1);
			this.tabPageHelp.Controls.Add(this.labelInvite1);
			this.tabPageHelp.Controls.Add(this.pictureBox1);
			this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
			this.tabPageHelp.Name = "tabPageHelp";
			this.tabPageHelp.Size = new System.Drawing.Size(990, 530);
			this.tabPageHelp.TabIndex = 0;
			this.tabPageHelp.Text = "Thylacine Help";
			this.tabPageHelp.UseVisualStyleBackColor = true;
			// 
			// labelInvite1
			// 
			this.labelInvite1.AutoSize = true;
			this.labelInvite1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInvite1.Location = new System.Drawing.Point(73, 65);
			this.labelInvite1.Name = "labelInvite1";
			this.labelInvite1.Size = new System.Drawing.Size(677, 25);
			this.labelInvite1.TabIndex = 0;
			this.labelInvite1.Text = "Oh Dear, it looks like your bot has not been invited to any guilds yet :(";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(75, 90);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(503, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Why not invite them into a guild now? Click below to be redirected to a invite";
			// 
			// buttonInviteOnly
			// 
			this.buttonInviteOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonInviteOnly.Location = new System.Drawing.Point(382, 198);
			this.buttonInviteOnly.Name = "buttonInviteOnly";
			this.buttonInviteOnly.Size = new System.Drawing.Size(166, 61);
			this.buttonInviteOnly.TabIndex = 1;
			this.buttonInviteOnly.Text = "Invite Bot";
			this.buttonInviteOnly.UseVisualStyleBackColor = true;
			this.buttonInviteOnly.Click += new System.EventHandler(this.buttonGetInvite_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(78, 128);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(504, 194);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// debugBreakToolStripMenuItem
			// 
			this.debugBreakToolStripMenuItem.Name = "debugBreakToolStripMenuItem";
			this.debugBreakToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.debugBreakToolStripMenuItem.Text = "Debug Break";
			this.debugBreakToolStripMenuItem.Click += new System.EventHandler(this.debugBreakToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(998, 581);
			this.Controls.Add(this.status);
			this.Controls.Add(this.tabs);
			this.Name = "MainForm";
			this.Text = "Thylacine";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabs.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.status.ResumeLayout(false);
			this.status.PerformLayout();
			this.tabPageHelp.ResumeLayout(false);
			this.tabPageHelp.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.StatusStrip status;
		private System.Windows.Forms.ToolStripSplitButton buttonGetInvite;
		private System.Windows.Forms.ToolStripStatusLabel statusGuild;
		private System.Windows.Forms.ToolStripStatusLabel statusUsers;
		private System.Windows.Forms.TabPage tabPageHelp;
		private System.Windows.Forms.Button buttonInviteOnly;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelInvite1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolStripMenuItem debugBreakToolStripMenuItem;
	}
}

