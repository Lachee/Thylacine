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
	public partial class ChannelList : UserControl
	{
		public Guild Guild { get; set; }

		public Channel Selected
		{
			get
			{
				if (Guild == null) return null;

				IDNameBox item = (IDNameBox)combobox.SelectedItem;
				return Guild.GetChannel(item.Identifier);
			}
		}

		public ChannelList() : this(null) { }
		public ChannelList(Guild guild)
		{
			InitializeComponent();

			this.Guild = guild;
			SyncChannels();
		}


		private void ChannelList_Load(object sender, EventArgs e)
		{

		}

		public void SyncChannels()
		{
			if (Guild == null) return;

			combobox.Items.Clear();
			foreach (Channel c in Guild.GetChannels().Values)
			{
				if (c.IsPrivate) continue;
				if (c.Type != ChannelType.Text) continue;

				combobox.Items.Add(new IDNameBox() { DisplayName = "#" + c.Name, Identifier = c.ID });
				if (c.Position == 0) combobox.SelectedIndex = combobox.Items.Count - 1;
			}
		}

		private void combobox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
