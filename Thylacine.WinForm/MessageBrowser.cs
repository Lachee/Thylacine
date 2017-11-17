using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thylacine.Models;
using System.Windows.Forms;

//Helps with the ambiguity
using Message = Thylacine.Models.Message;

namespace Thylacine.WinForm
{
	public partial class MessageBrowser : Form
	{
		public Guild Guild
		{
			get { return _guild; }
			set
			{
				_guild = value;
				this.Text = "Message Browser - " + _guild?.Name;
			}
		}
		private Guild _guild;		
		private List<Message> _messages;

		public MessageBrowser()
		{
			InitializeComponent();
		}

		private void MessageBrowser_Load(object sender, EventArgs e)
		{
			channelList.Guild = Guild;
		}

		private async void buttonSearch_Click(object sender, EventArgs e)
		{
			int limit = sliderMessages.Value;
			bool hasID = !string.IsNullOrEmpty(textbox_id.Text);
			ulong id = 0;


			//Validate the ID
			if (hasID && !ulong.TryParse(textbox_id.Text, out id))
			{
				MessageBox.Show("Invalid ID. Please give a unsigned long!");
				return;
			}

			//We need a ID if anything other than last message mode
			if (!hasID && !radioBeforeLastMessage.Checked)
			{
				MessageBox.Show("A ID is required for the search method.");
				return;
			}
			
			if (radioBeforeLastMessage.Checked)
				_messages = await channelList.Selected.FetchMessages(limit);
			else if (radioBefore.Checked)
				_messages = await channelList.Selected.FetchMessagesBefore(id, limit);
			else if(radioAround.Checked)
				_messages = await channelList.Selected.FetchMessagesAround(id, limit);
			else if (radioAfter.Checked)
				_messages = await channelList.Selected.FetchMessagesAfter(id, limit);

			UpdateMessages();
		}

		private void UpdateMessages()
		{
			listbox_messages.Items.Clear();
			foreach(Message m in _messages)
			{
				listbox_messages.Items.Add(new MessageListItem(m));
			}
		}

		private void sliderMessages_Scroll(object sender, EventArgs e)
		{
			textbox_messageCount.Text = sliderMessages.Value.ToString();
		}

		private void textboxMessages_TextChanged(object sender, EventArgs e)
		{
			//Valudate the number
			int value;
			if (int.TryParse(textbox_messageCount.Text, out value))
				sliderMessages.Value = value < 1 || value > 100 ? sliderMessages.Value : value;
			
			//Set the textbox
			textbox_messageCount.Text = sliderMessages.Value.ToString();
		}

		struct MessageListItem
		{
			public Message message;
			
			public MessageListItem(Message m) { message = m; }
			public override string ToString()
			{
				return message.Author.Username + ": " + message.Content;
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedItem = listbox_messages.SelectedItem;
			var selectedMessage = (MessageListItem)selectedItem;
			propertybox_message.SelectedObject = selectedMessage.message;
			textbox_message.Text = selectedMessage.message.FormatContent();
		}
	}
}
