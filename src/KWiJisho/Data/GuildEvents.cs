// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Entities;

namespace KWiJisho.Data
{
    /// <summary>
    /// Provides predefined embed builders for common guild events like welcoming and saying goodbye to users.
    /// </summary>
    internal class GuildEvents
    {
        /// <summary>
        /// Gets a builder used to create a welcome message embed.
        /// </summary>
        public GuildEventEmbedBuilder WelcomeBuilder => new()
        {
            Title = "BEM-VINDO",

            ImageUrl = "https://i.pinimg.com/originals/21/2a/18/212a18aaa6cc146f60752310ff6365ec.gif",

            UpdateType = Events.DiscordGuildHandler.GuildEventType.Welcome
        };

        /// <summary>
        /// Gets a builder used to create a goodbye message embed.
        /// </summary>
        public GuildEventEmbedBuilder GoodbyeBuilder => new()
        {
            Title = "ACHO QUE ISSO É UM ADEUS",

            ImageUrl = "https://i.pinimg.com/736x/8d/9c/bb/8d9cbb2d295733c198f7d1d2498697db.jpg",

            UpdateType = Events.DiscordGuildHandler.GuildEventType.Goodbye
        };
    }
}
