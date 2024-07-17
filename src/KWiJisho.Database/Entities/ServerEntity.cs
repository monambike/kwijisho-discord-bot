// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using SQLite;

namespace KWiJisho.Database.Entities
{
    [Table("Server")]
    public class ServerEntity
    {
        public int ServerId { get; set; }

        public ulong ServerGuid { get; set; }

        public string Name { get; set; }
    }
}
