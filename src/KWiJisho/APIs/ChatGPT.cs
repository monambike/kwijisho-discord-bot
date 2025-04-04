﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Config;
using KWiJisho.Utils;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KWiJisho.APIs
{
    /// <summary>
    /// This class provides methods for interacting with the OpenAI GPT-3.5 model
    /// using the OpenAI-API-dotnet library.
    /// API Documentation: <a href="https://github.com/OkGoDoIt/OpenAI-API-dotnet"/>
    /// </summary>
    public static class ChatGPT
    {
        /// <summary>
        /// Represents an instance of the OpenAIAPI configuration.
        /// </summary>
        private static readonly OpenAIAPI OpenAIAPI = new(ConfigJson.ChatGptToken);

        /// <summary>
        /// Represents a instance of KWiJisho conversation.
        /// </summary>
        private static readonly Conversation KWiJishoConversation = OpenAIAPI.Chat.CreateConversation();

        public static readonly LogContext LogContext = new()
        {
            IssuerId = Data.KWiJisho.Name
        };

        /// <summary>
        /// Asynchronous method to generate a prompt for summarizing text.
        /// </summary>
        /// <param name="input">The text to be summarized.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        public static async Task<string> GetPromptSummarizeTextAsync(string input)
            => await GetPromptAsync(input, "Summarize the following text to a maximum of 5 or 6 lines.");

        /// <summary>
        /// Asynchronous method to generate a prompt for translating text to Brazilian Portuguese.
        /// </summary>
        /// <param name="input">The text to be translated.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>

        public static async Task<string> GetPromptTranslateToPortugueseAsync(string input)
            => await GetPromptAsync(input, "Translate the following text into Brazilian Portuguese.");

        /// <summary>
        /// Asynchronous method to generate a prompt in the style of "KWiJisho".
        /// </summary>
        /// <param name="input">The user input text.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        public static async Task<string?> GetKWiJishoPromptAsync(string input)
        {
            // The default style for the ChatGPT prompts executed by this method.
            var personality = "Aja alegre e animada, falando de um jeito descontraído e se possível com emojis. Nada de formalidade, pontos finais e capitalizar o início das palavras."
                     + @"O seu nome agora é ""KWiJisho"", eu te dei esse nome porque você inicialmente era um bot de dicionário e esse é um jogo de palavras com ""Kawaii"" e ""Jisho"" em japonês. Fale pouco e resumido.";

            // Appending system message representing the prompt style.
            KWiJishoConversation.AppendSystemMessage(personality);

            // Appending user input to the conversation.
            KWiJishoConversation.AppendUserInput(input);

            // Getting a response from the chatbot asynchronously.
            var response = await GetResponseFromChatBotWithTimeoutAsync(10000);

            // Returning the response.
            return response;
        }

        /// <summary>
        /// Asynchronously retrieves a response from ChatGPT, returning null if the request times out.
        /// </summary>
        /// <param name="timeoutMilliseconds">The maximum time, in milliseconds, to wait for a response.</param>
        /// <returns>The chatbot response as a string, or null if the operation timed out.</returns>
        public static async Task<string?> GetResponseFromChatBotWithTimeoutAsync(int timeoutMilliseconds)
        {
            var task = KWiJishoConversation.GetResponseFromChatbotAsync();
            var timeoutTask = Task.Delay(timeoutMilliseconds);

            var completedTask = await Task.WhenAny(task, timeoutTask);

            if (completedTask == task)
            {
                try
                {
                    return await task;
                }
                catch (Exception ex)
                {
                    var match = Regex.Match(ex.Message, @"""message"":\s*""(.*?)""");
                    string errorMessage = match.Success ? match.Groups[1].Value : ex.Message;

                    await Logs.DefaultLog.AddErrorAsync(Log.Module.KWiGpt, LogContext ,$"Error while obtaining response from ChatGPT: {errorMessage}");
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Asynchronous method to generate a prompt for the OpenAI GPT-3.5 model.
        /// </summary>
        /// <param name="input">The user input text.</param>
        /// <param name="promptStyle">The style of the prompt to be added.</param>
        /// <returns>A Task representing the asynchronous operation, yielding the generated prompt.</returns>
        private static async Task<string> GetPromptAsync(string input, string promptStyle)
        {
            // Creating an instance of the OpenAIAPI class with the ChatGptToken.
            var api = new OpenAIAPI(ConfigJson.ChatGptToken);

            // Creating a new conversation.
            var chat = api.Chat.CreateConversation();

            // Appending system message representing the prompt style.
            chat.AppendSystemMessage(promptStyle);

            // Appending user input to the conversation.
            chat.AppendUserInput(input);

            // Getting a response from the chatbot asynchronously.
            var response = await chat.GetResponseFromChatbotAsync();

            // Returning the response.
            return response;
        }
    }
}
