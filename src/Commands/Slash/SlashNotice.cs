﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Slash
{
    /// <summary>
    /// Represents a set of notice slash commands.
    /// </summary>
    internal class SlashNotice : ApplicationCommandModule
    {
        /// <summary>
        /// Slash send news command method to send news at the news' channel.
        /// </summary>
        /// <param name="interactionContext">The interaction context from the command.</param>
        /// <param name="title">The news' title.s</param>
        /// <param name="description">The news' description.s</param>
        /// <param name="discordAttachment">The news's attachment. (if present)</param>
        /// <param name="sendInTramontina">If the news should be send in <see cref="ServerTramontina"/>; otherwise, it will send it to <see cref="ServerPersonal"/>.</param>
        /// <returns>A <see cref="Task"/> containing the result from the asynchronous method.</returns>
        [SlashCommand("send-news", "Manda uma notícia no canal de notícias!!")]
        [SlashRequireUserPermissions(Permissions.Administrator)]
        internal static async Task ExecuteSlashNewsAsync(InteractionContext interactionContext,
            [Option("title", "O título da sua mensagem!!")] string title,
            [Option("description", "A descrição da sua mensagem!!")] string description,
            [Option("image", "Quer anexar alguma imagem ao seu textinho?")] DiscordAttachment discordAttachment = null,
            [Option("send-in-tramontina", "Eu envio pro canal de notícias do Tramontina ao invés do servidor de teste?")] bool sendInTramontina = false)
        {
            // Acknowledge the interaction by deferring the response with a loading state.
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            // Call the SendNewsAsync method from the Notice class to send the news with provided parameters.
            await Notice.ExecuteNewsAsync(interactionContext.Client, interactionContext.Member, title, description, sendInTramontina, discordAttachment);

            // Delete the initial acknowledgment message after processing the command.
            await interactionContext.DeleteResponseAsync();
        }
    }
}
