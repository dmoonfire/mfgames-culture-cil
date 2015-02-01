// <copyright file="CalendarPoint.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;
    using System.Dynamic;
    using System.Linq;

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
            Calendar = calendar;
            JulianDayNumber = julianDayNumber;
            Elements = new CalendarElementValueDictionary();

            // Loops through all the open cycles and calculate each one.
            foreach (OpenCycle openCycle in calendar.OpenCycles)
            {
                openCycle.CalculateIndex(Elements, julianDayNumber);
            }

            // Remove the element values for the closed cycles.
            foreach (var closedCycle in Calendar.Elements.OfType<ClosedCycle>())
            {
                Elements.Remove(closedCycle.Id);
            }
        }

        private CalendarElementValueDictionary Elements { get; set; }
        public CalendarSystem Calendar { get; private set; }
        public decimal JulianDayNumber { get; private set; }

        /// <summary>
        /// Retrieves the zero-based index for an element (basis or cycle) represented
        /// by the current point.
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public int Get(string elementId)
        {
            return Elements[elementId];
        }
    }
}
