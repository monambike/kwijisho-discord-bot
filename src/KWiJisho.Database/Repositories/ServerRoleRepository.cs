// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Entities;
using KWiJisho.Database.Helpers;
using SQLite;

namespace KWiJisho.Database.Repositories
{
    /// <summary>
    /// Repository class for managing operations related to ServerRole.
    /// </summary>
    public class ServerRoleRepository
    {
        private readonly SQLiteAsyncConnection _connection = DatabaseService.Connection;

        /// <summary>
        /// Asynchronously retrieves a ServerRole associated with the specified server ID.
        /// </summary>
        /// <param name="serverId">The ID of the server for which to retrieve the ServerRole.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching ServerRole, or null if no match is found.
        /// </returns>
        public async Task<ServerRoleEntity?> GetServerRolesByServerId(int serverId)
        {
            try
            {
                var result = await _connection.Table<ServerRoleEntity>()
                    .Where(serverRole => serverRole.ServerId == serverId)
                    .ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to get server role data.");
                throw;
            }
        }
    }
}
