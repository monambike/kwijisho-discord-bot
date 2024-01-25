using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.IO;
using System.Threading.Tasks;

namespace KWIJisho.Models.Commands
{
    internal partial class CommandManager
    {
        internal class Birthday : BaseCommandModule
        {

            internal Command nextBirthday = new(nameof(nextBirthday), $@"Mostra de quem o aniversário tá mais perto.", BirthdayGroup);
            [Command(nameof(nextBirthday))]
            internal async Task GetNextBirthdayAsync(CommandContext commandContext)
            {
                var user = Utils.Birthday.GetUserByClosestBirthday();
                var message = Utils.Birthday.Test();

                // Getting image name and image's full path
                var fileName = $"500x281-talking.gif";
                var imagePath = Path.GetFullPath($"Resources/Images/{fileName}");

                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Title = "PRÓXIMO ANIVERSARIANTE",
                    Description = $@"O próximo aniversariante é.. {user.Username}!! {message}"
                }
                .WithImageUrl($"attachment://{imagePath}").Build();
                
                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }
        }
    }
}
