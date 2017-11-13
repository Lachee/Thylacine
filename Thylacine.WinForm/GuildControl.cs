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
	public partial class GuildControl : UserControl
	{
		private Guild _guild;
		public Guild Guild => _guild;

		public GuildControl(Guild guild)
		{
			InitializeComponent();

			_guild = channellist.Guild = guild;
		}

		private void GuildControl_Load(object sender, EventArgs arg)
		{
			Guild.OnMemberUpdate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnMemberUpdate(s, e); }));
			Guild.OnMemberCreate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnMemberUpdate(s, e); }));
			Guild.OnChannelUpdate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnChannelUpdate(s, e); }));
			Guild.OnChannelCreate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnChannelUpdate(s, e); }));

			UpdateChannels();
			UpdateMembers();
		}

		#region Events
		private void OnChannelUpdate(object sender, Event.ChannelEventArgs args) { UpdateChannels(); }
		private void OnMemberUpdate(object sender, Event.GuildMemberEventArgs args) { UpdateMembers(); }
		#endregion

		#region Updates
		private void UpdateChannels() { channellist.SyncChannels(); }

		private void UpdateMembers()
		{
			listview.Items.Clear();
			foreach (GuildMember m in Guild.GetMembers().Values)
			{
				string name = m.Nickname ?? m.User.Username;
				if (name == null) continue;

				//Ignore Offline Users
				if (m.Presence == null && checkbox_hideoffline.Checked) continue;



				listview.Items.Add(new ListViewItem(name) { Tag = m.ID });
			}
		}

		private void ApplyUserFilter() { UpdateMembers(); }

		#endregion

		private void checkbox_hideoffline_CheckStateChanged(object sender, EventArgs e) => ApplyUserFilter();
		private async void buttonMessageSend_Click(object sender, EventArgs e)
		{
			//Get the channel
			Channel channel = channellist.Selected;
			if (channel == null) return;

			//Send a message
			await channel.SendMessage(textMessage.Text);
			textMessage.Text = "";
			
			//Get the selected channel
		}

		private void listview_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listview.SelectedIndices.Count == 0)
			{
				memberview.Member = null;
				return;
			}

			ulong id = (ulong)listview.SelectedItems[0].Tag;
			memberview.Member = Guild.GetMember(id);
		}
	}
}

public struct IDNameBox
{
	public string DisplayName { get; set; }
	public ulong Identifier { get; set; }
	
	public override string ToString()
	{
		return DisplayName;
	}
}