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
		public Guild Guild { get { return _guild; } set { _guild = value; SyncChannels(); } }
		private Guild _guild;

		public bool ShowTextChannels { get; set; } = true;
		public bool ShowVoiceChannels { get; set; } = true;
		public string PrefixTextChannels { get; set; } = "#";
		public string PrefixVoiceChannels { get; set; } = "🔊";

		public event EventHandler SelectedIndexChanged;

		public Channel Selected
		{
			get
			{
				if (Guild == null) return null;
				if (combobox.SelectedItem == null) return null;

				IDNameBox item = (IDNameBox)combobox.SelectedItem;
				return Guild.GetChannel(item.Identifier);
			}
			set
			{
				if (Guild == null || value == null)
				{
					combobox.SelectedIndex = -1;
					return;
				}

				for(int i = 0; i < combobox.Items.Count; i++)
				{
					IDNameBox item = (IDNameBox)combobox.Items[i];
					if (item.Identifier.Equals(value.ID))
					{
						combobox.SelectedIndex = i;
						return;
					}
				}
			}
		}

		public ChannelList() : this(null) { }
		public ChannelList(Guild guild)
		{
			InitializeComponent();

			this.Guild = guild;
			SyncChannels();
		}
		
		public void SyncChannels()
		{
			//Clear all the items
			combobox.Items.Clear();

			//We have no guild, return.
			if (Guild == null) return;

			//Prepare the channels
			var channels = Guild.GetChannels().Values;
			var sorted = channels
				.Where(c => (c.Type == ChannelType.Text && ShowTextChannels) || (c.Type == ChannelType.Voice && ShowVoiceChannels))
				.OrderBy(c => c.Type);

			foreach (Channel c in sorted)
			{
				string name = (c.Type == ChannelType.Text ? PrefixTextChannels : PrefixVoiceChannels) + c.Name;

				combobox.Items.Add(new IDNameBox() { DisplayName = name, Identifier = c.ID });
				if (c.Position == 0) combobox.SelectedIndex = combobox.Items.Count - 1;
			}
		}



		private void ChannelList_Load(object sender, EventArgs e)
		{

		}
		private void combobox_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedIndexChanged?.Invoke(this, e);
		}
	}
}
