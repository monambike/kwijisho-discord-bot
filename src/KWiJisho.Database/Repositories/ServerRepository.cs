// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Entities;
using KWiJisho.Database.Helpers;
using SQLite;

namespace KWiJisho.Database.Repositories
{
    /// <summary>
    /// Repository class for managing operations related to Server.
    /// </summary>
    public class ServerRepository()
    {
        private static readonly Lazy<ServerRepository> _instance = new(() => new ServerRepository());

        public static ServerRepository Instance => _instance.Value;

        private readonly SQLiteAsyncConnection _connection = DatabaseService.Connection;

        public async Task<List<ServerEntity>> GetAllServersAsync()
        {
            try
            {
                var result = await _connection.Table<ServerEntity>()
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to get all servers data.");
                throw;
            }
        }

        public async Task<ServerEntity?> GetServerById(int serverId)
        {
            try
            {
                var result = await _connection.Table<ServerEntity>()
                    .Where(tableServer => tableServer.ServerId == serverId)
                    .ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to get server data.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets a Server information associated with the specified server GUID.
        /// </summary>
        /// <param name="serverGuid">The GUID of the server for which to get the Server information.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The task result contains 
        /// the first matching Server, or null if no match is found.
        /// </returns>
        public async Task<ServerEntity?> GetServerByGuid(ulong serverGuid)
        {
            try
            {
                var result = await _connection.Table<ServerEntity>()
                    .Where(tableServer => tableServer.ServerGuid == serverGuid)
                    .ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to get server data.");
                throw;
            }
        }

        public async Task<int> CreateServerAsync(ServerEntity server)
        {
            try
            {
                var rowsAffected = await _connection.InsertAsync(server);

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to create server data.");
                throw;
            }
        }

        public async Task<int> UpdateServerAsync(ServerEntity server)
        {
            try
            {
                var rowsAffected = await _connection.UpdateAsync(server);

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message} while trying to update server data.");
                throw;
            }
        }
    }
}
