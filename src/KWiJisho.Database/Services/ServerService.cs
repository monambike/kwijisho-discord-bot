// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Entities;
using KWiJisho.Database.Repositories;

namespace KWiJisho.Database.Services
{
    public static class ServerService
    {
        public static ServerRepository ServerRepository => new();

        public static ServerChannelRepository ServerChannelRepository => new();

        public static async Task<int> UpdateServerChannelByEnumAsync(ulong serverGuid, ChannelLink channel, ulong? newValue)
        {
            // Retrieving informations from the server.
            var server = await ServerRepository.GetServerByGuid(serverGuid);

            if (server == null)
            {
                server = new ServerEntity() { ServerGuid = serverGuid };
                await ServerRepository.CreateServerAsync(server);
                server = await ServerRepository.GetServerByGuid(serverGuid);
            }

            // Retrieving channel informations from the server.
            ServerChannelEntity? serverChannel = await ServerChannelRepository.GetServerChannelByServerIdAsync(server.ServerId);
            if (serverChannel == null)
            {
                await ServerChannelRepository.GetServerChannelByServerIdAsync(server.ServerId);
                serverChannel = await ServerChannelRepository.GetServerChannelByServerIdAsync(server.ServerId);
            }

            try
            {
                int rowsAffected = 0;

                var newServerChannel = ChangeServerChannelPropertyByEnum(serverChannel, channel, newValue);

                // Updating server channel record.
                rowsAffected += await ServerChannelRepository.UpdateServerChannelAsync(newServerChannel);

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to update server channel data.");
                return 0;
            }
        }

        public static ServerChannelEntity ChangeServerChannelPropertyByEnum(ServerChannelEntity serverChannel, ChannelLink serverChannelOptions, ulong? newValue)
        {
            switch (serverChannelOptions)
            {
                case ChannelLink.WelcomeChannel: serverChannel.WelcomeChannelGuid = newValue; break;
                case ChannelLink.GoodbyeChannel: serverChannel.GoodbyeChannelGuid = newValue; break;
                case ChannelLink.NewsChannel: serverChannel.NewsChannelGuid = newValue; break;
                case ChannelLink.LogChannel: serverChannel.LogChannelGuid = newValue; break;
                default: throw new NotImplementedException();
            };

            return serverChannel;
        }

        /// <summary>
        /// Enum that represents channel links.
        /// </summary>
        public enum ChannelLink
        {
            /// <summary>
            /// The link for the welcome channel.
            /// </summary>
            WelcomeChannel,

            /// <summary>
            /// The link for the goodbye channel.
            /// </summary>
            GoodbyeChannel,

            /// <summary>
            /// The link for the news channel.
            /// </summary>
            NewsChannel,

            /// <summary>
            /// The link for the log channel.
            /// </summary>
            LogChannel
        }

        public enum GuildAction
        {
            Unlink = 0,

            Link = 1
        }
    }
}
