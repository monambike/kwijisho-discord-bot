﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of info slash commands.
    /// </summary>
    public class SlashInfo : ApplicationCommandModule
    {
        /// <summary>
        /// Represents the command to show help about the Discord bot commands.
        /// </summary>
        [SlashCommand("help", "Mostra a ajuda.")]
        public static async Task ExecuteSlashHelpAsync(InteractionContext interactionContext)
        {
            await CommandInfo.ExecuteHelpAsync(interactionContext.Channel, interactionContext.Client);
            await interactionContext.DeferAsync();
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the command to show information about the bot and the bot owner.
        /// </summary>
        [SlashCommand("info", "Mostra informações básicas sobre mim e o meu criador. (@monambike)")]
        public static async Task ExecuteSlashInfoAsync(InteractionContext interactionContext)
        {
            await CommandInfo.ExecuteInfoAsync(interactionContext.Channel, interactionContext.Client);
            await interactionContext.DeferAsync();
            await interactionContext.DeleteResponseAsync();
        }
    }
}
