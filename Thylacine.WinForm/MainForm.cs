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
					discord.OnGuildRemove += (s, e) => this.Invoke(new MethodInvoker(delegate () { OnDiscordGuildUpdated(s, e); }));

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
			this.Text = "Thylacine - " + discord.User.Username;
			UpdateStatusText();
		}

		private void OnDiscordGuildUpdated(object sender, Event.GuildEventArgs args)
		{
			UpdateGuilds();
		}

		private void UpdateGuilds()
		{
			//Get a list of guilds
			Guild[] guilds = Discord.GetGuilds();

			//Make the tabs visible. Stop here if we have no guilds (this will make only the help visible).
			tabs.Visible = true;
			if (guilds.Length == 0) return;

			//Clear the pages
			tabs.TabPages.Clear();

			//For each guild, if we have its name, load it.
			for (int i = 0; i < guilds.Length; i++) {
				if (guilds[i].IsReady) AddGuildTab(guilds[i]);
			}

			//Update the status text
			//statusGuilds.Text = string.Format("Guilds: {0}/{1}", tabs.TabPages.Count, guilds.Length);

			//Update the user status too
			UpdateStatusText();
		}

		private void UpdateStatusText()
		{
			//This is slow and inefficient. #shitsandgigs
			statusUsers.Text = string.Format("Users: {0}", discord.GetUsers().Length);
			statusGuild.Text = string.Format("Guilds: {0}", discord.GetGuilds().Length);
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

		private void buttonGetInvite_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(discord.GetBotInvite(Permission.ALL));
		}

		private void debugBreakToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string name = discord.User.Username;
			Console.WriteLine("Sole reason of this button is to allow easy breakpoints in the editor. " + name);
		}
	}
}
