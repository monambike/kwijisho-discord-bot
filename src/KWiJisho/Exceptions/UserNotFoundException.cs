// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System;

namespace KWiJisho.Exceptions
{
    internal class UserNotFoundException : Exception
    {
        public int NotFoundUserId;
        public UserNotFoundException(string userId)
            : base($"User with ID {userId} was not found.")
        {

        }
    }
}
