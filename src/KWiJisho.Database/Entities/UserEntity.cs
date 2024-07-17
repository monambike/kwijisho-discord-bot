// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using SQLite;

namespace KWiJisho.Database.Entities
{
    [Table("User")]
    public class UserEntity
    {
        public int UserId { get; set; }

        public ulong UserGuid { get; set; }

        public string Username { get; set; }

        public string? Birthday { get; set; }
    }
}
