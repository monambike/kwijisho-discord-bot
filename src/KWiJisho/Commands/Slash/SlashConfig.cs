// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using KWiJisho.Database.Services;
using System.Threading.Tasks;
using static KWiJisho.Database.Services.ServerService;

namespace KWiJisho.Commands.Slash
{
    public class SlashConfig : ApplicationCommandModule
    {
        [SlashCommandGroup("config", "Comandos referentes à configuração do bot para o servidor!")]
        public class Config : ApplicationCommandModule
        {
            [SlashCommandGroup("channel", "Configurações referentes à canais do servidor!")]
            public class Channel : ApplicationCommandModule
            {
                [SlashCommand("link", "Define um link para um canal!")]
                public async Task ExecuteSlashLinkChannelAsync(InteractionContext interactionContext,
                    [Option("channel", "O canal do servidor a ser vinculado.")] DiscordChannel discordChannel,
                    [Option("link", "O vínculo que será realizado.")] ChannelLink channelLink)
                {
                    await CommandConfig.CommandLinkChannelAsync(interactionContext.Channel, discordChannel, channelLink);
                    await interactionContext.DeferAsync();
                    await interactionContext.DeleteResponseAsync();
                }

                [SlashCommand("unlink", "Tira o link para um canal!")]
                public static async Task ExecuteSlashUnlinkChannelAsync(InteractionContext interactionContext,
                    [Option("unlink", "O vínculo que será desfeito.")] ChannelLink channelLink)
                {
                    await CommandConfig.CommandUnlinkChannelAsync(interactionContext.Channel, channelLink);
                    await interactionContext.DeferAsync();
                    await interactionContext.DeleteResponseAsync();
                }


                [SlashCommand("view", "Vê os links para os canais!")]
                public static async Task ExecuteSlashViewChannelsAsync(InteractionContext interactionContext)
                {
                    await CommandConfig.CommandSlashViewChannelsAsync(interactionContext.Channel);
                    await interactionContext.DeferAsync();
                    await interactionContext.DeleteResponseAsync();
                }
            }

            [SlashCommandGroup("role", "Configurações referentes à canais do servidor!")]
            public class Role : ApplicationCommandModule
            {
                [SlashCommand("link", "Define um link para um cargo!")]
                public static async Task ExecuteSlashLinkRoleAsync(InteractionContext interactionContext)
                {

                }

                [SlashCommand("unlink", "Tira o link para um cargo!")]
                public static async Task ExecuteSlashUnlinkRoleAsync(InteractionContext interactionContext)
                {

                }


                [SlashCommand("view", "Vê os links para os cargos!")]
                public static async Task ExecuteSlashViewRolesAsync(InteractionContext interactionContext)
                {

                }
            }
        }
    }
}
