// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections.Generic;

namespace KWiJisho.Data
{
    /// <summary>
    /// Notification service messages for welcome, goodbye and boost.
    /// </summary>
    public class NotificationMessages
    {
        /// <summary>
        /// Welcome messages for notification service.
        /// </summary>
        public static List<string> WelcomeMessages =
        [
            "EAEEEEEE, Bem-vindo ao servidor {user} meu consagrado! ;D",
            "SEJA BEM V-V-V-VIIIIIIIIIIIINDO AO TRA-MON-TINAAAA 🎉 {user}",
            "Como vai {user} meu parceiro? 😎 Aproveite a sua estadia por aqui e se precisar de alguma titia KAWAI JISHO TÁ NA ÁAAAREA",
            "Bem-vindo {user} ao servidor MAIS LEGAL DE TODOS, com o bot mais legal da face da terra hehehe 😎 (vulgo euzinha)"
        ];

        /// <summary>
        /// Goodbye messages for notification service.
        /// </summary>
        public static List<string> GoodbyeMessages =
        [
            "Até logo amigo.. Foi bom te conhecer {user} :(",
            "Já vai tarde.. BRINCADEIRINHA HAHAHA... Ai mas não.. falando sério, vai fazer falta 🙁 {user}",
            "NÃAAO, partiu ainda tão tão joveeeeeeeeem 😭😭😭😭 Sentiremos sua falta {user}..",
            "{user}... Pera... Ele fazia parte desse servidor? 🤔 podia jurar que vi num servidor furry.. Q-Quer dizer.. 😦😶 Não que eu também esteja lá, me adicionaram contra minha vontade!"
        ];
    }
}
