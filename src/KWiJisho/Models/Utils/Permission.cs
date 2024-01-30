using DSharpPlus;
using DSharpPlus.Entities;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace KWiJisho.Models.Utils
{
    internal static class Permission
    {
        public static async Task<bool> RequireAdministratorAsync(DiscordChannel discordChannel, DiscordMember discordMember) =>
            // If user has permission, return true
            (discordMember?.Permissions.HasPermission(Permissions.Administrator) ?? false)
            // Otherwise, if user doesn't have or user is null, return "command can't be run" message
            || await discordChannel.SendMessageAsync("Esse comando é só para administradores.. (ᴗ_ ᴗ。) Eu sinto muito ") is not null && false;
    }
}
