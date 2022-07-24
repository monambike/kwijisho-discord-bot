using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KWIJisho
{
    internal class Dictionary : BaseCommandModule
    {
        internal class Word : BaseCommandModule
        {
            [Command("addw")]
            internal async Task Add(CommandContext commandContext, string wordName)
            {
                Server.Dictionary.InsertWord(wordName);

                var message = GetRandomAddMessage(wordName);
                _ = await commandContext.Channel.SendMessageAsync(message).ConfigureAwait(false);
            }

            internal async Task Delete(CommandContext commandContext, string wordName)
            {

            }

            internal async Task Edit(CommandContext commandContext, string wordName)
            {

            }

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

            [Command("seew")]
            internal async Task GetWord(CommandContext commandContext, string wordName)
            {

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
