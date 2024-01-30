using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Slash
{
    internal class SlashBirthday : ApplicationCommandModule
    {
        [SlashCommand("next-birthday", "Mostra o aniversário mais próximo!")]
        internal static async Task GetNextBirthdayAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await Birthday.GetNextBirthdayAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }


        [SlashCommand("birthday-list", "Mostra a lista de aniversariantes!")]
        internal static async Task GetListBirthdayAsync(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            await Birthday.GetListBirthdayAsync(interactionContext.Channel, interactionContext.Guild);
            await interactionContext.DeleteResponseAsync();
        }
    }
}
