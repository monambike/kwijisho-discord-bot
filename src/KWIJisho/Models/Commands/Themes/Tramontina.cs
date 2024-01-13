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
            /// <summary>
            /// Class that represents a group or channel inside "Tramontina" server
            /// </summary>
            internal class Tramontina : BaseCommandModule
            {
                private static TramontinaChannel CanalEscondidinho = new TramontinaChannel(1010349376922722436, "Canal Escondidinho", "🏃🏻💨");


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

                public virtual void SetEasterTheme()
                {

                }

                public Command themeChristmas = new Command("themeChristmas", @"Define o servidor para o tema de natal. (Só pode ser definido por um administrador)", ThemeGroup);
                [Command(nameof(themeChristmas))]
                public async Task SetChristmasTheme(CommandContext commandContext)
                {
                    CanalEscondidinho.ChangeEmoji(commandContext, "🎁🧦");

                    var discordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Color = new DiscordColor(77, 18, 161),
                        Title = "🎅🏻🎁 FELIZ NATAL!!",
                        Description = @"O servidor acabou de entrar NO CLIMA NATALINO 🥳. BOAS FESTAS À TODOS."
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
