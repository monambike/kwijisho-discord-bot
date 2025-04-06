// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Utils
{
    /// <summary>
    /// Contains default fallback responses used by the bot in some specific cases.
    /// </summary>
    internal class BotResponses
    {
        /// <summary>
        /// Response shown when the bot fails to communicate with the ChatGPT API.
        /// </summary>
        public static string ChatGptError => "Não consigo te responder agora :( eu não consegui ligar meus fiozinhos com a OpenAI (ChatGPT), eu sinto muito...";
    }
}
