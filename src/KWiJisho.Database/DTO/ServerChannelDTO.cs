﻿// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

namespace KWiJisho.Database.DTO
{
    public class ServerChannelDTO
    {
        public int ServerChannelId { get; set; }

        public ulong WelcomeChannelGuid { get; set; }

        public ulong GoodbyeChannelGuid { get; set; }

        public ulong NewsChannelGuid { get; set; }

        public ulong LogChannelGuid { get; set; }

        public ServerDTO Server { get; set; }
    }
}
