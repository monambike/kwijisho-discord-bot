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
            internal class Word : BaseCommandModule
            {
                public Command addw = new Command("addw", "Adiciona uma palavra no dicionário.");
                [Command(nameof(addw))]
                internal async Task Add(CommandContext commandContext, string wordName)
                {
                    Server.Dictionary.InsertWord(wordName);

                    var message = GetRandomAddMessage(wordName);
                    _ = await commandContext.Channel.SendMessageAsync(message).ConfigureAwait(false);
                }

                internal async Task Delete(CommandContext commandContext, string wordName)
                {
                    var message = wordName;
                    _ = await commandContext.Channel.SendMessageAsync(message).ConfigureAwait(false);
                }

                public Command editw = new Command("editw", "Edita uma palavra do dicionário.");
                [Command(nameof(editw))]
                internal async Task Edit(CommandContext commandContext, string wordName)
                {
                    var message = wordName;
                    _ = await commandContext.Channel.SendMessageAsync(message).ConfigureAwait(false);
                }

                [Command("seew")]
                internal async Task Get(CommandContext commandContext, string wordName)
                {
                    var message = wordName;
                    _ = await commandContext.Channel.SendMessageAsync(message).ConfigureAwait(false);
                }


                internal void GetRandomInternetWord()
                {

                }


                private string GetRandomAddMessage(string wordName)
                {
                    var quotedWordName = $@"""{wordName}""";

                    var messages = new List<string>
                    {
                        $"Tá na mão colega! Botei a {quotedWordName} no dicionário pra você \u1F60E",

                        $"A palavra {quotedWordName} foi adicionada com sucEEEEEESSO AO DICIONÁRIO!",
                        $"É! NÓIS! TA NA MÃO PORRAAAAAAAAAAAA!! \u1F973 PRO DI-CI-O-NÁARIO \u1F483\u1F483",

                        $"Eu achei a {quotedWordName} bem merda. Mas o seu bom gosto com palavras vai bem com a sua cara \u1F60F.",
                        $"Meu deus hein que ideia mixuruca... Pronto, botei {quotedWordName} no dicionário... \u1F644",
                        $"Se eu não recebesse pra isso eu nem fazia questão \u1F644. Pronto, botei {wordName} pra você lá."
                    };

                    int randomIndex = new Random().Next(messages.Count);
                    return messages[randomIndex];
                }
            }
        }
    }
}
