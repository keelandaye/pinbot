using System;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeelanBot
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;
        public static List<DiscordMessage> pins = new List<DiscordMessage>();

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfig
            {
                Token = "MzQ5MzI4ODYyMDExNzE5Njgw.DHz5bA.HDOUJkXpOdc-imfGkUSmnDQohqI",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });

            commands.RegisterCommands<Commands>();

            discord.MessageReactionAdd += async e =>
            {
                if (pins.Contains(e.Message))
                {
                    return;
                }
                if (e.Emoji.Equals(DiscordEmoji.FromName(e.Client, ":pushpin:")))
                {
                    var reacts = await e.Message.GetReactionsAsync(DiscordEmoji.FromName(e.Client, ":pushpin:"));
                    if (reacts.Count == 2 || e.User.Username.Equals("Keelan"))
                    {
                        DiscordEmbed em = new DiscordEmbed();
                        em.Color = RandColor();
                        em.Title = e.Message.Author.Username + " was pinned!";
                        em.Description = e.Message.Content;
                        em.Timestamp = DateTime.Now;
                        DiscordChannel c = e.Channel.Guild.GetChannel(349617470287511562);
                        await c.SendMessageAsync("", false, em);
                        await e.Message.CreateReactionAsync(DiscordEmoji.FromName(e.Client, ":white_check_mark:"));
                        pins.Add(e.Message);
                    }
                }
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        public static int RandColor()
        {
            Random r = new Random();
            return r.Next(0, 16777215);
        }
    }
}
