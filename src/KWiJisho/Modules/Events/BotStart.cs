using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Events
{
    internal class BotStart
    {
        internal static Task OnClientReady(object sender, ReadyEventArgs e) => Task.CompletedTask;
    }
}
