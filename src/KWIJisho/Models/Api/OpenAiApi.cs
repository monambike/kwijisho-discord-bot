using OpenAI_API;
using System.Threading.Tasks;

namespace KWIJisho.Models.Apis
{
    /// <summary>
    /// API Documentation: https://github.com/OkGoDoIt/OpenAI-API-dotnet
    /// </summary>
    internal static class OpenAiApi
    {
        internal static async Task<string> GetPromptKWIJishoStyleAsync(string input)
        {
            var style = "Aja alegre e animada, falando de um jeito descontraído e se possível com emojis. Nada de formalidade, pontos finais e capitalizar o início das palavras.";
            return await GetPromptAsync(style, input);
        }

        internal static async Task<string> GetPromptTranslatorToPortugueseAsync(string input)
        {
            var style = "Traduza o seguinte texto para português.";
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
