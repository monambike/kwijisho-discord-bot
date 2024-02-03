using OpenAI_API;
using System.Threading.Tasks;

namespace KWiJisho.APIs
{
    /// <summary>
    /// This class provides methods for interacting with the OpenAI GPT-3.5 model
    /// using the OpenAI-API-dotnet library.
    /// API Documentation: <a href="https://github.com/OkGoDoIt/OpenAI-API-dotnet"/>
    /// </summary>
    internal static class OpenAiApi
    {
        /// <summary>
        /// Asynchronous method to generate a prompt in the style of "KWiJisho."
        /// </summary>
        /// <param name="input">The user input text.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>

        internal static async Task<string> GetPromptKWiJishoStyleAsync(string input)
        {
            // The default style for the ChatGPT prompts executed by this method.
            var style = "Aja alegre e animada, falando de um jeito descontraído e se possível com emojis. Nada de formalidade, pontos finais e capitalizar o início das palavras."
                     + @"E o seu nome é ""KWiJisho"", eu te dei esse nome porque você inicialmente era um bot de dicionário e esse é um jogo de palavras com ""Kawaii"" e ""Jisho"" em japonês.";

            // Getting the prompt assynchronously.
            return await GetPromptAsync(style, input);
        }

        /// <summary>
        /// Asynchronous method to generate a prompt for summarizing text.
        /// </summary>
        /// <param name="input">The text to be summarized.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        internal static async Task<string> GetPromptSummarizeTextAsync(string input)
            => await GetPromptAsync(input, "Summarize the following text to a maximum of 5 or 6 lines.");

        /// <summary>
        /// Asynchronous method to generate a prompt for translating text to Brazilian Portuguese.
        /// </summary>
        /// <param name="input">The text to be translated.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>

        internal static async Task<string> GetPromptTranslateToPortugueseAsync(string input)
            => await GetPromptAsync(input, "Translate the following text into Brazilian Portuguese.");

        /// <summary>
        /// Asynchronous method to generate a prompt for the OpenAI GPT-3.5 model.
        /// </summary>
        /// <param name="userInput">The user input text.</param>
        /// <param name="promptStyle">The style of the prompt to be added.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        private static async Task<string> GetPromptAsync(string userInput, string promptStyle)
        {
            // Creating an instance of the OpenAIAPI class with the ChatGptToken.
            var api = new OpenAIAPI(ConfigJson.ChatGptToken);

            // Creating a new conversation.
            var chat = api.Chat.CreateConversation();

            // Appending system message representing the prompt style.
            chat.AppendSystemMessage(promptStyle);

            // Appending user input to the conversation.
            chat.AppendUserInput(userInput);

            // Getting a response from the chatbot asynchronously.
            var response = await chat.GetResponseFromChatbotAsync();

            // Returning the response.
            return response;
        }
    }
}
