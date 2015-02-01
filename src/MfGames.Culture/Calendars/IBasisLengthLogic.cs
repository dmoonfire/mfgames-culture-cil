// <copyright file="IBasisLengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System.Collections.Generic;

    public interface IBasisLengthLogic
    {
        /// <summary>
        /// Retrieve the number of reference elements in the basis.
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="values">The values collection that contains the current indexes.</param>
        /// <param name="count">The resulting number of referenced cycles.</param>
        /// <returns>True if this is a valid calculation.</returns>
        bool GetCount(Dictionary<string, object> variables, CalendarElementValueDictionary values, out int count);
    }
}
