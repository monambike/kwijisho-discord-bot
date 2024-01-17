using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace KWIJisho.Models.Events
{
    internal class BotStart
    {
        internal static Task OnClientReady(object sender, ReadyEventArgs e) => Task.CompletedTask;
    }
}
