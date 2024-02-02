using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Commands
{
    /// <summary>
    /// Provides methods for "prefix" and "slash" birthday commands.
    /// </summary>
    internal static class Birthday
    {
        private static Dictionary<int, string> Months => new()
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

        internal static async Task GetNextBirthdayAsync(DiscordChannel discordChannel, DiscordGuild discordGuild)
        {

            var discordUser = Utils.Birthday.TryGetDiscordUserByClosestBirthday(discordGuild);
            var discordMember = discordUser.GetUserDiscordMember(discordGuild);

            var daysRemaining = Utils.Birthday.GetDaysRemainingByUser(discordUser);
            var upcomingDate = Utils.Birthday.GetUpcomingBirthdayDate(daysRemaining);
            var message = Utils.Birthday.GenerateBirthdayMessage(discordUser);

            // Getting image name and image's full path.
            var fileName = $"500x500-happybirthday-{upcomingDate.ToString().ToLower()}.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Birthday/{fileName}");

            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = ConfigJson.DefaultColor.DiscordColor,
                Title = "PRÓXIMO ANIVERSARIANTE",
                Description = $@"O próximo aniversariante é.. {discordMember.Username}!! {message}"
            }
            .WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            await discordChannel.SendMessageAsync(new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                // The image gif of karen kujou happy talking.
                .AddFile(fileName, fileStream));
        }

        internal static async Task GetListBirthdayAsync(DiscordChannel discordChannel, DiscordGuild discordGuild)
        {
            var users = Utils.Birthday.GetUsersOrderByClosestBirthday();

            // Getting image name and image's full path.
            var fileName = $"500x281-talking.gif";
            var imagePath = Path.GetFullPath($"Resources/Images/{fileName}");

            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = ConfigJson.DefaultColor.DiscordColor,
                Title = "LISTA DE ANIVERSARIANTES",
            };

            // Adding a field for every month that someone makes birthday.
            foreach (var month in Months)
            {
                var usersBirthdayThisMonth = users.Where(user => user.Birthday.Month == month.Key);

                // Building a string that will hold all users that will make birthday
                // at current month.
                var usersBirthdayThisMonthString = string.Empty;
                foreach (var userBirthdayThisMonth in usersBirthdayThisMonth)
                {
                    var userInfo = userBirthdayThisMonth.GetUserDiscordMember(discordGuild);
                    if (userInfo is not null)
                    {
                        // Formatting user name
                        var name = (userInfo.DisplayName == userInfo.Username) ? userInfo.DisplayName : $"{userInfo.DisplayName} ({userInfo.Username})";
                        // Formatting user field with name and birthday month and day.
                        usersBirthdayThisMonthString += $"{userBirthdayThisMonth.Birthday:dd/MM} - {name}{Environment.NewLine}";
                    }
                }

                // If wasn't found no users or there's no birthday users this month, go to the next month.
                if (string.IsNullOrEmpty(usersBirthdayThisMonthString)) continue;

                // Adding field with all birthday users of this month.
                discordEmbedBuilder.AddField($"Aniversariantes de {month.Value.ToUpper()}", usersBirthdayThisMonthString);
            }

            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }
    }
}
