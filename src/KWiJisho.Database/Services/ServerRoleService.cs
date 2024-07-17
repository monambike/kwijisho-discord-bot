// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Config;
using KWiJisho.Database.Entities;
using SQLite;

namespace KWiJisho.Database.Services
{
    /// <summary>
    /// Service class for managing operations related to ServerRole.
    /// </summary>
    public class ServerRoleService
    {
        /// <summary>
        /// Asynchronously retrieves a ServerRole associated with the specified server ID.
        /// </summary>
        /// <param name="serverId">The ID of the server for which to retrieve the ServerRole.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching ServerRole, or null if no match is found.
        /// </returns>
        public static async Task<ServerRoleEntity?> GetServerRolesByServerId(int serverId)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            // Query the ServerRole table for entries matching the specified serverId,
            // and retrieve the results as a list asynchronously.
            var result = await connection.Table<ServerRoleEntity>()
                .Where(serverRole => serverRole.ServerId == serverId)
                .ToListAsync();

            // Return the first matching ServerRole or null if no results are found.
            return result.FirstOrDefault();
        }
    }
}
