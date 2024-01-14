using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace KWIJisho
{
    internal partial class CommandManager
    {
        internal partial class Info : BaseCommandModule
        {
            public static string furtherHelpDetailsMessage = @" Para receber detalhes sobre um comando digite ""help <nome do comando>"".";
            public Command help = new Command("help", $@"Mostra a ajuda.{furtherHelpDetailsMessage}", InfoGroup);
            [Command(nameof(help))]
            internal async Task GetHelp(CommandContext commandContext)
            {
                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Title = "AJUDA COM COMANDOS",
                    Description = $@"Lembre-se que pra colocar um comando você precisa colocar o ""!"" na frente!{furtherHelpDetailsMessage}",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                    {
                        Url = commandContext.Client.CurrentUser.AvatarUrl
                    }
                };

                foreach (var commandGroup in CommandGroups)
                {
                    string content = "";
                    foreach (var discordCommand in commandGroup.Commands)
                        content += $"**{ConfigJson.Prefix}{discordCommand.Name}:** {discordCommand.Description}{Environment.NewLine}";
                    discordEmbedBuilder.AddField(commandGroup.Name, content);
                }

                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }

            public Command info = new Command("info", @"Mostra informações básicas sobre mim e o meu criador.", InfoGroup);
            [Command(nameof(info))]
            public async Task GetInfo(CommandContext commandContext)
            {
                var message = @"Quem me criou foi o @monambike, você pode conferir o site dele em https://monambike.com.";
                await commandContext.Channel.SendMessageAsync(message);
            }
        }
    }
}
