// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Entities;
using KWiJisho.Database.Helpers;
using SQLite;

namespace KWiJisho.Database.Repositories
{
    /// <summary>
    /// Repository class for managing operations related to ServerChannel.
    /// </summary>
    public class ServerChannelRepository
    {
        private static readonly Lazy<ServerRepository> _instance = new(() => new ServerRepository());

        public static ServerRepository Instance => _instance.Value;

        private readonly SQLiteAsyncConnection _connection = DatabaseService.Connection;

        /// <summary>
        /// Asynchronously retrieves a ServerChannel associated with the specified server ID.
        /// </summary>
        /// <param name="serverId">The ID of the server for which to retrieve the ServerChannel.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching ServerChannel, or null if no match is found.
        /// </returns>
        public async Task<ServerChannelEntity?> GetServerChannelByServerIdAsync(int serverId)
        {
            try
            {
                var result = await _connection.Table<ServerChannelEntity>()
                    .Where(tableServerChannel => tableServerChannel.ServerId == serverId)
                    .ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to get server channel data.");
                throw;
            }
        }

        public async Task<int> UpdateServerChannelAsync(ServerChannelEntity serverChannel)
        {
            try
            {
                var rowsAffected = await _connection.UpdateAsync(serverChannel);

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to update server channel data.");
                throw;
            }
        }
    }
}
