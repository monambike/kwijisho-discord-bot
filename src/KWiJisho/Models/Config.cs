// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using KWiJisho.Database.Entities;
using KWiJisho.Database.Services;
using System.Threading.Tasks;

namespace KWiJisho.Models
{
    internal class Config
    {

        public static async Task<int> LinkChannel(ServerChannelEntity serverChannel)
        {
            // Creates a row for the current server and for subtables if there's not yet.
            await ServerService.CreateServerIfNotExistsAsync(serverChannel);

            // Updating server channel link.
            await ServerChannelService.UpdateServerChannelLink(serverChannel);
        }
    }
}
