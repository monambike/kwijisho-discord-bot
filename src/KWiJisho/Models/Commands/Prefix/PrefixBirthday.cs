using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixBirthday : BaseCommandModule
        {
            internal PrefixCommand nextBirthday = new(nameof(nextBirthday), $@"Mostra de quem o aniversário tá mais perto.", Birthday);
            [Command(nameof(nextBirthday))]
            internal async Task GetNextBirthdayAsync(CommandContext commandContext)
            {
                var user = Utils.Birthday.GetUserByClosestBirthday();
                var userInfo = commandContext.Guild.GetMemberAsync(user.Id).Result;
                var message = Utils.Birthday.GetUpcomingBirthdayDateByClosestBirthday();

                // Getting image name and image's full path
                var fileName = $"500x281-talking.gif";
                var imagePath = Path.GetFullPath($"Resources/Images/{fileName}");

                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Title = "PRÓXIMO ANIVERSARIANTE",
                    Description = $@"O próximo aniversariante é.. {userInfo.Username}!! {message}"
                }
                .WithImageUrl($"attachment://{imagePath}").Build();

                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }

            private Dictionary<int, string> months = new()
            {
                { 01, "Janeiro" },
                { 02, "Fevereiro" },
                { 03, "Março" },
                { 04, "Abril" },
                { 05, "Maio" },
                { 06, "Junho" },
                { 07, "Julho" },
                { 08, "Agosto" },
                { 09, "Setembro" },
                { 10, "Outubro" },
                { 11, "Novembro" },
                { 12, "Dezembro" }
            };

            internal PrefixCommand listBirthday = new(nameof(listBirthday), $@"Mostra a lista de aniversariantes.", Birthday);
            [Command(nameof(listBirthday))]
            internal async Task GetListBirthdayAsync(CommandContext commandContext)
            {
                var users = Utils.Birthday.GetUsersByClosestBirthday();

                // Getting image name and image's full path
                var fileName = $"500x281-talking.gif";
                var imagePath = Path.GetFullPath($"Resources/Images/{fileName}");

                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Title = "LISTA DE ANIVERSARIANTES",
                };

                // Adding a field for every month that someone makes birthday
                foreach (var month in months)
                {
                    var usersBirthdayThisMonth = users.Where(user => user.Birthday.Month == month.Key);

                    // Building a string that will hold all users that will make birthday
                    // at current month
                    var usersBirthdayThisMonthString = string.Empty;
                    foreach (var userBirthdayThisMonth in usersBirthdayThisMonth)
                    {
                        var userInfo = userBirthdayThisMonth.GetUserDiscordMember(commandContext);
                        if (userInfo is not null)
                        {
                            // Formatting user name
                            var name = (userInfo.DisplayName == userInfo.Username) ? userInfo.DisplayName : $"{userInfo.DisplayName} ({ userInfo.Username})";
                            // Formatting user field with name and birthday month and day
                            usersBirthdayThisMonthString += $"{userBirthdayThisMonth.Birthday:dd/MM} - {name}{Environment.NewLine}";
                        }
                    }

                    // If wasn't found no users or there's no birthday users this month, go to the next month
                    if (string.IsNullOrEmpty(usersBirthdayThisMonthString)) continue;

                    // Adding field with all birthday users of this month
                    discordEmbedBuilder.AddField($"Aniversariantes de {month.Value.ToUpper()}", usersBirthdayThisMonthString);
                }
                await commandContext.Channel.SendMessageAsync(discordEmbedBuilder);
            }
        }
    }
}
