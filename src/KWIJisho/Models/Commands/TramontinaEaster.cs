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
                public Command themeEaster = new Command("themeEaster", @"Define o servidor para o tema de páscoa. (Só pode ser definido por um administrador)", ThemeGroup);
                [Command(nameof(themeEaster))]
                public async Task SetEasterTheme(CommandContext commandContext)
                {
                    var discordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Title = "🎅🏻🎁 FELIZ NATAL!!",
                        Description = @"O servidor acabou de entrar NO CLIMA NATALINO 🥳. BOAS FESTAS À TODOS.",
                        Color = ConfigJson.ConfigJsonPurpleColor.DiscordColor,
                    };

                    foreach (var tramontinaChannel in TramontinaChannels)
                        tramontinaChannel.ChangeEmoji(commandContext, tramontinaChannel.EmojiTheme.Christmas);

                    await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
                }
            }
        }
    }
}
