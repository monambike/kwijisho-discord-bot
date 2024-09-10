// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using System.Threading.Tasks;
using static KWiJisho.Database.Services.ServerService;

namespace KWiJisho.Commands
{
    internal class CommandConfig
    {
        public static async Task CommandLinkChannelAsync(DiscordChannel interactionContext, DiscordChannel discordChannel, ChannelLink channelLink)
        {
            await UpdateServerChannelByEnumAsync(interactionContext.Guild.Id, channelLink, discordChannel.Id);
            await interactionContext.SendMessageAsync($@"Foi adicionado o de {discordChannel.Name} para ""{channelLink}"" com sucesso!");
        }

        public static async Task CommandUnlinkChannelAsync(DiscordChannel interactionContext, ChannelLink channelLink)
        {
            await UpdateServerChannelByEnumAsync(interactionContext.Guild.Id, channelLink, null);
            await interactionContext.SendMessageAsync($@"Foi limpo o vínculo do canal ""{channelLink}"" com sucesso!");
        }
    }
}
