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
        }

        public CalendarSystem Calendar { get; set; }
        public abstract bool IsValueElement { get; }

        /// <summary>
        /// Contains the identifier for the calendar element.
        /// </summary>
        public string Id { get; private set; }

        public abstract void CalculateIndex(
            CalendarElementValueDictionary values,
            decimal julianDayNumber);
    }
}
