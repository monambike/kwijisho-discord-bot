using DSharpPlus;
using DSharpPlus.Entities;
using KWiJisho.Entities;
using KWiJisho.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for theme prefix and slash commands.
    /// </summary>
    internal static class CommandThemeTramontina
    {
        /// <summary>
        /// Represents the cooldown for theme change.
        /// </summary>
        private static readonly CommandCooldown ThemeChangeCooldown = new(2, TimeSpan.FromMinutes(10), "mudar o tema do servidor");

        // Tramontina's Text Channels.
        internal static readonly ChannelTramontina Geral = new(692588978959941656, "geral", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "💬" }, { EmojiTheme.Christmas, "🍪" }, { EmojiTheme.Easter, "🐇" }, { EmojiTheme.Halloween, "🎃" }  });
        internal static readonly ChannelTramontina PrintsEternizados = new(841452121983418418, "prints-eternizados", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "💾" }, { EmojiTheme.Christmas, "🥛" }, { EmojiTheme.Easter, "🐰" }, { EmojiTheme.Halloween, "👺" }  });
        // Tramontina's Organized Text Channels.
        internal static readonly ChannelTramontina YouTube = new(1142723035447705600, "youtube", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "📹" }, { EmojiTheme.Christmas, "🌟" }, { EmojiTheme.Easter, "🍫" }, { EmojiTheme.Halloween, "🍭" } });
        internal static readonly ChannelTramontina Dicionario = new(1143020466190172220, "dicionario", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "📖" }, { EmojiTheme.Christmas, "⛄" }, { EmojiTheme.Easter, "🥕" }, { EmojiTheme.Halloween, "🔮" } });
        // Tramontina's Bot Channels.
        internal static readonly ChannelTramontina Waifu = new(692591710466998272, "waifu", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "💘" }, { EmojiTheme.Christmas, "💝" }, { EmojiTheme.Easter, "🌷" }, { EmojiTheme.Halloween, "🍬" } });
        internal static readonly ChannelTramontina Radio = new(841136093813538827, "radio", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "📻" }, { EmojiTheme.Christmas, "🎶" }, { EmojiTheme.Easter, "🙏🏻" }, { EmojiTheme.Halloween, "💀" } });
        internal static readonly ChannelTramontina OutrosBots = new(693742473155182663, "outros-bots", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "🤖" }, { EmojiTheme.Christmas, "⛄" }, { EmojiTheme.Easter, "🧺" }, { EmojiTheme.Halloween, "🧟" } });
        // Tramontina's Voice Channels.
        internal static readonly ChannelTramontina CanalEscondidinho = new(1010349376922722436, "Canal Escondidinho", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "🏃🏻💨" }, { EmojiTheme.Christmas, "🎁🧦" }, { EmojiTheme.Easter, "🐣🌱" }, { EmojiTheme.Halloween, "🏰👻" } });
        internal static readonly ChannelTramontina CorpoDeBombeiros1 = new(929778181458767932, "Corpo de Bombeiros 1", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "👨🏻🚒" }, { EmojiTheme.Christmas, "🎅🏻🛷" }, { EmojiTheme.Easter, "🐥🥚" }, { EmojiTheme.Halloween, "🧛🏻🩸" } });
        internal static readonly ChannelTramontina CorpoDeBombeiros2 = new(826257065303474186, "Corpo de Bombeiros 2", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "👩🏻🚒" }, { EmojiTheme.Christmas, "🤶🏻🛷" }, { EmojiTheme.Easter, "🐤🥚" }, { EmojiTheme.Halloween, "🧛🏻🩸" } });
        internal static readonly ChannelTramontina CantinhoDaFofoca = new(692588979404669018, "Cantinho da Fofoca", new Dictionary<EmojiTheme, string>
            { { EmojiTheme.Default, "👥💅🏻" }, { EmojiTheme.Christmas, "🍷🍴" }, { EmojiTheme.Easter, "🌸🐝" }, { EmojiTheme.Halloween, "🤡🎈" } });

        /// <summary>
        /// Channels from tramontina server that can receive theme changing.
        /// </summary>
        private static readonly List<ChannelTramontina> TramontinaChannels =
        [
            // Tramontina's Text Channels.
            Geral,
            PrintsEternizados,
            // Tramontina's Organized Text Channels.
            YouTube,
            Dicionario,
            // Tramontina's Bot Channels.
            Waifu,
            Radio,
            OutrosBots,
            // Tramontina's Voice Channels.
            CanalEscondidinho,
            CorpoDeBombeiros1,
            CorpoDeBombeiros2,
            CantinhoDaFofoca
        ];

        /// <summary>
        /// Sets the Tramontina server to a Theme according with parameterization.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the command is being executed.</param>
        /// <param name="discordClient">The Discord client instance.</param>
        /// <param name="emojiTheme">The chosen theme to set the Discord server.</param>
        /// <param name="title">The title from the theme notice.</param>
        /// <param name="description">The description from the theme notice</param>
        /// <param name="serverNameSuggestion">The server name suggestion.</param>
        /// <returns>A <see cref="Task"/> from the method execution.</returns>
        internal static async Task SetThemeAsync(DiscordChannel discordChannel, DiscordClient discordClient, EmojiTheme emojiTheme, string title, string description, string? serverNameSuggestion = null)
        {
            // Checking if can execute current command
            if (!ThemeChangeCooldown.CanExecute(discordChannel)) return;

            // Initial message so user can know .
            await discordChannel.SendMessageAsync("Só um segundinho... Vou botar as decorações então pode tomar um tempinho! ;P");

            // Modifies emoji from every mentioned channel.
            foreach (var tramontinaChannel in TramontinaChannels)
                tramontinaChannel.ChangeEmoji(discordClient, tramontinaChannel.EmojiTheme[emojiTheme]);

            // Presentation discord embed builder (first message).
            var presentationDiscordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = title,
                Description = $"{description}{Environment.NewLine}",
                Color = Config.ConfigJson.DefaultColor.DiscordColor
            }.AddField("HOOOOOOOOOORA DE ENTRAR NO CLIMA", $"Que tal aproveitar e tentar {"trocar o nome do servidor".ToDiscordBold()} pela minha sugestãozinha abaixo? ;D AHAHAHA");

            // Sending the first message (presentation).
            await discordChannel.SendMessageAsync(new DiscordMessageBuilder().AddEmbed(presentationDiscordEmbedBuilder));

            // Just appends the festive name suggestion if there's a suggestion, if not,
            // return the default server name.
            string fullServerNameWithSuggestion = string.IsNullOrEmpty(serverNameSuggestion) ? "Tramontina│Bizarre Adventures"
                : $"{serverNameSuggestion} - Tramontina│Bizarre Adventures";

            // Getting image name and image's full path.
            var fileName = $"128x128-mello-{emojiTheme.ToString().ToLower()}.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");

            // Suggestions discord embed builder (seconds message).
            var suggestionsDiscordEmbedBuilder = new DiscordEmbedBuilder
            {
                Description = fullServerNameWithSuggestion,
                Color = Config.ConfigJson.DefaultColor.DiscordColor
            }.WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Sending the second message with the image and button.
            await discordChannel.SendMessageAsync(new DiscordMessageBuilder()
                .AddEmbed(suggestionsDiscordEmbedBuilder)
                // The image suggestion for the server.
                .AddFile(fileName, fileStream)
                // Button to copy server name suggestion.
                .AddComponents(new DiscordButtonComponent(ButtonStyle.Primary, "copy_server_name_suggestion", "Copiar Sugestão de Nome")));
        }

        /// <summary>
        /// Sets the Tramontina server to Default Theme.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the command is being executed.</param>
        /// <param name="discordClient">The Discord client instance.</param>
        /// <returns>A <see cref="Task"/> from the method execution.</returns>
        internal static async Task ResetThemeAsync(DiscordChannel discordChannel, DiscordClient discordClient)
            => await SetThemeAsync(discordChannel, discordClient, EmojiTheme.Default,
                "Voltando ao normal!!",
                "Voltei o servidor pro seu tema original :D");

        /// <summary>
        /// Sets the Tramontina server to Christmas Theme.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the command is being executed.</param>
        /// <param name="discordClient">The Discord client instance.</param>
        /// <returns>A <see cref="Task"/> from the method execution.</returns>
        internal static async Task SetChristmasThemeAsync(DiscordChannel discordChannel, DiscordClient discordClient)
            => await SetThemeAsync(discordChannel, discordClient, EmojiTheme.Christmas,
                "🎅🏻🎁 FELIZ NATAL!! ☃️❄️",
                $"O servidor acabou de entrar NO {"CLIMA NATALINO".ToDiscordBold()} 🥳🎄✨. {"BOAS FESTAS À TODOS".ToDiscordBold()}." +
                $"{Environment.NewLine + Environment.NewLine}Tô passando aqui rapinho pra desejar a vocês um {"FELIZ NATAL".ToDiscordBold()}, que vocês tenham" +
                "um NATAL MARAVILHOSO! 🎅 🎁 Que seja cheio de amor, risadas e comidinhas deliciosas, " +
                "belezinha? 🥳🍗" +
                $"{Environment.NewLine + Environment.NewLine}Espero que aproveitem cada momento com a família e os amigos! 🤗💖 Mesmo que seja" +
                "em casa curtindo alguns joguinhos 🎮🎁" +
                "Feliz Natal, meus lindos!!!! 🌟🎉",
                "🎅🏻🎁FELIZ NATAL❄️");

        /// <summary>
        /// Sets the Tramontina server to Easter Theme.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the command is being executed.</param>
        /// <param name="discordClient">The Discord client instance.</param>
        /// <returns>A <see cref="Task"/> from the method execution.</returns>
        internal static async Task SetEasterThemeAsync(DiscordChannel discordChannel, DiscordClient discordClient)
            => await SetThemeAsync(discordChannel, discordClient, EmojiTheme.Easter,
                "🐇🥕 FELIZ PÁSCOA!! 🐣🥚",
                @"O coelhinho da páscoa deu um ""pulo"" no servidor! HAHAHA, PULO.. ESSA FOI BOA 🤭.",
                "🐇FELIZ PÁSCOA🐣");

        /// <summary>
        /// Sets the Tramontina server to Halloween Theme.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the command is being executed.</param>
        /// <param name="discordClient">The Discord client instance.</param>
        /// <returns>A <see cref="Task"/> from the method execution.</returns>
        internal static async Task SetHalloweenThemeAsync(DiscordChannel discordChannel, DiscordClient discordClient)
            => await SetThemeAsync(discordChannel, discordClient, EmojiTheme.Halloween,
                "🕷️🕸️ FELIZ HALLOWEEN!! 🧟👻",
                $"MUAHAHAHAWHWHA. O SERVIDOR ACABA DE ENTRAR EM CLIMA DE TERROR 🕷️🎃. SE PREPAREM PARA O PIOR DO {"MEDO".ToDiscordBold()}.",
                "🎃FELIZ HALLOWEEN👻");

        /// <summary>
        /// Represents seasonal themes that you can set to the Discord server.
        /// </summary>
        internal enum EmojiTheme
        {
            /// <summary>
            /// Represents the Default theme for a server.
            /// </summary>
            Default,

            /// <summary>
            /// Represents the Christmas theme for a setver.
            /// </summary>
            Christmas,

            /// <summary>
            /// Represents the Easter theme for a setver.
            /// </summary>
            Easter,

            /// <summary>
            /// Represents the Halloween theme for a setver.
            /// </summary>
            Halloween
        }
    }
}
