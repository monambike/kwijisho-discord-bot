using DSharpPlus.CommandsNext;

namespace KWiJisho.Modules.Utils
{
    /// <summary>
    /// The class that represents a new instance of a Discord channel with a
    /// unique identifier and a default name.
    /// <param name="id">The Discord channel unique identifier.</param>
    /// <param name="defaultName">The Discord channel default name.</param>
    /// </summary>
    internal class Channel(ulong id, string defaultName)
    {
        /// <summary>
        /// The Discord channel unique identifier.
        /// </summary>
        internal ulong Id = id;

        /// <summary>
        /// The Discord channel default name.
        /// </summary>
        internal string DefaultName = defaultName;

        /// <summary>
        /// Updates the name of the Discord channel asynchronously.
        /// </summary>
        /// <param name="commandContext">The command context containing information about the command.</param>
        /// <param name="newName">The new name for the Discord channel.</param>
        internal async void UpdateChannelNameAsync(CommandContext commandContext, string newName)
        {
            var channel = commandContext.Client.GetChannelAsync(Id).Result;
            // Rename the channel
            await channel.ModifyAsync(editChannel => editChannel.Name = $"{newName}");
        }
    }
}
