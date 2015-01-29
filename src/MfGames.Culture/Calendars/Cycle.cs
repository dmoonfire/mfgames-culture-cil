// <copyright file="Cycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;

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

        protected Cycle(string id, Basis basis)
            : this(id)
        {
            if (basis == null)
            {
                throw new ArgumentNullException("basis");
            }

            Basis = basis;
        }

        public Basis Basis { get; set; }

        public decimal GetLength(CalendarElementValueDictionary values)
        {
            return Basis.GetLength(values);
        }
    }
}
