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
	public partial class MainForm : Form
	{
		private Discord discord;
		public Discord Discord => discord;

		private ConnectingForm _connectingForm;

		public MainForm() { InitializeComponent(); }
		private void MainForm_Load(object sender, EventArgs e)
		{
			InitializeDiscord();
		}

		private void InitializeDiscord()
		{
			while (discord == null)
			{
				//Create the connect form
				ConnectForm connecting = new ConnectForm();
				var results = connecting.ShowDialog();
				
				if (results == DialogResult.OK)
				{
					//Prepare the connecting form
					_connectingForm = new ConnectingForm();

					//Create the discord instance
					discord = new Discord(connecting.Token);
					discord.OnDiscordReady += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnDiscordReady(s, e); }));
					discord.OnGuildUpdate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnDiscordGuildUpdated(s, e); }));
					discord.OnGuildCreate += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnDiscordGuildUpdated(s, e); }));

					discord.Connect();

					//Result was OK
					_connectingForm.ShowDialog();
				}
				else
				{
					//Result was an apport
					var abortresult = MessageBox.Show(
						"Cannot connect to Discord without bot authentication. Would you like to retry the connection?",
						"Failed to Connect",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question
					);

					if (abortresult != DialogResult.Yes)
					{
						Application.Exit();
						break;
					}
				}
			}
		}


		private void OnDiscordReady(object sender, Event.DiscordReadyEventArgs args)
		{			
			if (_connectingForm != null) _connectingForm.Close();
			UpdateStatusUsers();
		}

		private void OnDiscordGuildUpdated(object sender, Event.GuildEventArgs args)
		{
			UpdateGuilds();
		}

		private void UpdateGuilds()
		{
			//Get a list of guilds
			Guild[] guilds = Discord.GetGuilds();

			//Clear the tabs
			tabs.TabPages.Clear();

			//For each guild, if we have its name, load it.
			for (int i = 0; i < guilds.Length; i++) {
				if (guilds[i].IsReady) AddGuildTab(guilds[i]);
			}

			//Update the status text
			//statusGuilds.Text = string.Format("Guilds: {0}/{1}", tabs.TabPages.Count, guilds.Length);

			//Update the user status too
			//UpdateStatusUsers();
		}

		private void UpdateStatusUsers()
		{
			//This is slow and inefficient. #shitsandgigs
			//statusUsers.Text = string.Format("Users: {0}", Discord.GetUsers().Count);
		}

		private void AddGuildTab(Guild g)
		{
			TabPage tbp = new TabPage(g.Name);

			GuildControl content = new GuildControl(g);
			content.Name = "cnt" + tbp.Name;
			content.Dock = DockStyle.Fill;
			tbp.Controls.Add(content);

			tabs.TabPages.Add(tbp);
		}
	}
}
