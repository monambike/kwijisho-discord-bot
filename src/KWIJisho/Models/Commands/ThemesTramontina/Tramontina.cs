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

                private static TramontinaChannel Geral = new TramontinaChannel(692588978959941656, "geral", "💬");
                private static TramontinaChannel PrintsEternizados = new TramontinaChannel(841452121983418418, "prints-eternizados", "💾");

                private static TramontinaChannel YouTube = new TramontinaChannel(1142723035447705600, "youtube", "📹");
                private static TramontinaChannel Dicionario = new TramontinaChannel(1143020466190172220, "dicionario", "📖");

                private static TramontinaChannel Waifu = new TramontinaChannel(692591710466998272, "waifu", "💘");
                private static TramontinaChannel Radio = new TramontinaChannel(841136093813538827, "radio", "📻");
                private static TramontinaChannel OutrosBots = new TramontinaChannel(693742473155182663, "outros-bots", "🤖");

                private static TramontinaChannel CanalEscondidinho = new TramontinaChannel(1010349376922722436, "Canal Escondidinho", "🏃🏻💨");
                private static TramontinaChannel CorpoDeBombeiros1 = new TramontinaChannel(929778181458767932, "Corpo de Bombeiros 1", "👨🏻🚒");
                private static TramontinaChannel CorpoDeBombeiros2 = new TramontinaChannel(826257065303474186, "Corpo de Bombeiros 2", "👩🏻🚒");
                private static TramontinaChannel CantinhoDaFofoca = new TramontinaChannel(692588979404669018, "Cantinho da Fofoca", "👥💅🏻");


                public Command themeReset = new Command("themeReset", @"Define o servidor para o tema padrão. (Só pode ser definido por um administrador)", ThemeGroup);
                [Command(nameof(themeReset))]
                public async Task ResetTheme(CommandContext commandContext)
                {
                    CanalEscondidinho.ResetToDefaultEmoji(commandContext);

                    var discordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Color = new DiscordColor(77, 18, 161),
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
                public string DefaultTextTitle { get; set; }

                public string DefaultEmoji { get; set; }

                public TramontinaChannel(ulong id, string defaultTextTitle, string defaultEmoji) : base(id, $"{defaultEmoji}│{defaultTextTitle}")
                {
                    DefaultTextTitle = defaultTextTitle;
                    DefaultEmoji = defaultEmoji;
                }

                public void ResetToDefaultEmoji(CommandContext commandContext) => UpdateChannelName(commandContext, $"{DefaultEmoji}│{DefaultTextTitle}");

                public void ChangeEmoji(CommandContext commandContext, string emoji) => UpdateChannelName(commandContext, $"{emoji}│{DefaultTextTitle}");
            }
        }
    }
}
