using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thylacine.Models;

namespace Thylacine.WinForm
{
	public partial class MessageBrowser : Form
	{
		public Guild Guild { get; set; }
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
			var results = await channelList.Selected.FetchMessages(70); //FetchMessagesBefore(380220811078008842L, 70);
			Console.WriteLine("BREAK");
		}
	}
}
