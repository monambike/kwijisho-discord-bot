using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace KWIJisho
{
    internal partial class Commands
    {
        internal partial class Info : BaseCommandModule
        {
            [Command("info")]
            public async Task GetInfo(CommandContext commandContext)
            {
                var message = @"Quem me criou foi o @monambike, você pode conferir o site dele em https://monambike.com.";
                _ = await commandContext.Channel.SendMessageAsync(message).ConfigureAwait(false);
            }

            public Command help = new Command("help", @"Mostra a ajuda. Para receber detalhes sobre um comando digite ""help <nome do comando>""");
            [Command(nameof(help))]
            internal async Task GetHelp(CommandContext commandContext)
            {
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Color = new DiscordColor(77, 18, 161),
                    Title = "AJUDA COM COMANDOS",
                    Description = @"Lembre-se que pra colocar um comando você precisa colocar o ""!"" na frente!"
                    + @"Para receber detalhes sobre um comando digite ""help <nome do comando>""",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                    {
                        Url = commandContext.Client.CurrentUser.AvatarUrl
                    }
                };

                foreach (var discordCommand in DiscordCommands)
                    discordEmbedBuilder.AddField(discordCommand.Name, discordCommand.Description);

                _ = await commandContext.Channel.SendMessageAsync(discordEmbedBuilder).ConfigureAwait(false);
            }
        }
    }
}
