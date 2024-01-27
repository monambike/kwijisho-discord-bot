using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
{
    public class SlashCommands : ApplicationCommandModule
    {

        [SlashCommand("testkwijisho", "A slash command made to test the DSharpPlusSlashCommands library!")]
        public async Task TestCommand(InteractionContext ctx) { }
    }
}
