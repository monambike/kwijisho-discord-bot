using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of notice slash commands.
    /// </summary>
    internal class SlashThemeTramontina : ApplicationCommandModule
    {
        /// <summary>
        /// Represents the asynchronous slash reset theme method called when user asks for the slash reset theme command.
        /// </summary>
        /// <param name="interactionContext">The interaction context from the current command call.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        [SlashCommand("theme-reset", "Volta o tema do servidor para o Padrão!!")]
        [SlashRequireUserPermissions(Permissions.Administrator)]
        internal static async Task ExecuteSlashResetAsync(InteractionContext interactionContext)
        {
            // Acknowledge the interaction by deferring the response with a loading state.
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Call the SendNewsAsync method from the Notice class to send the news with provided parameters.
            await CommandThemeTramontina.ResetThemeAsync(interactionContext.Channel, interactionContext.Client);

            // Delete the initial acknowledgment message after processing the command.
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the asynchronous slash christmas theme method called when user asks for the slash christmas theme command.
        /// </summary>
        /// <param name="interactionContext">The interaction context from the current command call.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        [SlashCommand("theme-christmas", "🎅🏻 Define o tema do servidor para o de Natal!!")]
        [SlashRequireUserPermissions(Permissions.Administrator)]
        internal static async Task ExecuteSlashChristmasAsync(InteractionContext interactionContext)
        {
            // Acknowledge the interaction by deferring the response with a loading state.
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Call the SendNewsAsync method from the Notice class to send the news with provided parameters.
            await CommandThemeTramontina.SetChristmasThemeAsync(interactionContext.Channel, interactionContext.Client);

            // Delete the initial acknowledgment message after processing the command.
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the asynchronous slash easter theme method called when user asks for the slash easter theme command.
        /// </summary>
        /// <param name="interactionContext">The interaction context from the current command call.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        [SlashCommand("theme-easter", "🐇 Define o tema do servidor para o de Páscoa!!")]
        [SlashRequireUserPermissions(Permissions.Administrator)]
        internal static async Task ExecuteSlashEasterAsync(InteractionContext interactionContext)
        {
            // Acknowledge the interaction by deferring the response with a loading state.
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Call the SendNewsAsync method from the Notice class to send the news with provided parameters.
            await CommandThemeTramontina.SetEasterThemeAsync(interactionContext.Channel, interactionContext.Client);

            // Delete the initial acknowledgment message after processing the command.
            await interactionContext.DeleteResponseAsync();
        }

        /// <summary>
        /// Represents the asynchronous slash halloween theme method called when user asks for the slash halloween theme command.
        /// </summary>
        /// <param name="interactionContext">The interaction context from the current command call.</param>
        /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
        [SlashCommand("theme-halloween", "🎃 Define o tema do servidor para o de Halloween!!")]
        [SlashRequireUserPermissions(Permissions.Administrator)]
        internal static async Task ExecuteSlashHalloweenAsync(InteractionContext interactionContext)
        {
            // Acknowledge the interaction by deferring the response with a loading state.
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Call the SendNewsAsync method from the Notice class to send the news with provided parameters.
            await CommandThemeTramontina.SetHalloweenThemeAsync(interactionContext.Channel, interactionContext.Client);

            // Delete the initial acknowledgment message after processing the command.
            await interactionContext.DeleteResponseAsync();
        }
    }
}
