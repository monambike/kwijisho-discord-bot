// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using KWiJisho.Config;
using System.Threading.Tasks;
using static KWiJisho.Database.Services.ServerService;

namespace KWiJisho.Commands
{
    internal class CommandConfig
    {
        public static async Task CommandLinkChannelAsync(DiscordChannel interactionContext, DiscordChannel discordChannel, ChannelLink channelLink)
        {
            await UpdateServerChannelByEnumAsync(interactionContext.Guild.Id, channelLink, discordChannel.Id);
            
            // Adding the prompt into a embed builder.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Description = $@"Foi vinculado o canal ""{discordChannel.Name}"" em ""{channelLink}"" com sucesso!",                Color = ConfigJson.DefaultColor.DiscordColor
            };

            await interactionContext.SendMessageAsync(discordEmbedBuilder);
        }

        public static async Task CommandUnlinkChannelAsync(DiscordChannel interactionContext, ChannelLink channelLink)
        {
            await UpdateServerChannelByEnumAsync(interactionContext.Guild.Id, channelLink, null);

            // Adding the prompt into a embed builder.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Description = $@"Foi removido o vínculo do canal ""{channelLink}"" com sucesso!",
                Color = ConfigJson.DefaultColor.DiscordColor
            };

            await interactionContext.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
