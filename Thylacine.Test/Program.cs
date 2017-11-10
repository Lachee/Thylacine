using Thylacine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Thylacine.Helper;

namespace Thylacine.Test
{
    class Program
    {
		static void Main(string[] args)
		{
			Run().Wait();
		}
		static async Task Run() { 

            try
            {
                string key = System.IO.File.ReadAllText("botkey.key");

                Discord bot = new Discord(key);
                bot.OnMessageCreate += async (sender, e) =>
				{
					if (e.Message.Author.ID == bot.User.ID) return;
					if (e.Message.Author.ID != 172002275412279296L && !e.Message.Content.ToLowerInvariant().Contains("platypus")) return;

					//Prepare the random
					Random random = new Random();

					//Prepare the builder
					MessageBuilder mb = new MessageBuilder();
					mb.Append(e.Message.Author).Append(" ");

					//Prepare the content
					string content = e.Message.FormatContent();
					for (int i = 0; i < content.Length; i++)
						mb.Append(random.NextDouble() > 0.5D ? content[i].ToString().ToUpperInvariant() : content[i].ToString().ToLowerInvariant());

					Embed embed = new Embed()
					{
						Title = "SoMe faNcy AttaCheMent",
						Timestamp = DateTime.UtcNow,
						Image = new EmbedImage()
						{
							URL = "http://i0.kym-cdn.com/entries/icons/original/000/022/940/spongebobicon.jpg"
						}
					};

					//Send it out
					await e.Guild.GetChannel(e.Message.ChannelID).SendMessage(mb, false, e.Message.Attachments.Length > 0 ? embed : null);
					Console.WriteLine("{0}: {1}", e.Message.Author.Username, e.Message.FormatContent());
				};
				
				//Connect to the servers
                Console.WriteLine("Connecting...");
                bot.Connect();

				//Wait for the guilds to connect
				Console.WriteLine("Waiting for channels...");
				while(bot.GetGuilds().Length == 0)
				{
					Console.Write(".");
					await Task.Delay(100);
				}
				Console.WriteLine();

                Console.WriteLine("Type a message to send in our first guild, first channel. Type EXIT to quit");
                while (true)
                {
                    string message = Console.ReadLine();
                    if (message == "exit") break;
                  
                    var channels = bot.GetGuilds().First().GetChannels().Values.Where(c => c.Type == Models.ChannelType.Text).ToArray();

                    Console.WriteLine("Text Channels:");
                    foreach (var c in channels)
                        Console.WriteLine("Channel: " + c.Name);

                    var channel = channels.Length > 1 ? channels[1] : channels[0];

                    if (message == "test")
                    {
                        Invite invite = await channel.CreateInvite(60, 1, true, true);
						Console.WriteLine("Invite created: {0}", invite);
                        continue;
                    }


                    Console.WriteLine("Sending '" + message + "' to channel '" + (channel?.Name ?? "nothing") + "'");
                    Message msg = channel.SendMessage(message).Result;
                    Console.WriteLine("Message ID: {0}", msg.ID);
                }
            }catch(Exception e)
            {
                Console.WriteLine("A exception has occured: {0}", e.Message);
            }
            
            Console.WriteLine("Press anykey to exit");
            Console.ReadKey(true);
        }
        
        public static string FormatContent(Discord discord, string content)
        {
            if (discord == null) return content;
            
            Regex regex = new Regex(@"(?:<@|<@!)(?'user'\d+)>|<#(?'channel'\d+)>|<@&(?'role'\d+)>|(?:<:)(?'name'\w+)(?::)(?'id'\d+)(?:)");
            return regex.Replace(content, MatchEvaluator);
        }

        public static string MatchEvaluator(Match e)
        {
            if (e.Groups["user"] != null)
            {
                ulong id;
                if (!ulong.TryParse(e.Groups["user"].Value, out id))
                    return e.Value;
                
            }

            return e.Value;
        }
    }
}
