// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Database.Models
{
    public class User
    {
        public int UserId { get; set; }

        public ulong UserGuid { get; set; }

        public string Username { get; set; }

        public DateTime Birthday { get; set; }
    }
}
