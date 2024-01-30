using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixBirthday : BaseCommandModule
        {
            internal PrefixCommand nextBirthday = new(nameof(nextBirthday), "Mostra o aniversário mais próximo de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);
            [Command(nameof(nextBirthday))]
            internal async Task GetNextBirthdayAsync(CommandContext commandContext) => await Commands.Birthday.GetNextBirthdayAsync(commandContext.Channel, commandContext.Guild);

            internal PrefixCommand listBirthday = new(nameof(listBirthday), "Mostra a lista de aniversariantes de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);
            [Command(nameof(listBirthday))]
            internal async Task GetListBirthdayAsync(CommandContext commandContext) => await Commands.Birthday.GetListBirthdayAsync(commandContext.Channel, commandContext.Guild);
        }
    }
}
