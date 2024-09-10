// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.


using KWiJisho.Database.Entities;
using KWiJisho.Database.Repositories;

namespace KWiJisho.Database.Services
{
    public class ServerChannelService
    {
        public static ServerRepository ServerRepository => new();

        public static ServerChannelRepository ServerChannelRepository => new();

        public static async Task<ServerChannelEntity?> GetServerChannelByServerGuidAsync(ulong serverGuid)
        {
            try
            {
                // Retrieve the server by GUID
                var server = await ServerRepository.GetServerByGuid(serverGuid);
                if (server is null)
                {
                    Console.WriteLine($"Server with GUID {serverGuid} not found.");
                    return null;
                }

                // Retrieve the server channel by ServerId
                var serverChannel = await ServerChannelRepository.GetServerChannelByServerIdAsync(server.ServerId);
                if (serverChannel is null)
                {
                    Console.WriteLine($"Server channel for ServerId {server.ServerId} not found.");
                    return null;
                }

                return serverChannel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred while retrieving server channel data: {ex.Message}");
                throw;
            }
        }
    }
}
