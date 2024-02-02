using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of birthday prefix commands.
        /// </summary>
        internal class PrefixBirthday : BaseCommandModule
        {
            /// <summary>
            /// Represents the command to show the next person to have birthday.
            /// </summary>
            internal PrefixCommand nextBirthday = new(nameof(nextBirthday), "Mostra o aniversário mais próximo de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);
            [Command(nameof(nextBirthday))]
            internal async Task GetNextBirthdayAsync(CommandContext commandContext) => await Commands.Birthday.GetNextBirthdayAsync(commandContext.Channel, commandContext.Guild);

            /// <summary>
            /// Represents the command to list people will have birthday.
            /// </summary>
            internal PrefixCommand listBirthday = new(nameof(listBirthday), "Mostra a lista de aniversariantes de alguém presente no servidor e cadastrado na listinha de aniversariantes!", Birthday);
            [Command(nameof(listBirthday))]
            internal async Task GetListBirthdayAsync(CommandContext commandContext) => await Commands.Birthday.GetListBirthdayAsync(commandContext.Channel, commandContext.Guild);
        }
    }
}
