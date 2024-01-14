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
                public Command themeHalloween = new Command("themeHalloween", @"Define o servidor para o tema de halloween. (Só pode ser definido por um administrador)", ThemeGroup);
                [Command(nameof(themeHalloween))]
                public async Task SetHalloweenTheme(CommandContext commandContext)
                {
                    foreach (var tramontinaChannel in TramontinaChannels)
                        tramontinaChannel.ChangeEmoji(commandContext, tramontinaChannel.EmojiTheme.Halloween);

                    await commandContext.Channel.SendMessageAsync(new DiscordEmbedBuilder
                    {
                        Title = "🕷️🕸️ FELIZ HALLOWEEN!! 🧟👻",
                        Description = @"MUAHAHAHAWHWHA. O SERVIDOR ACABA DE ENTRAR EM CLIMA DE TERROR 🕷️🎃. SE PREPAREM PARA O PIOR DO **MEDO**.",
                        Color = ConfigJson.ConfigJsonPurpleColor.DiscordColor,
                    });
                }
            }
        }
    }
}
