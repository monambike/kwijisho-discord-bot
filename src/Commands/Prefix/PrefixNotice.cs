using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using KWiJisho.Commands;
using KWiJisho.Utils;
using System.Threading.Tasks;

namespace KWiJisho.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of prefix channel notice commands.
        /// </summary>
        internal class PrefixNotice : BaseCommandModule
        {
            // The send news command syntax
            private static readonly string commandSyntax = $@"{ConfigJson.Prefix}{nameof(sendNews)} ""{{title}}"" ""{{description}}"" true".ToDiscordBold();

            // The help description for the send news command
            private static readonly string description =
$@"Envia uma notícia ao servidor. Formato: {commandSyntax}.
Considerações:
{"-".ToDiscordEscape()} Você pode anexar uma imagem à mensagem e ela será enviada junto à notícia, caso anexar mais de uma só a primeira será enviada.
{"-".ToDiscordEscape()} Você precisa por aquele true no final para não ser enviado pro meu servidor de testes! :)";

            /// <summary>
            /// Represents the prefix command method to send news at the news channel.
            /// </summary>
            internal PrefixCommand sendNews = new(nameof(sendNews), description, Manage, Permissions.Administrator);
            [Command(nameof(sendNews))]
            [RequireUserPermissions(Permissions.Administrator)]
            internal async Task SendNewsAsync(CommandContext commandContext, string title, string description, bool sendInTramontina = false)
            {
                // If there's an attachment in the list, get the first one
                var attachment = commandContext.Message.Attachments.Count > 0 ? commandContext.Message.Attachments[0] : null;

                // Send the message
                await Notice.SendNewsAsync(commandContext.Client, commandContext.Member, title, description, sendInTramontina, attachment);

            }
        }
    }
}
