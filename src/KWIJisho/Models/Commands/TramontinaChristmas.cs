using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace KWIJisho
{
    internal partial class CommandManager
    {
        internal partial class Theme
        {
            internal partial class Tramontina : BaseCommandModule
            {
                public Command themeChristmas = new Command("themeChristmas", @"Define o servidor para o tema de natal. (Só pode ser definido por um administrador)", ThemeGroup);
                [Command(nameof(themeChristmas))]
                public async Task SetChristmasTheme(CommandContext commandContext)
                {
                    foreach (var tramontinaChannel in TramontinaChannels)
                        tramontinaChannel.ChangeEmoji(commandContext, tramontinaChannel.EmojiTheme.Christmas);

                    await commandContext.Channel.SendMessageAsync(new DiscordEmbedBuilder
                    {
                        Title = "🎅🏻🎁 FELIZ NATAL!! ☃️❄️",
                        Description = @"O servidor acabou de entrar NO CLIMA NATALINO 🥳. BOAS FESTAS À TODOS.",
                        Color = ConfigJson.ConfigJsonPurpleColor.DiscordColor,
                        ImageUrl = "Resources/Images/Tramontina/128x128-mello-christmas.png"
                    });
                }
            }
        }
    }
}
