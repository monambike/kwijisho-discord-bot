// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using KWiJisho.Config;
using KWiJisho.Data;
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
        /// Represents a set of notice prefix commands.
        /// </summary>
        public class PrefixNotice : BaseCommandModule
        {
            // The send news command syntax
            private static readonly string sendNewsCommandSyntax = $@"{ConfigJson.Prefix}{nameof(sendNews)} ""{{title}}"" ""{{description}}"" true".ToDiscordBold();

            // The send news help description
            private static readonly string sendNewsDescription =
$@"Envia uma notícia ao servidor. Formato: {sendNewsCommandSyntax}.
Considerações:
{"-".ToDiscordEscape()} Você pode anexar uma imagem à mensagem e ela será enviada junto à notícia, caso anexar mais de uma só a primeira será enviada.
{"-".ToDiscordEscape()} Você precisa por aquele true no final para não ser enviado pro meu servidor de testes! :)";

            /// <summary>
            /// Represents the send news prefix command.
            /// </summary>
            public PrefixCommand sendNews = new(nameof(sendNews), sendNewsDescription, Manage, Permissions.Administrator);

            /// <summary>
            /// Prefix send news command method to send news at the news' channel.
            /// </summary>
            /// <param name="commandContext">The command context.</param>
            /// <param name="title">The news' title.s</param>
            /// <param name="description">The news' description.s</param>
            /// <param name="sendInTramontina">If the news should be send in <see cref="Servers.Tramontina"/>; otherwise, it will send it to <see cref="Servers.Personal"/>.</param>
            /// <returns>A <see cref="Task"/> containing the result from the asynchronous method.</returns>
            [Command(nameof(sendNews))]
            [RequireUserPermissions(Permissions.Administrator)]
            public async Task SendNewsAsync(CommandContext commandContext, string title, string description, bool sendInTramontina = false)
            {
                // If there's an attachment in the list, get the first one
                var attachment = commandContext.Message.Attachments.Count > 0 ? commandContext.Message.Attachments[0] : null;

                // Send the message
                await CommandNotice.ExecuteNewsAsync(commandContext.Client, commandContext.Member, title, description, sendInTramontina, attachment);
            }
        }
    }
}
