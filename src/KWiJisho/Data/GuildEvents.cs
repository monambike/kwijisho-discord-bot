// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Entities;

namespace KWiJisho.Data
{
    internal class GuildEvents
    {
        public GuildEventEmbedBuilder WelcomeBuilder => new()
        {
            Title = "BEM-VINDO",

            ImageUrl = "https://i.pinimg.com/originals/21/2a/18/212a18aaa6cc146f60752310ff6365ec.gif",

            UpdateType = Events.DiscordGuildHandler.GuildEventType.Welcome
        };

        public GuildEventEmbedBuilder GoodbyeBuilder => new()
        {
            Title = "ACHO QUE ISSO É UM ADEUS",

            ImageUrl = "https://i.pinimg.com/736x/8d/9c/bb/8d9cbb2d295733c198f7d1d2498697db.jpg",

            UpdateType = Events.DiscordGuildHandler.GuildEventType.Goodbye
        };
    }
}
