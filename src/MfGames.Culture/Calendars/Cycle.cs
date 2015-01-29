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
    public class Cycle
    {
        public Cycle(string id, Basis basis)
            : this()
        {
            this.Id = id;
            this.Basis = basis;
        }

        public Cycle()
        {
        }

        /// <summary>
        /// Contains the unique identifier of the cycle.
        /// </summary>
        /// <remarks>
        /// This should be a space-separated, title cased name.
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the parent cycle that this cycle is a member of. The presence of this
        /// also indicates that the index will be reset every time the parent cycle changes.
        /// </summary>
        public Cycle ParentCycle { get; set; }

        /// <summary>
        /// Contains the basis of the calendar, which is either another Cycle within the
        /// calendar or Basis.JulianDay.
        /// </summary>
        public Basis Basis { get; set; }
    }
}