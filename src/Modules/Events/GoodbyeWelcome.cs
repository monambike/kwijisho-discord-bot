using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KWiJisho.Modules.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Modules.Events
{
    /// <summary>
    /// Provides a set of events and methods fired when a user enters and leaves a discord
    /// server.
    /// </summary>
    internal class GoodbyeWelcome
    {
        /// <summary>
        /// Handles the event when a new member joins the Discord server. Sends a welcome message
        /// with a image and a random string.
        /// </summary>
        /// <param name="sender">The discord client instance.</param>
        /// <param name="e">Event arguments containing information about the guild member.</param>
        /// <returns>A <see cref="Task"/> representing the assynchronous operation.</returns>
        internal static async Task OnGuildMemberAddedAsync(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);
            // Getting welcome image info.
            var fileName = $"500x500-welcome.gif";
            var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");

            // Making the Discord Embed Builder with the message body.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = $"BEM-VINDO",
                Description = GetRandomWelcomeMessage(e.Member.Mention),
                Color = ConfigJson.DefaultColor.DiscordColor
            }.WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Creating message builder and attaching the message embed builder and image file.
            var discordMessageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder).AddFile(fileName, fileStream);

            // Sending the message on welcome channel.
            await e.Guild.GetChannel(ServerTramontina.WelcomeChannelId).SendMessageAsync(discordMessageBuilder);
        }

        /// <summary>
        /// Handles the event when a member leaves the Discord server. Sends a goodbye message
        /// with a image and a random string.
        /// </summary>
        /// <param name="sender">The discord client instance.</param>
        /// <param name="e">Events arguments containing information about the guild member.</param>
        /// <returns>A <see cref="Task"/> representing the assynchronous operation.</returns>
        internal static async Task OnGuildMemberRemovedAsync(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            // If senders is null throws an exception.
            ArgumentNullException.ThrowIfNull(sender);

            // Getting welcome image info.
            var fileName = $"1173x315-goodbye.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");

            // Making the discord embed builder with the message body content and image file.
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = @$"""ACHO QUE ISSO É UM ADEUS""",
                Description = GetRandomGoodbyeMessage(e.Member.Mention),
                Color = ConfigJson.DefaultColor.DiscordColor
            }.WithImageUrl($"attachment://{imagePath}").Build();

            using var fileStream = new FileStream(imagePath, FileMode.Open);
            // Creating message builder and attaching the message embed builder and image file.
            var discordMessageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder).AddFile(fileName, fileStream);

            // Sending the message on welcome channel
            await e.Guild.GetChannel(ServerTramontina.WelcomeChannelId).SendMessageAsync(discordMessageBuilder);
        }

        /// <summary>
        /// Gets a random string welcome message.
        /// </summary>
        /// <param name="user">The user that will receive the welcome message.</param>
        /// <returns>The string containing the welcome message .</returns>
        internal static string GetRandomWelcomeMessage(string user) => UtilList.GetRandomValueFromList([
            $"EAEEEEEE, Bem-vindo ao servidor {user} meu consagrado! ;D",
            $"SEJA BEM V-V-V-VIIIIIIIIIIIINDO AO TRA-MON-TINAAAA 🎉 {user}",
            $"Como vai {user} meu parceiro? 😎 Aproveite a sua estadia por aqui e se precisar de alguma titia KAWAI JISHO TÁ NA ÁAAAREA",
            $"Bem-vindo {user} ao servidor MAIS LEGAL DE TODOS, com o bot mais legal da face da terra hehehe 😎 (vulgo euzinha)"
        ]);

        /// <summary>
        /// Gets a random string goodbye message.
        /// </summary>
        /// <param name="user">The user that will receive the welcome message.</param>
        /// <returns>The string containing the goodbye message .</returns>
        internal static string GetRandomGoodbyeMessage(string user) => UtilList.GetRandomValueFromList([
            $"Até logo amigo.. Foi bom te conhecer {user} :(",
            $"Já vai tarde.. BRINCADEIRINHA HAHAHA... Ai mas não.. falando sério, vai fazer falta 🙁 {user}",
            $"NÃAAO, partiu ainda tão tão joveeeeeeeeem 😭😭😭😭 Sentiremos sua falta {user}..",
            $"{user}... Pera... Ele fazia parte desse servidor? 🤔 podia jurar que vi num servidor furry.. Q-Quer dizer.. 😦😶 Não que eu também esteja lá, me adicionaram contra minha vontade!"
        ]);
    }
}
