// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using System;
using System.Globalization;

namespace KWiJisho.Utils
{
    /// <summary>
    /// Provides utility methods for working with dates.
    /// </summary>
    internal class UtilDate
    {
        /// <summary>
        /// Returns the date in ISO 8601 date format.
        /// </summary>
        /// <param name="dateTime">The DateTime to be converted.</param>
        /// <returns>A string containing a date in the ISO 8601 format.</returns>
        public static string ConvertDateTimeToISO8601AsString(DateTime dateTime)
            => dateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns the date in UNIX date format.
        /// </summary>
        /// <param name="dateTime">The DateTime to be converted.</param>
        /// <returns>A long containing a date in the UNIX format.</returns>
        public static long ConvertDateTimeToUnixTimestamp(DateTime dateTime)
        {
            // Ensuring that the DateTime is in UTC.
            var utcDateTime = dateTime.ToUniversalTime();

            // Calculating the Unix timestamp.
            long unixTimestamp = ((DateTimeOffset)utcDateTime).ToUnixTimeSeconds();

            return unixTimestamp;
        }
    }
}
