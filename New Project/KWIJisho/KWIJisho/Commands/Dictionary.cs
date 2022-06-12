using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace KWIJisho
{
    internal class Dictionary : BaseCommandModule
    {
        internal class Word : BaseCommandModule
        {
            [Command("addw")]
            internal void Add(CommandContext commandContext)
            {
                await commandContext.Channel.SendMessageAsync("aaaaaaaa").ConfigureAwait(false);
            }

            internal void Remove()
            {

            }

            internal void Edit()
            {

            }

            internal void GetRandomInternetWord()
            {

            }
        }
    }
}
