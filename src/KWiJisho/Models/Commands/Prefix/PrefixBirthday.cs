using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixBirthday : BaseCommandModule
        {

            internal PrefixCommand nextBirthday = new(nameof(nextBirthday), $@"Mostra de quem o aniversário tá mais perto.", BirthdayGroup);
            [Command(nameof(nextBirthday))]
            internal async Task GetNextBirthdayAsync(CommandContext commandContext)
            {
                var user = Utils.Birthday.GetUserByClosestBirthday();
                var message = Utils.Birthday.GetUpcomingBirthdayDateByClosestBirthday();

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

            string[] months =
            {
                "Janeiro",
                "Fevereiro",
                "Março",
                "Abril",
                "Maio",
                "Junho",
                "Julho",
                "Agosto",
                "Setembro",
                "Outubro",
                "Novembro",
                "Dezembro"
            };

            enum teste
            {
                
                Janeiro = 01,
                Fevereiro = 02,
                Março = 03,
                Abril = 04,
                Maio = 05,
                Junho = 06,
                Julho = 07,
                Agosto = 08,
                Setembro = 09,
                Outubro = 10,
                Novembro = 11,
                Dezembro = 12,
            }

            internal PrefixCommand listBirthday = new(nameof(listBirthday), $@"Mostra a lista de aniversariantes.", BirthdayGroup);
            [Command(nameof(listBirthday))]
            internal async Task GetListBirthdayAsync(CommandContext commandContext)
            {
                var users = Utils.Birthday.GetUsersByClosestBirthday();
                var message = Utils.Birthday.GetUpcomingBirthdayDateByClosestBirthday();

                // Getting image name and image's full path
                var fileName = $"500x281-talking.gif";
                var imagePath = Path.GetFullPath($"Resources/Images/{fileName}");

                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Title = "LISTA DE ANIVERSARIANTES",
                };

                //foreach ( var month in months)
                //{
                //    var monthName = GetMonthName(month);
                //    var thisUsers = users.Where(user => user.Birthday.Month == month);
                //    foreach (var thisUser in thisUsers)
                //        discordEmbedBuilder.AddField(monthName, thisUser.Username);
                //}
                //await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }
        }
    }
}
