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
                    foreach (var tramontinaChannel in TramontinaChannels)
                        tramontinaChannel.ChangeEmoji(commandContext, tramontinaChannel.EmojiTheme.Easter);

                    await commandContext.Channel.SendMessageAsync(new DiscordEmbedBuilder
                    {
                        Title = "🐇🥕 FELIZ PÁSCOA!! 🐣🥚",
                        Description = @"O coelhinho da páscoa deu um ""pulo"" no servidor! HAHAHA, PULO.. ESSA FOI BOA 🤭.",
                        Color = ConfigJson.DefaultColor.DiscordColor,
                    });
                }
            }
        }
    }
}
