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

        /// <summary>
        /// Gets the unique identifier for this element. This cannot be reused
        /// for any other element with in the same calendar.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Returns the Pascal-formatted name of the element.
        /// </summary>
        public string PascalId
        {
            get { return Id.Replace(" of ", " Of ").Replace(" ", ""); }
        }

        public CalendarSystem Calendar { get; set; }
    }

    public interface ICalendarElement
    {
    }
}
