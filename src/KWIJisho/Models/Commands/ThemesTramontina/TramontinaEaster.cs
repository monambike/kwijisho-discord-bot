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
                        Color = new DiscordColor(77, 18, 161),
                        Title = "🎅🏻🎁 FELIZ NATAL!!",
                        Description = @"O servidor acabou de entrar NO CLIMA NATALINO 🥳. BOAS FESTAS À TODOS."
                    };

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

                    await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
                }
            }
        }
    }
}
