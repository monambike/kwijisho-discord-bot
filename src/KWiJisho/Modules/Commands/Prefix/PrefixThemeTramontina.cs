using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWiJisho.Modules.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands.Prefix
{
    /// <summary>
    /// Provides a set of commands and classes for managing commands in a Discord server.
    /// </summary>
    internal partial class PrefixCommandManager
    {
        /// <summary>
        /// Represents a set of prefix commands to change theme in a Discord server.
        /// </summary>
        internal class PrefixTheme
        {
            /// <summary>
            /// Represents seasonal themes that you can set to the Discord server.
            /// </summary>
            internal enum EmojiTheme { Default, Christmas, Easter, Halloween }

            /// <summary>
            /// Represents a set of prefix commands to change theme designed specifically for Tramontina Discord server.
            /// </summary>
            internal class PrefixThemeTramontina : BaseCommandModule
            {
                // Tramontina's Text Channels.
                internal static readonly TramontinaChannel Geral = new(692588978959941656, "geral", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "💬" }, { EmojiTheme.Christmas, "🍪" }, { EmojiTheme.Easter, "🐇" }, { EmojiTheme.Halloween, "🎃" }  });
                internal static readonly TramontinaChannel PrintsEternizados = new(841452121983418418, "prints-eternizados", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "💾" }, { EmojiTheme.Christmas, "🥛" }, { EmojiTheme.Easter, "🐰" }, { EmojiTheme.Halloween, "👺" }  });
                // Tramontina's Organized Text Channels.
                internal static readonly TramontinaChannel YouTube = new(1142723035447705600, "youtube", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "📹" }, { EmojiTheme.Christmas, "🌟" }, { EmojiTheme.Easter, "🍫" }, { EmojiTheme.Halloween, "🍭" } });
                internal static readonly TramontinaChannel Dicionario = new(1143020466190172220, "dicionario", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "📖" }, { EmojiTheme.Christmas, "⛄" }, { EmojiTheme.Easter, "🥕" }, { EmojiTheme.Halloween, "🔮" } });
                // Tramontina's Bot Channels.
                internal static readonly TramontinaChannel Waifu = new(692591710466998272, "waifu", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "💘" }, { EmojiTheme.Christmas, "💝" }, { EmojiTheme.Easter, "🌷" }, { EmojiTheme.Halloween, "🍬" } });
                internal static readonly TramontinaChannel Radio = new(841136093813538827, "radio", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "📻" }, { EmojiTheme.Christmas, "🎶" }, { EmojiTheme.Easter, "🙏🏻" }, { EmojiTheme.Halloween, "💀" } });
                internal static readonly TramontinaChannel OutrosBots = new(693742473155182663, "outros-bots", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "🤖" }, { EmojiTheme.Christmas, "⛄" }, { EmojiTheme.Easter, "🧺" }, { EmojiTheme.Halloween, "🧟" } });
                // Tramontina's Voice Channels.
                internal static readonly TramontinaChannel CanalEscondidinho = new(1010349376922722436, "Canal Escondidinho", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "🏃🏻💨" }, { EmojiTheme.Christmas, "🎁🧦" }, { EmojiTheme.Easter, "🐣🌱" }, { EmojiTheme.Halloween, "🏰👻" } });
                internal static readonly TramontinaChannel CorpoDeBombeiros1 = new(929778181458767932, "Corpo de Bombeiros 1", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "👨🏻🚒" }, { EmojiTheme.Christmas, "🎅🏻🛷" }, { EmojiTheme.Easter, "🐥🥚" }, { EmojiTheme.Halloween, "🧛🏻🩸" } });
                internal static readonly TramontinaChannel CorpoDeBombeiros2 = new(826257065303474186, "Corpo de Bombeiros 2", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "👩🏻🚒" }, { EmojiTheme.Christmas, "🤶🏻🛷" }, { EmojiTheme.Easter, "🐤🥚" }, { EmojiTheme.Halloween, "🧛🏻🩸" } });
                internal static readonly TramontinaChannel CantinhoDaFofoca = new(692588979404669018, "Cantinho da Fofoca", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "👥💅🏻" }, { EmojiTheme.Christmas, "🍷🍴" }, { EmojiTheme.Easter, "🌸🐝" }, { EmojiTheme.Halloween, "🤡🎈" } });

                /// <summary>
                /// Channels from tramontina server that can receive theme changing.
                /// </summary>
                private static readonly List<TramontinaChannel> TramontinaChannels =
                [
                    Geral, PrintsEternizados, // Tramontina's Text Channels.
                    YouTube, Dicionario, // Tramontina's Organized Text Channels.
                    Waifu, Radio, OutrosBots, // Tramontina's Bot Channels.
                    CanalEscondidinho, CorpoDeBombeiros1, CorpoDeBombeiros2, CantinhoDaFofoca // Tramontina's Voice Channels.
                ];

                /// <summary>
                /// Resets the Tramontina server emojis.
                /// </summary>
                internal PrefixCommand themeReset = new(nameof(themeReset), @"Define o servidor para o tema padrão.", Manage, Permissions.Administrator);
                [Command(nameof(themeReset))]
                internal async Task ResetThemeAsync(CommandContext commandContext)
                    => await SetThemeAsync(commandContext, EmojiTheme.Default,
                        "Voltando ao normal!!",
                        "Voltei o servidor pro seu tema original :D");

                /// <summary>
                /// Sets the Tramontina server to Christmas Theme.
                /// </summary>
                internal PrefixCommand themeChristmas = new(nameof(themeChristmas), @"Define o servidor para o tema de natal.", Manage, Permissions.Administrator);
                [Command(nameof(themeChristmas))]
                [RequireUserPermissions(Permissions.Administrator)]
                internal async Task SetChristmasThemeAsync(CommandContext commandContext)
                    => await SetThemeAsync(commandContext, EmojiTheme.Christmas,
                        "🎅🏻🎁 FELIZ NATAL!! ☃️❄️",
                        $"O servidor acabou de entrar NO {"CLIMA NATALINO".ToDiscordBold()} 🥳🎄✨. {"BOAS FESTAS À TODOS".ToDiscordBold()}." +
                        $"{Environment.NewLine+Environment.NewLine}Tô passando aqui rapinho pra desejar a vocês um {"FELIZ NATAL".ToDiscordBold()}, que vocês tenham" +
                        "um NATAL MARAVILHOSO! 🎅 🎁 Que seja cheio de amor, risadas e comidinhas deliciosas, " +
                        "belezinha? 🥳🍗" +
                        $"{Environment.NewLine+Environment.NewLine}Espero que aproveitem cada momento com a família e os amigos! 🤗💖 Mesmo que seja" +
                        "em casa curtindo alguns joguinhos 🎮🎁" +
                        "Feliz Natal, meus lindos!!!! 🌟🎉",
                        "🎅🏻🎁FELIZ NATAL❄️");

                /// <summary>
                /// Sets the Tramontina server to Easter Theme.
                /// </summary>
                internal PrefixCommand themeEaster = new(nameof(themeEaster), @"Define o servidor para o tema de páscoa.", Manage, Permissions.Administrator);
                [Command(nameof(themeEaster))]
                [RequireUserPermissions(Permissions.Administrator)]
                internal async Task SetEasterThemeAsync(CommandContext commandContext)
                    => await SetThemeAsync(commandContext, EmojiTheme.Easter,
                        "🐇🥕 FELIZ PÁSCOA!! 🐣🥚",
                        @"O coelhinho da páscoa deu um ""pulo"" no servidor! HAHAHA, PULO.. ESSA FOI BOA 🤭.",
                        "🐇FELIZ PÁSCOA🐣");

                /// <summary>
                /// Sets the Tramontina server to Halloween Theme.
                /// </summary>
                internal PrefixCommand themeHalloween = new(nameof(themeHalloween), @"Define o servidor para o tema de halloween.", Manage, Permissions.Administrator);
                [Command(nameof(themeHalloween))]
                [RequireUserPermissions(Permissions.Administrator)]
                internal async Task SetHalloweenThemeAsync(CommandContext commandContext)
                    => await SetThemeAsync(commandContext, EmojiTheme.Halloween,
                        "🕷️🕸️ FELIZ HALLOWEEN!! 🧟👻",
                        $"MUAHAHAHAWHWHA. O SERVIDOR ACABA DE ENTRAR EM CLIMA DE TERROR 🕷️🎃. SE PREPAREM PARA O PIOR DO {"MEDO".ToDiscordBold()}.",
                        "🎃FELIZ HALLOWEEN👻");

                /// <summary>
                /// Sets the Tramontina server to a Theme according with parameterization.
                /// </summary>
                [RequireUserPermissions(Permissions.Administrator)]
                private static async Task SetThemeAsync(CommandContext commandContext, EmojiTheme emojiTheme, string title, string description, string serverNameSuggestion = null)
                {
                    // Admin permission check
                    //if (!await Permission.RequireAdministratorAsync(commandContext.Channel, commandContext.Member)) return;

                    // Initial message so user can know .
                    await commandContext.Channel.SendMessageAsync("Só um segundinho... Vou botar as decorações então pode tomar um tempinho! ;P");

                    // Modifies emoji from every mentioned channel.
                    foreach (var tramontinaChannel in TramontinaChannels)
                        tramontinaChannel.ChangeEmoji(commandContext, tramontinaChannel.EmojiTheme[emojiTheme]);

                    // Presentation discord embed builder (first message).
                    var presentationDiscordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Title = title,
                        Description = $"{description}{Environment.NewLine}",
                        Color = ConfigJson.DefaultColor.DiscordColor
                    }.AddField("HOOOOOOOOOORA DE ENTRAR NO CLIMA", $"Que tal aproveitar e tentar {"trocar o nome do servidor".ToDiscordBold()} pela minha sugestãozinha abaixo? ;D AHAHAHA");

                    // Sending the first message (presentation).
                    await commandContext.Channel.SendMessageAsync(new DiscordMessageBuilder().AddEmbed(presentationDiscordEmbedBuilder));

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
                        Color = ConfigJson.DefaultColor.DiscordColor
                    }.WithImageUrl($"attachment://{imagePath}").Build();

                    // Sending the second message with the image and button.
                    await commandContext.Channel.SendMessageAsync(new DiscordMessageBuilder()
                        .AddEmbed(suggestionsDiscordEmbedBuilder)
                        // The image suggestion for the server.
                        .AddFile(fileName, new FileStream(imagePath, FileMode.Open))
                        // Button to copy server name suggestion.
                        .AddComponents(new DiscordButtonComponent(ButtonStyle.Primary, "copy_server_name_suggestion", "Copiar Sugestão de Nome")));
                }
            }

            /// <summary>
            /// Initializes a new instance of <see cref="TramontinaChannel"/> class with the specified id, the default channel
            /// text title and emoji theme.
            /// </summary>
            /// <param name="id">The Id from the Discord channel.</param>
            /// <param name="defaultTextTitle">The default text title from the Discord channel.</param>
            /// <param name="emojiTheme">The emoji theme dictionary avaiable for this channel.</param>
            internal class TramontinaChannel(ulong id, string defaultTextTitle, Dictionary<EmojiTheme, string> emojiTheme) : Channel(id, $"{defaultTextTitle}")
            {
                /// <summary>
                /// The default text title for the tramontina Discord channel.
                /// </summary>
                internal string DefaultTextTitle { get; set; } = defaultTextTitle;

                /// <summary>
                /// The dictionary containing the EmojiTheme that represents the seasonal theme associate to the emoji, and
                /// the string that represents the emoji itself for the tramontina Discord channel.
                /// </summary>
                internal Dictionary<EmojiTheme, string> EmojiTheme { get; set; } = emojiTheme;

                /// <summary>
                /// Change the emoji for current channel. At the current moment, you can do this twice with the same channel
                /// every 10 minutes.
                /// </summary>
                /// <param name="commandContext">The command context of the interaction.</param>
                /// <param name="emoji">The emoji to be changed in the channel.</param>
                internal void ChangeEmoji(CommandContext commandContext, string emoji) => UpdateChannelNameAsync(commandContext, $"{emoji}│{DefaultTextTitle}");

            }
        }
    }
}
