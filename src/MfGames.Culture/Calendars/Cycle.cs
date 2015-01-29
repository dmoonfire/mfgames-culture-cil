// <copyright file="Cycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    /// <summary>
    /// Defines an element within a calendar that is individual identified, such as a day or month.
    /// It can be an open-ended without an end point, such as a year, or it can repeatedly loop
    /// within the context of a larger cycle, such as the day of month or day of year.
    /// </summary>
    public abstract class Cycle : CalendarElement
    {
        protected Cycle(string id)
            : base(id)
        {
        }

        public Basis Basis { get; set; }
    }
}
