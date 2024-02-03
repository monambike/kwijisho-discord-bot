using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
        /// Represents a set of NASA prefix commands.
        /// </summary>
        internal class PrefixNasa : BaseCommandModule
        {
            /// <summary>
            /// Represent the command to get the APOD at its current default state.
            /// </summary>
            internal PrefixCommand apod = new(nameof(apod), $"(APOD - Astronomy Picture of the Day) Te trago a imagem do dia fresquinha diretamente do site da Nasa!", Astronomy);

            /// <summary>
            /// Represent the command to get the APOD resumed and translated to portuguese.
            /// </summary>
            internal PrefixCommand apodResume = new(nameof(apodResume), $"Te trago o mesmo conteúdo do comando {"!apod".ToDiscordBold()} mas resumido e traduzido pra português. Muito mais fácil e divertido de ler!", Astronomy);

            /// <summary>
            /// Represents the asynchronous prefix APOD method callend when user asks for the APOD command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            [Command(nameof(apod))]
            internal async Task ApodAsync(CommandContext commandContext)
            {
                // Triggering typing async so user understand that the bot is processing.
                await commandContext.TriggerTypingAsync();

                // Sending the original APOD
                await Nasa.ApodAsync(commandContext.Channel);
            }

            /// <summary>
            /// Represents the asynchronous prefix APOD resume method callend when user asks for the APOD resume command.
            /// </summary>
            /// <param name="commandContext">The command context from the current command call.</param>
            /// <returns>A <see cref="Task"/> from the current asynchronous task.</returns>
            /// <returns></returns>
            [Command(nameof(apodResume))]
            internal async Task ApodResumeAsync(CommandContext commandContext)
            {
                // Triggering typing async so user understand that the bot is processing.
                await commandContext.TriggerTypingAsync();

                // Sending the APOD resume
                await Nasa.ApodResumeAsync(commandContext.Channel);
            }
        }
    }
}
