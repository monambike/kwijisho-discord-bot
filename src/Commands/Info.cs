using DSharpPlus;
using DSharpPlus.Entities;
using KWiJisho.Commands.Prefix;
using KWiJisho.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Commands
{
    /// <summary>
    /// Provides methods for info prefix and slash commands.
    /// </summary>
    internal static class Info
    {
        /// <summary>
        /// Sends a help message containing a list of available commands and their descriptions to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the message will be sent.</param>
        /// <param name="discordClient">The Discord client instance.</param>
        internal static async Task ExecuteHelpAsync(DiscordChannel discordChannel, DiscordClient discordClient)
        {
            // Initializing discord embed builder
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = Data.ConfigJson.DefaultColor.DiscordColor,
                Title = "AJUDA COM COMANDOS",
                Description = $@"Lembre-se que pra colocar um comando você precisa colocar o ""!"" na frente!",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = discordClient.CurrentUser.AvatarUrl
                }
            };

            // For each command group inside command group list
            foreach (var commandGroup in PrefixCommandManager.CommandGroups)
            {
                string content = "";
                // For each command inside the current command group
                foreach (var discordCommand in commandGroup.Commands)
                    // Append the string that will represent the command and its description to the content
                    content += $"{Data.ConfigJson.Prefix}{discordCommand.Name}: ".ToDiscordBold() + $"{discordCommand.Description}{Environment.NewLine}";
                // Add a field with the command group name and the appended content
                discordEmbedBuilder.AddField(commandGroup.Name, content);
            }

            // Sending the help list into the specified channel
            await discordChannel.SendMessageAsync(discordEmbedBuilder);
        }

        /// <summary>
        /// Sends an information message about the bot, its creator, and contact details to the specified Discord channel.
        /// </summary>
        /// <param name="discordChannel">The Discord channel where the message will be sent.</param>
        /// <param name="discordClient">The Discord client instance.</param>
        internal static async Task ExecuteInfoAsync(DiscordChannel discordChannel, DiscordClient discordClient)
        {
            // Description for discord embed builder
            var description = $@"Que legal que você quer saber mais sobre mim AHAHAHAHA eu sou a KWiJisho 🌟 😎 o bot {"MAIS LEGAL DE TODOS!!!!!".ToDiscordBold()} criado " +
                "pro servidor Tramontina." +
                $"{Environment.NewLine + Environment.NewLine}Você não vai encontrar um bot tão simpático quanto eu AHAHAHHA." +
                $" Mas enfim 😎 🌟 chega de tanta legalzisse e vamos direto aos detalhes." +
                $"{Environment.NewLine + Environment.NewLine}O meu querido dono é o @monambike 💛 foi ele quem me criou e me fez ser quem eu sou hoje." +
                $"Se quiser conversar com ele aposto que ele ficará feliz em falar com você ainda mais setindo que você é uma pessoa legal. ;D" +
                $"Vou te mostrar {"algumas informações de contato".ToDiscordBold()}.";

            // Getting image name and image's full path.
            var fileName = $"500x281-talking.gif";
            var imagePath = Path.GetFullPath($"Resources/Images/KarenKujo/{fileName}");

            // Initializing discord embed builder
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Color = Data.ConfigJson.DefaultColor.DiscordColor,
                Title = "OLÁAAAAAAAA 🌟 🥳🎉",
                Description = description,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = discordClient.CurrentUser.AvatarUrl
                }
            }
            .AddField("Instagram", $"Profissional: https://www.instagram.com/monambike{Environment.NewLine}Pessoal: https://www.instagram.com/monambike_portfolio")
            .AddField("GitHub", "https://github.com/monambike")
            .AddField("Site Pessoal", "https://monambike.com")
            .WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Sending the second message with the image and button.
            await discordChannel.SendMessageAsync(new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                // The image gif of karen kujou happy talking.
                .AddFile(fileName, fileStream));
        }
    }
}
