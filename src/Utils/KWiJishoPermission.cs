using DSharpPlus;
using System;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Provides utility methods for handling permissions in the KWiJisho application.
    /// </summary>
    internal static class KWiJishoPermission
    {
        /// <summary>
        /// Generates a custom error message for a permission check failure.
        /// </summary>
        /// <param name="permission">The required permission.</param>
        /// <returns>A formatted error message indicating the required permission.</returns>
        internal static string PermissionCustomErrorMessage(Permissions permission)
            => $@"Esse comando é só para pessoas que possuem a permissão ""{GetPermissionNameInPortuguese(permission)}"".. (ᴗ_ ᴗ。) Eu sinto muito..";

        /// <summary>
        /// Gets the Portuguese name of a Discord permission.
        /// </summary>
        /// <param name="permission">The Discord permission.</param>
        /// <returns>The Portuguese name of the permission.</returns>
        internal static string GetPermissionNameInPortuguese(Permissions permission)
        {
            return permission switch
            {
                Permissions.None => throw new NotImplementedException(),
                Permissions.All => throw new NotImplementedException(),
                Permissions.CreateInstantInvite => throw new NotImplementedException(),
                Permissions.KickMembers => throw new NotImplementedException(),
                Permissions.BanMembers => throw new NotImplementedException(),
                Permissions.Administrator => "administrador",
                Permissions.ManageChannels => throw new NotImplementedException(),
                Permissions.ManageGuild => throw new NotImplementedException(),
                Permissions.AddReactions => throw new NotImplementedException(),
                Permissions.ViewAuditLog => throw new NotImplementedException(),
                Permissions.PrioritySpeaker => throw new NotImplementedException(),
                Permissions.AccessChannels => throw new NotImplementedException(),
                Permissions.SendMessages => throw new NotImplementedException(),
                Permissions.SendTtsMessages => throw new NotImplementedException(),
                Permissions.ManageMessages => throw new NotImplementedException(),
                Permissions.EmbedLinks => throw new NotImplementedException(),
                Permissions.AttachFiles => throw new NotImplementedException(),
                Permissions.ReadMessageHistory => throw new NotImplementedException(),
                Permissions.MentionEveryone => throw new NotImplementedException(),
                Permissions.UseExternalEmojis => throw new NotImplementedException(),
                Permissions.UseVoice => throw new NotImplementedException(),
                Permissions.Speak => throw new NotImplementedException(),
                Permissions.MuteMembers => throw new NotImplementedException(),
                Permissions.DeafenMembers => throw new NotImplementedException(),
                Permissions.MoveMembers => throw new NotImplementedException(),
                Permissions.UseVoiceDetection => throw new NotImplementedException(),
                Permissions.ChangeNickname => throw new NotImplementedException(),
                Permissions.ManageNicknames => throw new NotImplementedException(),
                Permissions.ManageRoles => throw new NotImplementedException(),
                Permissions.ManageWebhooks => throw new NotImplementedException(),
                Permissions.ManageEmojis => throw new NotImplementedException(),
                Permissions.Stream => throw new NotImplementedException(),
                Permissions.RequestToSpeak => throw new NotImplementedException(),
                Permissions.ManageEvents => throw new NotImplementedException(),
                Permissions.ManageThreads => throw new NotImplementedException(),
                Permissions.UseExternalStickers => throw new NotImplementedException(),
                Permissions.SendMessagesInThreads => throw new NotImplementedException(),
                Permissions.StartEmbeddedActivities => throw new NotImplementedException(),
                Permissions.ModerateMembers => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
