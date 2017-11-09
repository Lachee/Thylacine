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
			_guild = guild;
		}

		private void GuildControl_Load(object sender, EventArgs arg)
		{
			Guild.OnMemberUpdate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnMemberUpdate(s, e); }));
			Guild.OnMemberCreate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnMemberUpdate(s, e); }));
			Guild.OnChannelUpdate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnChannelUpdate(s, e); }));
			Guild.OnChannelCreate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnChannelUpdate(s, e); }));

			properties.SelectedObject = Guild;
			UpdateChannels();
			UpdateMembers();
		}

		#region Events
		private void OnChannelUpdate(object sender, Event.ChannelEventArgs args) { UpdateChannels(); }
		private void OnMemberUpdate(object sender, Event.GuildMemberEventArgs args) { UpdateMembers(); }
		#endregion

		#region Updates
		private void UpdateChannels()
		{
			comboChannels.Items.Clear();
			foreach (Channel c in Guild.GetChannels().Values)
			{
				if (c.IsPrivate) continue;
				if (c.Type != ChannelType.Text) continue;

				comboChannels.Items.Add(new IDNameBox() { DisplayName = "#" + c.Name, Identifier = c.ID });
				if (c.Position == 0) comboChannels.SelectedIndex = comboChannels.Items.Count - 1;
			}
		}

		private void UpdateMembers()
		{
			listview.Items.Clear();
			foreach(GuildMember m in Guild.GetMembers().Values)
			{
				string name = m.Nickname ?? m.User.Username;
				if (name == null)
					continue;

				listview.Items.Add(new ListViewItem(name) { Tag = m.ID });
				//groupUsers.Controls.Add(new Label() { Text = m.Nickname });
			}

			statusUsersOnline.Text = string.Format("Online: {0}", Guild.MemberCount);
		}
		#endregion

		private async void buttonMessageSend_Click(object sender, EventArgs e)
		{
			//Get the channel
			var combo = (IDNameBox)(comboChannels.SelectedItem);
			Channel channel = Guild.GetChannel(combo.Identifier);

			//Send a message
			await channel.SendMessage(textMessage.Text);
			textMessage.Text = "";

			//Get the selected channel
		}

		private void listview_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listview.SelectedIndices.Count == 0)
			{
				properties.SelectedObject = Guild;
				return;
			}

			ulong id = (ulong)listview.SelectedItems[0].Tag;
			properties.SelectedObject = Guild.GetMember(id);
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