using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using KWiJisho.Commands;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Provides methods for "prefix" and "slash" notice commands.
    /// </summary>
    internal class SlashNotice : ApplicationCommandModule
    {
        /// <summary>
        /// Represents the slash command method to send news at the news channel.
        /// </summary>
        /// <param name="interactionContext">The interaction context from the command.</param>
        /// <param name="title">The message title.</param>
        /// <param name="description">The message description.</param>
        /// <param name="discordAttachment">The attachment (if present).</param>
        /// <param name="sendInTramontina">If the message should be sent in Tramontina Discord server or the Personal Test Server.</param>
        /// <returns></returns>
        [SlashCommand("send-news", "Manda uma notícia no canal de notícias!!")]
        [SlashRequireUserPermissions(Permissions.Administrator)]
        internal static async Task ChatGptPromptAsync(InteractionContext interactionContext,
            [Option("title", "O título da sua mensagem!!")] string title,
            [Option("description", "A descrição da sua mensagem!!")] string description,
            [Option("image", "Quer anexar alguma imagem ao seu textinho?")] DiscordAttachment discordAttachment = null,
            [Option("send-in-tramontina", "Eu envio pro canal de notícias do Tramontina ao invés do servidor de teste?")] bool sendInTramontina = false)
        {
            // Acknowledge the interaction by deferring the response with a loading state.
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Call the SendNewsAsync method from the Notice class to send the news with provided parameters.
            await Notice.SendNewsAsync(interactionContext.Client, interactionContext.Member, title, description, sendInTramontina, discordAttachment);

            // Delete the initial acknowledgment message after processing the command.
            await interactionContext.DeleteResponseAsync();
        }
    }
}
