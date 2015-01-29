// <copyright file="CalendarPoint.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;
    using System.Dynamic;

    /// <summary>
    /// Represents a single temporal point within a specific calendar system.
    /// </summary>
    public class CalendarPoint : DynamicObject
    {
        public CalendarPoint(CalendarSystem calendar, decimal julianDayNumber)
        {
            // Establish the contracts.
            if (calendar == null)
            {
                throw new ArgumentNullException("calendar");
            }

            if (julianDayNumber < 0)
            {
                throw new ArgumentException(
                    "Cannot use a negative Julian Day Number.",
                    "julianDayNumber");
            }

            // Save the member variables.
            this.Calendar = calendar;
            this.JulianDayNumber = julianDayNumber;
        }

        public CalendarSystem Calendar { get; private set; }
        public decimal JulianDayNumber { get; private set; }
    }
}
