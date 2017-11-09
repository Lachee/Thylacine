using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thylacine.WinForm
{
	public partial class ConnectForm : Form
	{
		const string BOTKEY_FILE = "botkey.key";
		public string Token => text_botkey.Text;

		public ConnectForm() { InitializeComponent(); }

		private void ConnectForm_Load(object sender, EventArgs e)
		{
			//Attempt to read the key file
			string key = "";
			label_readkeyfile.Visible = TryLoadKey(out key);

			//Enable things
			text_botkey.Text = key;

		}

		private bool TryLoadKey(out string key)
		{
			key = "";

			//Make sure the file exists
			if (!System.IO.File.Exists(BOTKEY_FILE))
				return false;

			//Input the key
			key = System.IO.File.ReadAllText(BOTKEY_FILE);
			return true;
		}
	}
}
