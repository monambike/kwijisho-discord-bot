// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Data;

namespace KWiJisho.Database.Services
{
    /// <summary>
    /// Service class for managing operations related to Server.
    /// </summary>
    public class ServerService
    {
        /// <summary>
        /// Asynchronously retrieves a Server information associated with the specified server GUID.
        /// </summary>
        /// <param name="serverGuid">The GUID of the server for which to retrieve the Server information.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching Server, or null if no match is found.
        /// </returns>
        public static async Task<Server?> GetServerByServerGuid(int serverGuid)
        {
            // Establish a connection to the SQLite database asynchronously.
            var db = new Connection().SQLiteAsyncConnection;

            // Query the Server table for entries matching the specified serverGuid,
            // and retrieve the results as a list asynchronously.
            var result = await db.Table<Server>()
                .Where(server => server.ServerId == serverGuid)
                .ToListAsync();

            // Return the first matching Server or null if no results are found.
            return result.FirstOrDefault();
        }
    }
}
