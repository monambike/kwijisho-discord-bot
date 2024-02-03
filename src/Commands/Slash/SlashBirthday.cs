using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of birthday slash commands.
    /// </summary>
    internal class SlashBirthday : ApplicationCommandModule
    {
        /// <summary>
        /// Represents the command to show the next person to have birthday.
        /// </summary>
        [SlashCommand("next-birthday", "Mostra o aniversário mais próximo!")]
        internal static async Task ExecuteSlashNextBirthdayAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await Birthday.ExecuteNextBirthdayAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the command to list people will have birthday.
        /// </summary>

        [SlashCommand("birthday-list", "Mostra a lista de aniversariantes!")]
        internal static async Task ExecuteSlashBirthdayListAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await Birthday.ExecuteBirthdayListAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }
    }
}
