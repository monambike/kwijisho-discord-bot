// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using SQLite;

namespace KWiJisho.Database.Data
{
    [Table("ServerRole")]
    public class ServerRole
    {
        public int ServerRoleId { get; set; }

        public ulong BirthdayRoleGuid { get; set; }

        public int ServerId { get; set; }
    }
}
