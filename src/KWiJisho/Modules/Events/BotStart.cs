using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Events
{
    /// <summary>
    /// Provides a set of events and methods fired when bot starts.
    /// </summary>
    internal class BotStart
    {
        /// <summary>
        /// Handles the events when the bot's client is ready.
        /// </summary>
        /// <param name="sender">The object that triggered the events.</param>
        /// <param name="e">The event arguments containing information about the ready event.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous execution of the event handler.</returns>
        internal static Task OnClientReady(object sender, ReadyEventArgs e) => Task.CompletedTask;
    }
}
