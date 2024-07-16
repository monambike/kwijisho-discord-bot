// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Data;

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
        public static async Task<ServerChannel?> GetServerChannelsByServerId(int serverId)
        {
            // Establish a connection to the SQLite database asynchronously.
            var db = new Connection().SQLiteAsyncConnection;

            // Query the ServerChannel table for entries matching the specified serverId,
            // and retrieve the results as a list asynchronously.
            var result = await db.Table<ServerChannel>()
                .Where(serverChannel => serverChannel.ServerId == serverId)
                .ToListAsync();

            // Return the first matching ServerChannel or null if no results are found.
            return result.FirstOrDefault();
        }
    }
}
