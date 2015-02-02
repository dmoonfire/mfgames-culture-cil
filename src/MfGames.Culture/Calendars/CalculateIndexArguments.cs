// <copyright file="CalculateIndexArguments.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    public class CalculateIndexArguments
    {
        public CalculateIndexArguments(
            CalendarElementValueDictionary elements,
            decimal julianDate)
        {
            Elements = elements;
            JulianDate = julianDate;
        }

        public CalendarElementValueDictionary Elements { get; set; }
        public decimal JulianDate { get; set; }
        public bool ExceededCycle { get; set; }
    }
}
