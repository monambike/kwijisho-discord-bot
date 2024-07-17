// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Config;
using KWiJisho.Database.Entities;
using SQLite;

namespace KWiJisho.Database.Services
{
    /// <summary>
    /// Service class for managing operations related to Server.
    /// </summary>
    public class ServerService
    {
        public static async Task<ServerEntity?> GetServerById(int serverId)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            // Query the Server table for entries matching the specified serverGuid,
            // and retrieve the results as a list asynchronously.
            var result = await connection.Table<ServerEntity>()
                .Where(tableServer => tableServer.ServerId == serverId)
                .ToListAsync();

            // Return the first matching Server or null if no results are found.
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously retrieves a Server information associated with the specified server GUID.
        /// </summary>
        /// <param name="serverGuid">The GUID of the server for which to retrieve the Server information.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching Server, or null if no match is found.
        /// </returns>
        public static async Task<ServerEntity?> GetServerByGuid(ulong serverGuid)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            // Query the Server table for entries matching the specified serverGuid,
            // and retrieve the results as a list asynchronously.
            var result = await connection.Table<ServerEntity>()
                .Where(tableServer => tableServer.ServerGuid == serverGuid)
                .ToListAsync();

            // Return the first matching Server or null if no results are found.
            return result.FirstOrDefault();
        }

        public static async Task<int> CreateServerAsync(ServerEntity server)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            var rowsAffected = await UpdateServerAsync(server);
            // Check if there's no rows affected by the update.
            if (rowsAffected == 0)
                // Inserting into the database.
                rowsAffected = await connection.InsertAsync(server);

            return rowsAffected;
        }

        public static async Task CreateServerIfNotExistsAsync(ServerEntity server)
        {
            var existingServer = GetServerByGuid(server.ServerGuid);
            if (existingServer == null) await CreateServerAsync(server);
        }

        public static async Task<int> UpdateServerAsync(ServerEntity server)
        {
            // Establish a connection to the SQLite database asynchronously.
            var connection = new SQLiteAsyncConnection(ConnectionConfig.DatabasePath);

            var rowsAffected = await connection.UpdateAsync(server);

            return rowsAffected;
        }
    }
}
