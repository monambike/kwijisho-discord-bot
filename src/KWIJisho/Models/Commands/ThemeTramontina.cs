using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWIJisho.Models.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace KWIJisho.Models.Commands
{
    internal partial class CommandManager
    {
        internal partial class Theme
        {
            internal partial class Tramontina : BaseCommandModule
            {
                // Text Channels
                private static readonly TramontinaChannel Geral = new TramontinaChannel(692588978959941656, "geral", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "💬" }, { EmojiTheme.Christmas, "🍪" }, { EmojiTheme.Easter, "🐇" }, { EmojiTheme.Halloween, "🎃" }  });
                private static readonly TramontinaChannel PrintsEternizados = new TramontinaChannel(841452121983418418, "prints-eternizados", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "💾" }, { EmojiTheme.Christmas, "🥛" }, { EmojiTheme.Easter, "🐰" }, { EmojiTheme.Halloween, "👺" }  });
                // Organized Text Channels
                private static readonly TramontinaChannel YouTube = new TramontinaChannel(1142723035447705600, "youtube", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "📹" }, { EmojiTheme.Christmas, "🌟" }, { EmojiTheme.Easter, "🍫" }, { EmojiTheme.Halloween, "🍭" } });
                private static readonly TramontinaChannel Dicionario = new TramontinaChannel(1143020466190172220, "dicionario", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "📖" }, { EmojiTheme.Christmas, "⛄" }, { EmojiTheme.Easter, "🥕" }, { EmojiTheme.Halloween, "🔮" } });
                // Bot Channels
                private static readonly TramontinaChannel Waifu = new TramontinaChannel(692591710466998272, "waifu", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "💘" }, { EmojiTheme.Christmas, "💝" }, { EmojiTheme.Easter, "🌷" }, { EmojiTheme.Halloween, "🍬" } });
                private static readonly TramontinaChannel Radio = new TramontinaChannel(841136093813538827, "radio", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "📻" }, { EmojiTheme.Christmas, "🎶" }, { EmojiTheme.Easter, "🙏🏻" }, { EmojiTheme.Halloween, "💀" } });
                private static readonly TramontinaChannel OutrosBots = new TramontinaChannel(693742473155182663, "outros-bots", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "🤖" }, { EmojiTheme.Christmas, "⛄" }, { EmojiTheme.Easter, "🧺" }, { EmojiTheme.Halloween, "🧟" } });
                // Voice Channels
                private static readonly TramontinaChannel CanalEscondidinho = new TramontinaChannel(1010349376922722436, "Canal Escondidinho", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "🏃🏻💨" }, { EmojiTheme.Christmas, "🎁🧦" }, { EmojiTheme.Easter, "🐣🌱" }, { EmojiTheme.Halloween, "🏰👻" } });
                private static readonly TramontinaChannel CorpoDeBombeiros1 = new TramontinaChannel(929778181458767932, "Corpo de Bombeiros 1", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "👨🏻🚒" }, { EmojiTheme.Christmas, "🎅🏻🛷" }, { EmojiTheme.Easter, "🐥🥚" }, { EmojiTheme.Halloween, "🧛🏻🩸" } });
                private static readonly TramontinaChannel CorpoDeBombeiros2 = new TramontinaChannel(826257065303474186, "Corpo de Bombeiros 2", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "👩🏻🚒" }, { EmojiTheme.Christmas, "🤶🏻🛷" }, { EmojiTheme.Easter, "🐤🥚" }, { EmojiTheme.Halloween, "🧛🏻🩸" } });
                private static readonly TramontinaChannel CantinhoDaFofoca = new TramontinaChannel(692588979404669018, "Cantinho da Fofoca", new Dictionary<EmojiTheme, string>
                    { { EmojiTheme.Default, "👥💅🏻" }, { EmojiTheme.Christmas, "🍷🍴" }, { EmojiTheme.Easter, "🌸🐝" }, { EmojiTheme.Halloween, "🤡🎈" } });

                /// <summary>
                /// Channels from tramontina server that can receive theme changing.
                /// </summary>
                private static readonly List<TramontinaChannel> TramontinaChannels = new List<TramontinaChannel>
                {
                    Geral, PrintsEternizados, // Text Channels
                    YouTube, Dicionario, // Organized Text Channels
                    Waifu, Radio, OutrosBots, // Bot Channels
                    CanalEscondidinho, CorpoDeBombeiros1, CorpoDeBombeiros2, CantinhoDaFofoca // Voice Channels
                };

                /// <summary>
                /// Resets the Tramontina server emojis.
                /// </summary>
                internal Command themeReset = new Command("themeReset", @"Define o servidor para o tema padrão.", ThemeGroup, true);
                [Command(nameof(themeReset))]
                internal async Task ResetTheme(CommandContext commandContext)
                    => await SetTheme(commandContext, EmojiTheme.Default,
                        "Voltando ao normal!!",
                        "Voltei o servidor pro seu tema original :D");

                /// <summary>
                /// Sets the Tramontina server to Christmas Theme.
                /// </summary>
                internal Command themeChristmas = new Command("themeChristmas", @"Define o servidor para o tema de natal.", ThemeGroup, true);
                [Command(nameof(themeChristmas))]
                internal async Task SetChristmasTheme(CommandContext commandContext)
                    => await SetTheme(commandContext, EmojiTheme.Christmas,
                        "🎅🏻🎁 FELIZ NATAL!! ☃️❄️",
                        "O servidor acabou de entrar NO CLIMA NATALINO 🥳. BOAS FESTAS À TODOS.",
                        "🎅🏻🎁FELIZ NATAL❄️");

                /// <summary>
                /// Sets the Tramontina server to Easter Theme.
                /// </summary>
                internal Command themeEaster = new Command("themeEaster", @"Define o servidor para o tema de páscoa.", ThemeGroup, true);
                [Command(nameof(themeEaster))]
                internal async Task SetEasterTheme(CommandContext commandContext)
                    => await SetTheme(commandContext, EmojiTheme.Easter,
                        "🐇🥕 FELIZ PÁSCOA!! 🐣🥚",
                        @"O coelhinho da páscoa deu um ""pulo"" no servidor! HAHAHA, PULO.. ESSA FOI BOA 🤭.",
                        "🐇FELIZ PÁSCOA🐣");

                /// <summary>
                /// Sets the Tramontina server to Halloween Theme.
                /// </summary>
                internal Command themeHalloween = new Command("themeHalloween", @"Define o servidor para o tema de halloween.", ThemeGroup, true);
                [Command(nameof(themeHalloween))]
                internal async Task SetHalloweenTheme(CommandContext commandContext)
                    => await SetTheme(commandContext, EmojiTheme.Halloween,
                        "🕷️🕸️ FELIZ HALLOWEEN!! 🧟👻",
                        "MUAHAHAHAWHWHA. O SERVIDOR ACABA DE ENTRAR EM CLIMA DE TERROR 🕷️🎃. SE PREPAREM PARA O PIOR DO **MEDO**.",
                        "🕷️FELIZ HALLOWEEN👻");

                /// <summary>
                /// Sets the Tramontina server to a Theme according with parameterization.
                /// </summary>
                private async Task SetTheme(CommandContext commandContext, EmojiTheme emojiTheme, string title, string description, string serverNameSuggestion = null)
                {
                    // Initial message so user can know 
                    await commandContext.Channel.SendMessageAsync("Só um segundinho... Vou botar as decorações então pode tomar um tempinho! ;P");

                    // Modifies emoji from every mentioned channel
                    //foreach (var tramontinaChannel in TramontinaChannels)
                    //    tramontinaChannel.ChangeEmoji(commandContext, tramontinaChannel.EmojiTheme[emojiTheme]);

                    // Send a message to show conclusion and deliever a suggested server name and picture
                    string nomeDoServidor = string.IsNullOrEmpty(serverNameSuggestion) ? "Tramontina│Bizarre Adventures" : $"{serverNameSuggestion} - Tramontina│Bizarre Adventures";

                    var fileName = $"128x128-mello-{emojiTheme.ToString().ToLower()}.png";
                    var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");
                    // Message body
                    var firstDiscordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Title = title,
                        Description = $"{description}{Environment.NewLine}",
                        Color = ConfigJson.DefaultColor.DiscordColor
                    };

                    // Message body
                    var secondDiscordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Title = "TROQUE O NOME DO SERVER",
                        Description = $"Que tal aproveitar e tentar **trocar o nome do servidor** pela minha sugestãozinha abaixo? ;D"
                        + $"{Environment.NewLine}Sugestão de Nome do Servidor: `{nomeDoServidor}`",
                        Color = ConfigJson.DefaultColor.DiscordColor
                    }.WithImageUrl($"attachment://{imagePath}").Build();

                    // Button to copy server name suggestion
                    var button = new DiscordButtonComponent(ButtonStyle.Primary, "copy_server_name_suggestion", "Copiar Sugestão de Nome");

                    // Sending the first message
                    await commandContext.Channel.SendMessageAsync(new DiscordMessageBuilder()
                        .AddEmbed(firstDiscordEmbedBuilder));

                    // Sending the second message with the image and button
                    await commandContext.Channel.SendMessageAsync(new DiscordMessageBuilder()
                        .AddEmbed(secondDiscordEmbedBuilder)
                        .AddFile(fileName, new FileStream(imagePath, FileMode.Open))
                        .AddComponents(button));
                }
            }

            internal class TramontinaChannel : Channel
            {
                internal string DefaultTextTitle { get; set; }

                internal Dictionary<EmojiTheme, string> EmojiTheme { get; set; }

                internal TramontinaChannel(ulong id, string defaultTextTitle, Dictionary<EmojiTheme, string> emojiTheme) : base(id, $"{defaultTextTitle}")
                {
                    DefaultTextTitle = defaultTextTitle;
                    EmojiTheme = emojiTheme;
                }

                internal void ChangeEmoji(CommandContext commandContext, string emoji) => UpdateChannelName(commandContext, $"{emoji}│{DefaultTextTitle}");

            }
        }
    }
}
