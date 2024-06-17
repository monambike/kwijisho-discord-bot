using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of NASA slash commands.
    /// </summary>
    internal class SlashNasa : ApplicationCommandModule
    {
        /// <summary>
        /// Represents a command group to senda random animated emoji.
        /// </summary>
        [SlashCommandGroup("nasa-apod", "Envia um emoji animado aleatório! hehe.")]
        public class Emoji : ApplicationCommandModule
        {
            /// <summary>
            /// Represents the command to get the english APOD.
            /// </summary>
            [SlashCommand("english", "The mostro o APOD igualzinho ao site da NASA!")]
            internal static async Task ExecuteEnglishApodAsync(InteractionContext interactionContext)
            {
                // Acknowledge the interaction by deferring the response with a loading state.
                await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

                await Nasa.ApodAsync(interactionContext.Channel);

                // Delete the initial acknowledgment message after processing the command.
                await interactionContext.DeleteResponseAsync();
            }

            /// <summary>
            /// Represents the command to get the portuguese APOD.
            /// </summary>
            [SlashCommand("portuguese", "The mostro o APOD em português resumidinho e fácil de ler!")]
            internal static async Task ExecutePortugueseApodHelpAsync(InteractionContext interactionContext)
            {
                // Acknowledge the interaction by deferring the response with a loading state.
                await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

                await Nasa.ApodResumeAsync(interactionContext.Channel);

                // Delete the initial acknowledgment message after processing the command.
                await interactionContext.DeleteResponseAsync();
            }
        }
    }
}
