using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KWiJisho.Models.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KWiJisho.Models.Commands.Prefix
{
    internal partial class PrefixCommandManager
    {
        internal class PrefixInfo : BaseCommandModule
        {
            internal const string furtherHelpDetailsMessage = @" Para receber detalhes sobre um comando digite ""help <nome do comando>"".";
            internal PrefixCommand help = new(nameof(help), $"Mostra a ajuda.{furtherHelpDetailsMessage}", Info);
            [Command(nameof(help))]
            internal async Task GetHelpAsync(CommandContext commandContext) => await Commands.Info.GetHelpAsync(commandContext.Channel, commandContext.Client);

            internal PrefixCommand info = new(nameof(info), "Mostra informações básicas sobre mim e o meu criador.", Info);
            [Command(nameof(info))]
            internal async Task GetInfoAsync(CommandContext commandContext)
            {
                var description = $@"Que legal que você quer saber mais sobre mim AHAHAHAHA eu sou a KWiJisho 🌟 😎 o bot {"MAIS LEGAL DE TODOS!!!!!".ToDiscordBold()} criado " +
                    "pro servidor Tramontina." +
                    $"{Environment.NewLine + Environment.NewLine}Você não vai encontrar um bot tão simpático quanto eu AHAHAHHA." +
                    $" Mas enfim 😎 🌟 chega de tanta legalzisse e vamos direto aos detalhes." +
                    $"{Environment.NewLine + Environment.NewLine}O meu querido dono é o @monambike 💛 foi ele quem me criou e me fez ser quem eu sou hoje." +
                    $"Se quiser conversar com ele aposto que ele ficará feliz em falar com você ainda mais setindo que você é uma pessoa legal. ;D" +
                    $"Vou te mostrar {"algumas informações de contato".ToDiscordBold()}.";


                // Getting image name and image's full path
                var fileName = $"500x281-talking.gif";
                var imagePath = Path.GetFullPath($"Resources/Images/KarenKujo/{fileName}");

                var discordEmbedBuilder = new DiscordEmbedBuilder
                {
                    Color = ConfigJson.DefaultColor.DiscordColor,
                    Title = "OLÁAAAAAAAA 🌟 🥳🎉",
                    Description = description,
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                    {
                        Url = commandContext.Client.CurrentUser.AvatarUrl
                    }
                }.AddField("Instagram", $"Profissional: https://www.instagram.com/monambike{Environment.NewLine}Pessoal: https://www.instagram.com/monambike_portfolio")
                .AddField("GitHub", "https://github.com/monambike")
                .AddField("Site Pessoal", "https://monambike.com")
                .WithImageUrl($"attachment://{imagePath}").Build();

                // Sending the second message with the image and button
                await commandContext.Channel.SendMessageAsync(new DiscordMessageBuilder()
                    .AddEmbed(discordEmbedBuilder)
                    // The image gif of karen kujou happy talking
                    .AddFile(fileName, new FileStream(imagePath, FileMode.Open)));
            }
        }
    }
}
