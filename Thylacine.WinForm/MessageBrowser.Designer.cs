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
			this.radioBefore = new System.Windows.Forms.RadioButton();
			this.radioAround = new System.Windows.Forms.RadioButton();
			this.radioAfter = new System.Windows.Forms.RadioButton();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.radioBeforeLastMessage = new System.Windows.Forms.RadioButton();
			this.sliderMessages = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.textbox_messageCount = new System.Windows.Forms.TextBox();
			this.textbox_id = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listbox_messages = new System.Windows.Forms.ListBox();
			this.propertybox_message = new System.Windows.Forms.PropertyGrid();
			this.channelList = new Thylacine.WinForm.ChannelList();
			this.textbox_message = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.sliderMessages)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Channel:";
			// 
			// radioBefore
			// 
			this.radioBefore.AutoSize = true;
			this.radioBefore.Location = new System.Drawing.Point(76, 114);
			this.radioBefore.Name = "radioBefore";
			this.radioBefore.Size = new System.Drawing.Size(56, 17);
			this.radioBefore.TabIndex = 3;
			this.radioBefore.Text = "Before";
			this.radioBefore.UseVisualStyleBackColor = true;
			// 
			// radioAround
			// 
			this.radioAround.AutoSize = true;
			this.radioAround.Location = new System.Drawing.Point(76, 138);
			this.radioAround.Name = "radioAround";
			this.radioAround.Size = new System.Drawing.Size(59, 17);
			this.radioAround.TabIndex = 3;
			this.radioAround.Text = "Around";
			this.radioAround.UseVisualStyleBackColor = true;
			// 
			// radioAfter
			// 
			this.radioAfter.AutoSize = true;
			this.radioAfter.Location = new System.Drawing.Point(138, 114);
			this.radioAfter.Name = "radioAfter";
			this.radioAfter.Size = new System.Drawing.Size(47, 17);
			this.radioAfter.TabIndex = 3;
			this.radioAfter.Text = "After";
			this.radioAfter.UseVisualStyleBackColor = true;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(76, 187);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 4;
			this.buttonSearch.Text = "Search";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// radioBeforeLastMessage
			// 
			this.radioBeforeLastMessage.AutoSize = true;
			this.radioBeforeLastMessage.Checked = true;
			this.radioBeforeLastMessage.Location = new System.Drawing.Point(76, 91);
			this.radioBeforeLastMessage.Name = "radioBeforeLastMessage";
			this.radioBeforeLastMessage.Size = new System.Drawing.Size(125, 17);
			this.radioBeforeLastMessage.TabIndex = 3;
			this.radioBeforeLastMessage.TabStop = true;
			this.radioBeforeLastMessage.Text = "Before Last Message";
			this.radioBeforeLastMessage.UseVisualStyleBackColor = true;
			// 
			// sliderMessages
			// 
			this.sliderMessages.LargeChange = 25;
			this.sliderMessages.Location = new System.Drawing.Point(117, 49);
			this.sliderMessages.Maximum = 100;
			this.sliderMessages.Minimum = 1;
			this.sliderMessages.Name = "sliderMessages";
			this.sliderMessages.Size = new System.Drawing.Size(155, 45);
			this.sliderMessages.TabIndex = 5;
			this.sliderMessages.TickFrequency = 10;
			this.sliderMessages.Value = 50;
			this.sliderMessages.Scroll += new System.EventHandler(this.sliderMessages_Scroll);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Messages:";
			// 
			// textbox_messageCount
			// 
			this.textbox_messageCount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.textbox_messageCount.Location = new System.Drawing.Point(76, 49);
			this.textbox_messageCount.MaxLength = 3;
			this.textbox_messageCount.Name = "textbox_messageCount";
			this.textbox_messageCount.Size = new System.Drawing.Size(35, 20);
			this.textbox_messageCount.TabIndex = 6;
			this.textbox_messageCount.Text = "50";
			this.textbox_messageCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textbox_messageCount.TextChanged += new System.EventHandler(this.textboxMessages_TextChanged);
			// 
			// textbox_id
			// 
			this.textbox_id.Location = new System.Drawing.Point(76, 161);
			this.textbox_id.Name = "textbox_id";
			this.textbox_id.Size = new System.Drawing.Size(196, 20);
			this.textbox_id.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(49, 164);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(21, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "ID:";
			// 
			// listbox_messages
			// 
			this.listbox_messages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listbox_messages.FormattingEnabled = true;
			this.listbox_messages.Location = new System.Drawing.Point(291, 12);
			this.listbox_messages.Name = "listbox_messages";
			this.listbox_messages.Size = new System.Drawing.Size(340, 303);
			this.listbox_messages.TabIndex = 9;
			this.listbox_messages.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// propertybox_message
			// 
			this.propertybox_message.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.propertybox_message.HelpVisible = false;
			this.propertybox_message.LineColor = System.Drawing.SystemColors.ControlDark;
			this.propertybox_message.Location = new System.Drawing.Point(637, 12);
			this.propertybox_message.Name = "propertybox_message";
			this.propertybox_message.Size = new System.Drawing.Size(237, 303);
			this.propertybox_message.TabIndex = 10;
			// 
			// channelList
			// 
			this.channelList.BackColor = System.Drawing.Color.Transparent;
			this.channelList.Guild = null;
			this.channelList.Location = new System.Drawing.Point(76, 12);
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
			// textbox_message
			// 
			this.textbox_message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textbox_message.Location = new System.Drawing.Point(291, 322);
			this.textbox_message.Multiline = true;
			this.textbox_message.Name = "textbox_message";
			this.textbox_message.ReadOnly = true;
			this.textbox_message.Size = new System.Drawing.Size(583, 118);
			this.textbox_message.TabIndex = 11;
			// 
			// MessageBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(886, 452);
			this.Controls.Add(this.textbox_message);
			this.Controls.Add(this.propertybox_message);
			this.Controls.Add(this.listbox_messages);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textbox_id);
			this.Controls.Add(this.textbox_messageCount);
			this.Controls.Add(this.sliderMessages);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.radioBeforeLastMessage);
			this.Controls.Add(this.radioAfter);
			this.Controls.Add(this.radioAround);
			this.Controls.Add(this.radioBefore);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.channelList);
			this.Name = "MessageBrowser";
			this.Text = "Message Browser";
			this.Load += new System.EventHandler(this.MessageBrowser_Load);
			((System.ComponentModel.ISupportInitialize)(this.sliderMessages)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ChannelList channelList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioBefore;
		private System.Windows.Forms.RadioButton radioAround;
		private System.Windows.Forms.RadioButton radioAfter;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.RadioButton radioBeforeLastMessage;
		private System.Windows.Forms.TrackBar sliderMessages;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textbox_messageCount;
		private System.Windows.Forms.TextBox textbox_id;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listbox_messages;
		private System.Windows.Forms.PropertyGrid propertybox_message;
		private System.Windows.Forms.TextBox textbox_message;
	}
}