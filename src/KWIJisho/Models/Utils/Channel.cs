using DSharpPlus.CommandsNext;

namespace KWIJisho.Models.Utils
{
    /// <summary>
    /// Class that represents a Discord Channel.
    /// </summary>
    internal class Channel
    {
        internal ulong Id { get; private set; }

        internal string DefaultName { get; set; }

        internal Channel(ulong id, string defaultName)
        {
            Id = id;
            DefaultName = defaultName;
        }

        internal async void UpdateChannelNameAsync(CommandContext commandContext, string newName)
        {
            var channel = commandContext.Client.GetChannelAsync(Id).Result;
            // Rename the channel
            await channel.ModifyAsync(editChannel => editChannel.Name = $"{newName}");
        }
    }
}
