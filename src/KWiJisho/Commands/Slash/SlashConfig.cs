// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System.Threading.Tasks;
using static KWiJisho.Database.Services.ServerService;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of config slash commands.
    /// </summary>
    public class SlashConfig : ApplicationCommandModule
    {
        /// <summary>
        /// Represents a set of config slash commands.
        /// </summary>
        [SlashCommandGroup("config", "Comandos referentes à configuração do bot para o servidor!")]
        public class Config : ApplicationCommandModule
        {
            /// <summary>
            /// Represents a set of channel slash commands.
            /// </summary>
            [SlashCommandGroup("channel", "Configurações referentes à canais do servidor!")]
            public class Channel : ApplicationCommandModule
            {
                /// <summary>
                /// Represents the command to configure a link to a channel.
                /// </summary>
                [SlashCommand("link", "Define um link para um canal!")]
                [SlashRequireUserPermissions(Permissions.Administrator)]
                public async Task ExecuteSlashLinkChannelAsync(InteractionContext interactionContext,
                    [Option("channel", "O canal do servidor a ser vinculado.")] DiscordChannel discordChannel,
                    [Option("link", "O vínculo que será realizado.")] ChannelLink channelLink)
                {
                    await CommandConfig.CommandLinkChannelAsync(interactionContext.Channel, discordChannel, channelLink);
                    await interactionContext.DeferAsync();
                    await interactionContext.DeleteResponseAsync();
                }

                /// <summary>
                /// Represents the command to configure an unlink to a channel.
                /// </summary>
                [SlashCommand("unlink", "Tira o link para um canal!")]
                [SlashRequireUserPermissions(Permissions.Administrator)]
                public static async Task ExecuteSlashUnlinkChannelAsync(InteractionContext interactionContext,
                    [Option("unlink", "O vínculo que será desfeito.")] ChannelLink channelLink)
                {
                    await CommandConfig.CommandUnlinkChannelAsync(interactionContext.Channel, channelLink);
                    await interactionContext.DeferAsync();
                    await interactionContext.DeleteResponseAsync();
                }


                /// <summary>
                /// Represents the command to see channel link settings.
                /// </summary>
                [SlashCommand("view", "Vê os links para os canais!")]
                [SlashRequireUserPermissions(Permissions.Administrator)]
                public static async Task ExecuteSlashViewChannelsAsync(InteractionContext interactionContext)
                {
                    await CommandConfig.CommandSlashViewChannelsAsync(interactionContext.Channel);
                    await interactionContext.DeferAsync();
                    await interactionContext.DeleteResponseAsync();
                }
            }
        }
    }
}
