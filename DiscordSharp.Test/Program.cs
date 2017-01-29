using DiscordSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordSharp.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                string key = System.IO.File.ReadAllText("botkey.key");

                DiscordBot bot = new DiscordBot(key);
                bot.OnMessageCreate += (sender, e) =>
                {
                    if (e.Message.Author.IsBot) return;

                    MessageBuilder mb = new MessageBuilder();
                    mb.Append(e.Message.Author).Append(" said: ").Append(e.Message);
                // e.Guild.GetChannel(e.Message.ChannelID).SendMessage(mb, false);

                Console.WriteLine("{0}: {1}", e.Message.Author.Username, e.Message.FormatContent());
                };

                Console.WriteLine("Connecting...");
                bot.Connect();

                Console.WriteLine("Type a message to send in our first guild, first channel. Type EXIT to quit");
                while (true)
                {
                    string message = Console.ReadLine();
                    if (message == "exit") break;

                    var channels = bot.GetGuilds().First().Channels.Where(c => c.Type == Models.ChannelType.Text).ToArray();

                    Console.WriteLine("Text Channels:");
                    foreach (var c in channels)
                        Console.WriteLine("Channel: " + c.Name);

                    var channel = channels.Length > 1 ? channels[1] : channels[0];

                    Console.WriteLine("Sending '" + message + "' to channel '" + (channel?.Name ?? "nothing") + "'");
                    Message msg = channel.SendMessage(message);
                    Console.WriteLine("Message ID: {0}", msg.ID);
                }
            }catch(Exception e)
            {
                Console.WriteLine("A exception has occured: {0}", e.Message);
            }
            
            Console.WriteLine("Press anykey to exit");
            Console.ReadKey(true);
        }
        
        public static string FormatContent(DiscordBot discord, string content)
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
