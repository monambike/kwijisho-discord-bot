﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using KWiJisho.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    public partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of NASA prefix commands.
        /// </summary>
        public class PrefixNasa : BaseCommandModule
        {
            /// <summary>
            /// Represent the command to get the APOD in english.
            /// </summary>
            public PrefixCommand apod = new(nameof(apod), $"(APOD - Astronomy Picture of the Day) Te trago a imagem do dia fresquinha e resumida diretamente do site da Nasa!", Astronomy);

            /// <summary>
            /// Represent the command to get the APOD in portuguese.
            /// </summary>
            public PrefixCommand apodPortuguese = new(nameof(apodPortuguese), $"Te trago o mesmo conteúdo do comando {"!apod".ToDiscordBold()} mas resumido e traduzido pra português!!", Astronomy);

            /// <summary>
            /// Represents the asynchronous prefix APOD method callend when user asks for the APOD command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(apod))]
            public async Task ApodEnglishAsync(CommandContext commandContext)
            {
                // Triggering typing async so user understand that the bot is processing.
                await commandContext.TriggerTypingAsync();

                // Sending the APOD in english.
                await CommandNasa.ApodEnglishAsync(commandContext.Channel);
            }

            /// <summary>
            /// Represents the asynchronous prefix APOD in portuguese method called when user asks for the APOD in portuguese command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            /// <returns></returns>
            [Command(nameof(apodPortuguese))]
            public async Task ApodPortugueseAsync(CommandContext commandContext)
            {
                // Triggering typing async so user understand that the bot is processing.
                await commandContext.TriggerTypingAsync();

                // Sending the APOD in portuguese.
                await CommandNasa.ApodPortugueseAsync(commandContext.Channel);
            }
        }
    }
}
