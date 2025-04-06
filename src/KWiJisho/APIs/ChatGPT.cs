// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Config;
using KWiJisho.Utils;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KWiJisho.APIs
{
    /// <summary>
    /// This class provides methods for interacting with the OpenAI model using the OpenAI dotnet library.
    /// API Documentation: <a href="https://github.com/openai/openai-dotnet"/>
    /// </summary>
    public static class ChatGPT
    {
        /// <summary>
        /// Represents an instance of the ChatGPT API Client configuration from OpenAI.
        /// </summary>
        private static ChatClient ChatClient = new(ConfigJson.ChatGptModel, ConfigJson.ChatGptToken);

        /// <summary>
        /// Represents a instance of KWiJisho log context.
        /// </summary>
        public static readonly LogContext LogContext = new()
        {
            IssuerId = Data.KWiJisho.Name
        };

        /// <summary>
        /// Asynchronous method to generate a prompt for summarizing text.
        /// </summary>
        /// <param name="input">The text to be summarized.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        public static async Task<string?> GetPromptSummarizeTextAsync(string input)
        {
            await Logs.DefaultLog.AddInfoAsync(Log.Module.ChatGpt, LogContext, $"Summarizing the text...");
            return await GetPromptAsync(input, "Summarize the following text to a maximum of 5 or 6 lines.");
        }

        /// <summary>
        /// Asynchronous method to generate a prompt for translating text to Brazilian Portuguese.
        /// </summary>
        /// <param name="input">The text to be translated.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>

        public static async Task<string?> GetPromptTranslateToBrazilianPortugueseAsync(string input)
        {
            await Logs.DefaultLog.AddInfoAsync(Log.Module.ChatGpt, LogContext, $"Translating the text for Brazilian Portuguese...");
            return await GetPromptAsync(input, "Translate the following text into Brazilian Portuguese.");
        }

        /// <summary>
        /// Asynchronous method to generate a prompt in the style of "KWiJisho".
        /// </summary>
        /// <param name="input">The user input text.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        public static async Task<string?> GetKWiJishoPromptAsync(string input)
        {
            // The default style for the ChatGPT prompts executed by this method.
            var personality = "Aja alegre e animada, falando de um jeito descontraído e se possível com emojis. Nada de formalidade, pontos finais e capitalizar o início das palavras."
                     + @"O seu nome agora é ""KWiJisho"", eu te dei esse nome porque você inicialmente era um bot de dicionário e esse é um jogo de palavras com ""Kawaii"" e ""Jisho"" em japonês. Fale pouco e resumido de um jeito natural como se conhecesse todo mundo do servidor Tramontina.";

            // Getting a response from the chatbot asynchronously.
            var response = await GetPromptAsync(input, personality);

            // Returning the response.
            return response;
        }

        /// <summary>
        /// Asynchronous method to generate a prompt for the OpenAI GPT-3.5 model.
        /// </summary>
        /// <param name="input">The user input text.</param>
        /// <param name="promptStyle">The style of the prompt to be added.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        private static async Task<string?> GetPromptAsync(string input, string promptStyle)
        {
            if (!ConfigJson.EnableChatGpt) return null;

            List<ChatMessage> chatMessages = [];
            ClientResult<ChatCompletion> chatCompletion;

            chatMessages.Add(promptStyle);
            chatMessages.Add(new UserChatMessage(input));

            try
            {
                chatCompletion = await ChatClient.CompleteChatAsync(chatMessages);
                await Logs.DefaultLog.AddInfoAsync(Log.Module.ChatGpt, LogContext, $"Tokens were successfully consumed by a request to ChatGPT. Input: {chatCompletion.Value.Usage.InputTokenCount} Output: {chatCompletion.Value.Usage.OutputTokenCount}");
            }
            catch (Exception ex)
            {
                var match = Regex.Match(ex.Message, @"""message"":\s*""(.*?)""");
                string errorMessage = match.Success ? match.Groups[1].Value : ex.Message;

                await Logs.DefaultLog.AddErrorAsync(Log.Module.ChatGpt, LogContext, $"Error while obtaining response from ChatGPT: {errorMessage}");
                return null;
            }

            // Getting a response from the chatbot asynchronously.
            var textResponse = chatCompletion.Value.Content[0].Text;
            chatMessages.Add(new AssistantChatMessage(textResponse));

            // Returning the response.
            return textResponse;
        }
    }
}
