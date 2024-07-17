// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Database.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public ulong UserGuid { get; set; }

        public string Username { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
