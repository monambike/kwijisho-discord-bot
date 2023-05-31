using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KWIJisho
{
    internal partial class Commands
    {
        internal partial class Dictionary : BaseCommandModule
        {
            [Command("seeall")]
            internal async Task GetAll(CommandContext commandContext)
            {
                Server.Dictionary.GetAllWords();

                var message = "Essa é a lista de palavras do dicionário.";

                _ = await commandContext
                    .Channel
                    .SendMessageAsync(message)
                    .ConfigureAwait(false);
            }
        }
    }
}
