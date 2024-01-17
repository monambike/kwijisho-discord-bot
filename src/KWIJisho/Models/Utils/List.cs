using System;
using System.Collections.Generic;

namespace KWIJisho.Models.Utils
{
    internal static class List
    {
        /// <summary>
        /// Returns a random value from a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetRandomValueFromList<T>(List<T> list) => list[new Random().Next(list.Count)];
    }
}
