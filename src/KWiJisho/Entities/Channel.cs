// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;

namespace KWiJisho.Entities
{
    /// <summary>
    /// The class that represents a new instance of a Discord channel with a
    /// unique identifier and a default name.
    /// <param name="id">The Discord channel unique identifier.</param>
    /// <param name="defaultName">The Discord channel default name.</param>
    /// </summary>
    public class Channel(ulong id, string defaultName)
    {
        /// <summary>
        /// The Discord channel unique identifier.
        /// </summary>
        public ulong Id = id;

        /// <summary>
        /// The Discord channel default name.
        /// </summary>
        public string DefaultName = defaultName;

        /// <summary>
        /// Updates asynchronously the name of the Discord channel.
        /// </summary>
        /// <param name="discordClient">The Discord client instance.</param>
        /// <param name="newName">The new name for the Discord channel.</param>
        public async void UpdateChannelNameAsync(DiscordClient discordClient, string newName)
        {
            // Get the channel on current Discord server based their Id.
            var channel = discordClient.GetChannelAsync(Id).Result;

            // Rename the channel with the new name.
            await channel.ModifyAsync(editChannel => editChannel.Name = $"{newName}");
        }
    }
}
