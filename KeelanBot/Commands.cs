using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace KeelanBot
{
    class Commands
    {
        public static List<DiscordUser> invites = new List<DiscordUser>();
        public static List<DiscordUser> interview = new List<DiscordUser>();
        public static DiscordUser CurrentInterview;
        public static bool interviewing = false;

        [Command("hi")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hey there {ctx.User.Mention}!");
        }

        [Command("embed")]
        public async Task Embed(CommandContext ctx, String args)
        {
            DiscordEmbed e = new DiscordEmbed();
            e.Title = args + " was pinned!";
            e.Color = 16711680;
            await ctx.RespondAsync("", false, e);
        }
    }
}
