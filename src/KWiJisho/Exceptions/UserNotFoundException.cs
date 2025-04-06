// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System;

namespace KWiJisho.Exceptions
{
    /// <summary>
    /// Exception thrown when a user with a specified ID cannot be found.
    /// </summary>
    /// <remarks>
    /// This exception should be used when a user lookup fails, typically when accessing user related data by ID.
    /// </remarks>
    internal class UserNotFoundException : Exception
    {
        /// <summary>
        /// The ID of the user that could not be found.
        /// </summary>
        public int NotFoundUserId;

        /// <summary>
        /// Exception thrown when a user with a specified ID cannot be found.
        /// </summary>
        /// <remarks>
        /// This exception should be used when a user lookup fails, typically when accessing user related data by ID.
        /// </remarks>
        /// <param name="userId">The ID of the user that was not found.</param>
        public UserNotFoundException(string userId)
            : base($"User with ID {userId} was not found.")
        {

        }
    }
}
