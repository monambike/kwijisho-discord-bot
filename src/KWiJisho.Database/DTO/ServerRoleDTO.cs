// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Database.DTO
{
    public class ServerRoleDTO
    {
        public int ServerRoleId { get; set; }

        public ulong BirthdayRoleGuid { get; set; }

        public ServerDTO ServerId { get; set; }
    }
}
