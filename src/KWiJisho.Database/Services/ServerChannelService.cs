// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Config;
using KWiJisho.Database.Entities;
using SQLite;

namespace KWiJisho.Database.Services
{
    /// <summary>
    /// Service class for managing operations related to ServerChannel.
    /// </summary>
    public class ServerChannelService
    {
        /// <summary>
        /// Asynchronously retrieves a ServerChannel associated with the specified server ID.
        /// </summary>
        /// <param name="serverId">The ID of the server for which to retrieve the ServerChannel.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching ServerChannel, or null if no match is found.
        /// </returns>
        public static async Task<ServerChannelEntity?> GetServerChannelsByServerIdAsync(int serverId)
        {
            var server = new ServerChannelEntity() { ServerId = serverId };
            return await GetServerChannelsAsync(server);
        }

        public static async Task<ServerChannelEntity?> GetServerChannelsAsync(ServerChannelEntity serverChannel)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            // Query the ServerChannel table for entries matching the specified serverId,
            // and retrieve the results as a list asynchronously.
            var result = await connection.Table<ServerChannelEntity>()
                .Where(tableServerChannel => tableServerChannel == serverChannel)
                .ToListAsync();

            // Return the first matching ServerChannel or null if no results are found.
            return result.FirstOrDefault();
        }

        public static async Task<int> UpdateServerChannelByEnumAsync(ulong serverGuid, ChannelLink channel, ulong? newValue)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            // Retrieving information from the server.
            var server = await ServerService.GetServerByGuid(serverGuid);

            // Retrieving channels information from the server.
            ServerChannelEntity? serverChannel = await GetServerChannelsByServerIdAsync(server.ServerId);
            
            // Updating the link to a single provided channel.
            serverChannel = UpdateServerChannelLink(serverChannel, channel, newValue);

            try
            {

                int rowsAffected;
                if (existingServer != null)
                {
                    // Updating if record exists.
                    rowsAffected = await connection.UpdateAsync(serverChannel);
                }
                else
                {
                    // Inserting if record doesn't exist.
                    rowsAffected = await connection.InsertAsync(serverChannel);
                }

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0;
            }
        }

        public static ServerChannelEntity UpdateServerChannelLink(ServerChannelEntity serverChannel, ChannelLink serverChannelOptions, ulong? newValue)
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
    }
}
