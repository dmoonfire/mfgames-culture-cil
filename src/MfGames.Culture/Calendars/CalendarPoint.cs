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
            Calendar = calendar;
            JulianDayNumber = julianDayNumber;
            ElementValues = new CalendarElementValueDictionary();

            // Loops through all the open cycles and calculate each one.
            foreach (OpenCycle openCycle in calendar.OpenCycles)
            {
                CalculateCycle(JulianDayNumber, openCycle);
            }
        }

        private CalendarElementValueDictionary ElementValues { get; set; }
        public CalendarSystem Calendar { get; private set; }
        public decimal JulianDayNumber { get; private set; }

        /// <summary>
        /// Calculates the given cycle and stores the value.
        /// </summary>
        /// <param name="julianDayNumber"></param>
        /// <param name="cycle"></param>
        private void CalculateCycle(decimal julianDayNumber, OpenCycle cycle)
        {
            // Reset the elements to zero.
            foreach (CalendarElement element in Calendar.ValueElements)
            {
                ElementValues[element.Id] = 0;
            }

            // Iterate through various permutations of the given open cycle until
            // we find the cycle that the JDN is contained within. This is done
            // by starting with the first one and then calculating the length.
            while (julianDayNumber > 0)
            {
                // We need to get the length of the next element.
                decimal length = cycle.GetLength(ElementValues);

                // Check to see if the length is greater than the JDN. If it is, then
                // previous index is the correct one. Otherwise, we increment.
                if (julianDayNumber < length)
                {
                    break;
                }

                // We have to advance it, so reduce the JDN by our length and repeat.
                julianDayNumber -= length;
                ElementValues[cycle.Id]++;
            }
        }

        /// <summary>
        /// Retrieves the zero-based index for an element (basis or cycle) represented
        /// by the current point.
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public int Get(string elementId)
        {
            return ElementValues[elementId];
        }
    }
}
