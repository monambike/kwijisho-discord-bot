// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace KWiJisho.Events
{
    /// <summary>
    /// Provides a set of events and methods for when user is updated.
    /// </summary>
    public class DiscordUserUpdateHandler
    {
        public static async Task OnUserUpdatedAsync(DiscordClient discordClient, UserUpdateEventArgs userUpdateEventArgs)
        {
            var username = userUpdateEventArgs.UserBefore.Username;

            if (userUpdateEventArgs.UserBefore.Id == 1191998014491996181)
            {
                if (userUpdateEventArgs.UserBefore.Username != userUpdateEventArgs.UserAfter.Username)
                {
                    var description = $"O {username} mudou o nome de usuário de {userUpdateEventArgs.UserBefore.Username} para {userUpdateEventArgs.UserAfter.Username}";
                }

                if (userUpdateEventArgs.UserBefore.AvatarUrl != userUpdateEventArgs.UserAfter.AvatarUrl)
                {
                    var profilePicture = $"O {username} mudou a foto de perfil de {userUpdateEventArgs.UserBefore.AvatarUrl} para {userUpdateEventArgs.UserAfter.AvatarUrl}";
                }

                if (userUpdateEventArgs.UserBefore.BannerUrl != userUpdateEventArgs.UserAfter.BannerUrl)
                {
                    var banner = $"O {username} mudou o banner de {userUpdateEventArgs.UserBefore.BannerUrl} para {userUpdateEventArgs.UserAfter.BannerUrl}";
                }

                if (userUpdateEventArgs.UserBefore.PremiumType != userUpdateEventArgs.UserAfter.PremiumType)
                {

                    var banner = $"O {username} mudou de premium de {userUpdateEventArgs.UserBefore.PremiumType} para {userUpdateEventArgs.UserAfter.PremiumType}";
                }
            }
        }
    }
}
