using OpenAI_API;
using System.Threading.Tasks;

namespace KWiJisho.Models.Api
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

        internal static async Task<string> GetPromptSummarizeTextAsync(string input)
            => await GetPromptAsync(input, "Summarize the following text to a maximum of 5 or 6 lines.");

        internal static async Task<string> GetPromptTranslateToPortugueseAsync(string input)
            => await GetPromptAsync(input, "Translate the following text into Brazilian Portuguese.");

        private static async Task<string> GetPromptAsync(string userInput, string promptStyle)
        {
            // Getting chat gpt token and creating conversation
            var api = new OpenAIAPI(ConfigJson.ChatGptToken);
            var chat = api.Chat.CreateConversation();

            // Appending texts for the prompt
            chat.AppendSystemMessage(promptStyle);
            // Appending user input
            chat.AppendUserInput(userInput);

            // Returning message
            var response = await chat.GetResponseFromChatbotAsync();
            return response;
        }
    }
}
