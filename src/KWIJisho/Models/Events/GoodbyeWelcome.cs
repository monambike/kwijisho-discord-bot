using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KWIJisho.Models.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KWIJisho.Models.Events
{
    internal class GoodbyeWelcome
    {
        internal static async Task OnGuildMemberAddedAsync(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);
            // Getting welcome image info
            var fileName = $"500x500-welcome.gif";
            var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");

            // Message body
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = $"BEM-VINDO",
                Description = GetRandomWelcomeMessage(e.Member.Mention),
                Color = ConfigJson.DefaultColor.DiscordColor
            }.WithImageUrl($"attachment://{imagePath}").Build();

            // Creating message builder with message body and image file
            var discordMessageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                .AddFile(fileName, new FileStream(imagePath, FileMode.Open));

            // Sending the message on welcome channel
            await e.Guild.GetChannel(842222447410544650).SendMessageAsync(discordMessageBuilder);
        }

        internal static async Task OnGuildMemberRemovedAsync(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);
            var message = $"{e.Member.Username} Change Da World My Final Message...GoodBye";


            // Getting welcome image info
            var fileName = $"1173x315-goodbye.png";
            var imagePath = Path.GetFullPath($"Resources/Images/Tramontina/{fileName}");

            // Message body
            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = @$"""ACHO QUE ISSO É UM ADEUS""",
                Description = GetRandomGoodbyeMessage(e.Member.Mention),
                Color = ConfigJson.DefaultColor.DiscordColor
            }.WithImageUrl($"attachment://{imagePath}").Build();

            // Creating message builder with message body and image file
            var discordMessageBuilder = new DiscordMessageBuilder()
                .AddEmbed(discordEmbedBuilder)
                .AddFile(fileName, new FileStream(imagePath, FileMode.Open));

            // Sending the message on welcome channel
            await e.Guild.GetChannel(842222447410544650).SendMessageAsync(discordMessageBuilder);

            // Welcome channel
            var welcomeChannel = e.Guild.GetChannel(842222447410544650);
            await welcomeChannel.SendMessageAsync(message);

            // General chat
            var generalChannel = e.Guild.GetChannel(692588978959941656);
            await generalChannel.SendMessageAsync(message);
        }

        /// <summary>
        /// Gets a random welcome message.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static string GetRandomWelcomeMessage(string user) => List.GetRandomValueFromList([
            $"EAEEEEEE, Bem-vindo ao servidor {user} meu consagrado! ;D",
            $"SEJA BEM V-V-V-VIIIIIIIIIIIINDO AO TRA-MON-TINAAAA 🎉 {user}",
            $"Como vai parceiro? 😎 Aproveite a sua estadia por aqui e se precisar de alguma titia KAWAI JISHO TÁ NA ÁAAAREA",
            $"Bem-vindo ao servidor MAIS LEGAL DE TODOS, com o bot mais legal da face da terra hehehe 😎 (vulgo euzinha)"
        ]);

        /// <summary>
        /// Gets a random goodbye message.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static string GetRandomGoodbyeMessage(string user) => List.GetRandomValueFromList([
            $"Até logo amigo.. Foi bom te conhecer {user} :(",
            $"Já vai tarde.. BRINCADEIRINHA HAHAHA... Ai mas não.. falando sério, vai fazer falta 🙁 {user}",
            $"NÃAAO, partiu ainda tão tão joveeeeeeeeem 😭😭😭😭 Sentiremos sua falta {user}..",
            $"Pera.. Essa pessoa.. Fazia parte desse servidor? 🤔 podia jurar que vi num servidor furry.. Q-Quer dizer.. 😦😶 Não que eu também esteja lá, me adicionaram contra minha vontade!"
        ]);
    }
}
