using OpenAI_API;
using System.Threading.Tasks;

namespace KWiJisho.Models.Apis
{
    /// <summary>
    /// API Documentation: https://github.com/OkGoDoIt/OpenAI-API-dotnet
    /// </summary>
    internal static class OpenAiApi
    {
        internal static async Task<string> GetPromptKWiJishoStyleAsync(string input)
        {
            var style = "Aja alegre e animada, falando de um jeito descontraído e se possível com emojis. Nada de formalidade, pontos finais e capitalizar o início das palavras."
                     + @"E o seu nome é ""KWiJisho"", eu te dei esse nome porque você inicialmente era um bot de dicionário e esse é um jogo de palavras com ""Kawaii"" e ""Jisho"" em japonês.";
            return await GetPromptAsync(style, input);
        }

        internal enum TranslationType { Translate, TranslateAndResume }
        internal static async Task<string> GetPromptTranslateToPortugueseAsync(string input, TranslationType text)
        {
            var style = text switch
            {
                TranslationType.Translate => "Traduza o seguinte texto para português.",
                TranslationType.TranslateAndResume => "Traduza esse texto para português reformulando-o. " +
                "Quero que você o reformule pensando nas partes mais importantes e que fique fácil de entender, mesmo para quem não é muito " +
                "chegado no assunto. Se possível deixe-o com no máximo 5 ou 6 linhas. E evite usar palavras difíceis, tente trocar palavras " +
                "difíceis por palavras mais fáceis e comuns.",
                _ => throw new System.NotImplementedException()
            };
            return await GetPromptAsync(style, input);
        }

        private static async Task<string> GetPromptAsync(string style, string input)
        {
            // Getting chat gpt token and creating conversation
            var api = new OpenAIAPI(ConfigJson.ChatGptToken);
            var chat = api.Chat.CreateConversation();

            // Appending texts for the prompt
            chat.AppendSystemMessage(style);
            chat.AppendUserInput(input);

            // Returning message
            var response = await chat.GetResponseFromChatbotAsync();
            return response;
        }
    }
}
