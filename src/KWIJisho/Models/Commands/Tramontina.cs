using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KWIJisho
{
    internal partial class CommandManager
    {
        internal partial class Theme
        {
            internal partial class Tramontina : BaseCommandModule
            {
                // CANAIS DE TEXTO
                private static readonly TramontinaChannel Geral = new TramontinaChannel(692588978959941656, "geral", "💬", new EmojiTheme(
                    christmas: "🍪", easter: "🐇", halloween: "🎃"));
                private static readonly TramontinaChannel PrintsEternizados = new TramontinaChannel(841452121983418418, "prints-eternizados", "💾", new EmojiTheme(
                    christmas:"🥛", easter: "🐰", halloween: "👺"));
                // CANAIS DE TEXTO ORGANIZADO
                private static readonly TramontinaChannel YouTube = new TramontinaChannel(1142723035447705600, "youtube", "📹", new EmojiTheme(
                    christmas: "🌟", easter: "🍫", halloween: "🍭"));
                private static readonly TramontinaChannel Dicionario = new TramontinaChannel(1143020466190172220, "dicionario", "📖", new EmojiTheme(
                    christmas: "⛄", easter: "🥕", halloween: "🔮"));
                // CANAIS DE BOT
                private static readonly TramontinaChannel Waifu = new TramontinaChannel(692591710466998272, "waifu", "💘", new EmojiTheme(
                    christmas: "💝", easter: "🌷", halloween: "🍬"));
                private static readonly TramontinaChannel Radio = new TramontinaChannel(841136093813538827, "radio", "📻", new EmojiTheme(
                    christmas: "🎶", easter: "🙏🏻", halloween: "💀"));
                private static readonly TramontinaChannel OutrosBots = new TramontinaChannel(693742473155182663, "outros-bots", "🤖", new EmojiTheme(
                    christmas: "⛄", easter: "🧺", halloween: "🧟"));
                // CANAIS DE VOZ
                private static readonly TramontinaChannel CanalEscondidinho = new TramontinaChannel(1010349376922722436, "Canal Escondidinho", "🏃🏻💨", new EmojiTheme(
                    christmas: "🎁🧦", easter: "🐣🌱", halloween: "🏰👻"));
                private static readonly TramontinaChannel CorpoDeBombeiros1 = new TramontinaChannel(929778181458767932, "Corpo de Bombeiros 1", "👨🏻🚒", new EmojiTheme(
                    christmas: "🎅🏻🛷", easter: "🐥🥚", halloween: "🧛🏻🩸"));
                private static readonly TramontinaChannel CorpoDeBombeiros2 = new TramontinaChannel(826257065303474186, "Corpo de Bombeiros 2", "👩🏻🚒", new EmojiTheme(
                    christmas: "🤶🏻🛷", easter: "🐤🥚", halloween: "🧛🏻🩸"));
                private static readonly TramontinaChannel CantinhoDaFofoca = new TramontinaChannel(692588979404669018, "Cantinho da Fofoca", "👥💅🏻", new EmojiTheme(
                    christmas: "🍷🍴", easter: "🌸🐝", halloween: "🤡🎈"));

                private static readonly List<TramontinaChannel> TramontinaChannels = new List<TramontinaChannel>
                {
                    Geral,
                    PrintsEternizados,
                    YouTube,
                    Dicionario,
                    Waifu,
                    Radio,
                    OutrosBots,
                    CanalEscondidinho,
                    CorpoDeBombeiros1,
                    CorpoDeBombeiros2,
                    CantinhoDaFofoca
                };

                public Command themeReset = new Command("themeReset", @"Define o servidor para o tema padrão. (Só pode ser definido por um administrador)", ThemeGroup);
                [Command(nameof(themeReset))]
                public async Task ResetTheme(CommandContext commandContext)
                {
                    foreach (var channel in TramontinaChannels)
                        channel.ResetToDefaultEmoji(commandContext);

                    var discordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Color = ConfigJson.DefaultColor.DiscordColor,
                        Title = "Voltando ao normal!!",
                        Description = @"Voltei o servidor pro seu tema original :D"
                    };
                    await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
                }
            }

            internal class Channel
            {
                public ulong Id { get; private set; }

                public string DefaultName { get; set; }

                public Channel(ulong id, string defaultName)
                {
                    Id = id;
                    DefaultName = defaultName;
                }

                public async void UpdateChannelName(CommandContext commandContext, string newName)
                {
                    var channel = commandContext.Client.GetChannelAsync(Id).Result;
                    // Rename the channel
                    await channel.ModifyAsync(editChannel => editChannel.Name = $"{newName}");
                }
            }

            internal class TramontinaChannel : Channel
            {
                internal string DefaultTextTitle { get; set; }

                internal string DefaultEmoji { get; set; }

                internal EmojiTheme EmojiTheme { get; set; }

                internal TramontinaChannel(ulong id, string defaultTextTitle, string defaultEmoji, EmojiTheme emojiTheme) : base(id, $"{defaultEmoji}│{defaultTextTitle}")
                {
                    DefaultTextTitle = defaultTextTitle;
                    DefaultEmoji = defaultEmoji;
                    EmojiTheme = emojiTheme;
                }

                internal void ResetToDefaultEmoji(CommandContext commandContext) => UpdateChannelName(commandContext, $"{DefaultEmoji}│{DefaultTextTitle}");

                internal void ChangeEmoji(CommandContext commandContext, string emoji) => UpdateChannelName(commandContext, $"{emoji}│{DefaultTextTitle}");

            }

            internal class EmojiTheme
            {
                internal string Christmas { get; set; }
                internal string Easter { get; set; }
                internal string Halloween { get; set; }

                public EmojiTheme(string christmas, string easter, string halloween)
                {
                    Christmas = christmas;
                    Easter = easter;
                    Halloween = halloween;
                }
            }
        }
    }
}
