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
                    Geral.ChangeEmoji(commandContext, "🍪");
                    PrintsEternizados.ChangeEmoji(commandContext, "🥛");
                    YouTube.ChangeEmoji(commandContext, "🌟");
                    Dicionario.ChangeEmoji(commandContext, "⛄");
                    Waifu.ChangeEmoji(commandContext, "💝");
                    Radio.ChangeEmoji(commandContext, "🎶");
                    OutrosBots.ChangeEmoji(commandContext, "⛄");
                    CanalEscondidinho.ChangeEmoji(commandContext, "🎁🧦");
                    CorpoDeBombeiros1.ChangeEmoji(commandContext, "🎅🏻🛷");
                    CorpoDeBombeiros2.ChangeEmoji(commandContext, "🤶🏻🛷");
                    CantinhoDaFofoca.ChangeEmoji(commandContext, "🍷🍴");

                    var discordEmbedBuilder = new DiscordEmbedBuilder
                    {
                        Color = ConfigJson.ConfigJsonPurpleColor.DiscordColor,
                        Title = "🎅🏻🎁 FELIZ NATAL!!",
                        Description = @"O servidor acabou de entrar NO CLIMA NATALINO 🥳. BOAS FESTAS À TODOS."
                    };
                    await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
                }
            }
        }
    }
}
