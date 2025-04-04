// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of birthday slash commands.
    /// </summary>
    public class SlashBirthday : ApplicationCommandModule
    {
        /// <summary>
        /// Represents the command to show the next person to have birthday.
        /// </summary>
        [SlashCommand("next-birthday", "Mostra o aniversário mais próximo!")]
        public static async Task ExecuteSlashNextBirthdayAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await CommandBirthday.CommandNextBirthdayAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the command to list people will have birthday.
        /// </summary>

        [SlashCommand("birthday-list", "Mostra a lista de aniversariantes!")]
        public static async Task ExecuteSlashBirthdayListAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await CommandBirthday.CommandBirthdayListAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the command to say happy birthday to someone.
        /// </summary>
        [SlashCommand("happy-birthday", "Diz feliz aniversário para alguém!")]
        public static async Task ExecuteSlashHappyBirthdayAsync(InteractionContext interactionContext,
            [Option("user", "O usuário para qual você quer desejar feliz aniversário.")] DiscordUser discordUser)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await CommandBirthday.CommandHappyBirthdayAsync(interactionContext.Channel, interactionContext.Guild, discordUser);
            await interactionContext.DeleteResponseAsync();
        }
    }
}
