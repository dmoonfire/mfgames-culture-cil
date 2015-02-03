// <copyright file="CalendarPoint.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace MfGames.Culture.Calendars
{
    /// <summary>
    /// Represents a single temporal point within a specific calendar system.
    /// </summary>
    public class CalendarPoint : DynamicObject
    {
        #region Constructors and Destructors

        public CalendarPoint(CalendarSystem calendar, decimal julianDate)
        {
            // Establish the contracts.
            if (calendar == null)
            {
                throw new ArgumentNullException("calendar");
            }

            if (julianDate < 0)
            {
                throw new ArgumentException(
                    "Cannot use a negative Julian Day Number.",
                    "julianDate");
            }

            // Save the member variables.
            Calendar = calendar;
            JulianDate = julianDate;

            // Retrieve the values for the given Julian Date.
            Values = calendar.GetValues(julianDate);
        }

        #endregion

        #region Public Properties

        public CalendarSystem Calendar { get; private set; }
        public decimal JulianDate { get; private set; }
        public CalendarElementValueCollection Values { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Retrieves the zero-based index for an element (basis or cycle) represented
        /// by the current point.
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public int Get(string elementId)
        {
            if (!Values.ContainsKey(elementId))
            {
                throw new KeyNotFoundException(
                    "Cannot find calendar element: " + elementId + ".");
            }

            return Values[elementId];
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Calendar.Elements.Select(e => e.PascalId);
        }

        public override bool TryGetMember(
            GetMemberBinder binder,
            out object result)
        {
            CalendarElement element = Calendar.Elements.First(
                e => e.PascalId == binder.Name);

            result = Values[element.Id];
            return true;
        }

        #endregion
    }
}
