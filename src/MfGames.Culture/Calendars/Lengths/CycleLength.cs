// <copyright file="CycleLength.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Lengths
{
    public abstract class CycleLength
    {
        #region Public Methods and Operators

        public abstract decimal CalculateIndex(
            string id,
            CalendarElementValueCollection values,
            decimal relativeJulianDay);

        #endregion
    }
}
