namespace Thylacine.WinForm
{
	partial class MessageBrowser
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
			this.label1 = new System.Windows.Forms.Label();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.channelList = new Thylacine.WinForm.ChannelList();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Channel:";
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(16, 40);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(56, 17);
			this.radioButton1.TabIndex = 3;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Before";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(16, 63);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(59, 17);
			this.radioButton2.TabIndex = 3;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Around";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(16, 86);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(47, 17);
			this.radioButton3.TabIndex = 3;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "After";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// channelList
			// 
			this.channelList.BackColor = System.Drawing.Color.Transparent;
			this.channelList.Guild = null;
			this.channelList.Location = new System.Drawing.Point(68, 9);
			this.channelList.MaximumSize = new System.Drawing.Size(10000, 1000);
			this.channelList.MinimumSize = new System.Drawing.Size(21, 21);
			this.channelList.Name = "channelList";
			this.channelList.PrefixTextChannels = "#";
			this.channelList.PrefixVoiceChannels = "🔊";
			this.channelList.Selected = null;
			this.channelList.ShowTextChannels = true;
			this.channelList.ShowVoiceChannels = false;
			this.channelList.Size = new System.Drawing.Size(196, 25);
			this.channelList.TabIndex = 0;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(16, 109);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 4;
			this.buttonSearch.Text = "Search";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// MessageBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(886, 452);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.radioButton3);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.channelList);
			this.Name = "MessageBrowser";
			this.Text = "MessageBrowser";
			this.Load += new System.EventHandler(this.MessageBrowser_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ChannelList channelList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.Button buttonSearch;
	}
}