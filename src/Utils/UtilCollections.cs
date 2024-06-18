// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System;
using System.Collections.Generic;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Provides utility methods for working with lists.
    /// </summary>
    internal static class UtilCollections
    {
        /// <summary>
        /// Returns a random value from a given list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list from which to retrieve a random value.</param>
        /// <returns>A random value from the list.</returns>
        public static T GetRandomValueFromList<T>(List<T> list) => list[new Random().Next(list.Count)];
    }
}
