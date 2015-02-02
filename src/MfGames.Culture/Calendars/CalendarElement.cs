// <copyright file="CalendarElement.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;

    /// <summary>
    /// Defines the common logic between all the calendar elements that are
    /// referenced throughout the system.
    /// </summary>
    public abstract class CalendarElement : ICalendarElement
    {
        protected CalendarElement(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(
                    "Identifier cannot be null or blank.",
                    "id");
            }

            Id = id;
            CalculatedCycles = new CalendarElementCollection<CalendarElement>();
        }

        public CalendarSystem Calendar { get; set; }
        public abstract bool IsValueElement { get; }

        /// <summary>
        /// Contains the identifier for the calendar element.
        /// </summary>
        public string Id { get; private set; }

        public CalendarElementCollection<CalendarElement> CalculatedCycles
        {
            get; set;
        }

        public virtual void CalculateIndex(CalculateIndexArguments args)
        {
            // Loop through all the calculated cycles. We have to reset the Julian Date
            // each time otherwise the destructive call with invalidate the
            // successive cycles.
            decimal baseJulianDate = args.JulianDate;
            bool exceededCycle = args.ExceededCycle;

            foreach (CalendarElement calculatedCycle in CalculatedCycles)
            {
                args.JulianDate = baseJulianDate;
                calculatedCycle.CalculateIndex(args);
            }

            // Restore the arguments back to the base.
            args.JulianDate = baseJulianDate;
            args.ExceededCycle = exceededCycle;
        }
    }
}
