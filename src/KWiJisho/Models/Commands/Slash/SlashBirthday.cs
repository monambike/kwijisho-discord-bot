using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
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
        internal static async Task GetNextBirthdayAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await Birthday.GetNextBirthdayAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the command to list people will have birthday.
        /// </summary>

        [SlashCommand("birthday-list", "Mostra a lista de aniversariantes!")]
        internal static async Task GetListBirthdayAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await Birthday.GetListBirthdayAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }
    }
}
