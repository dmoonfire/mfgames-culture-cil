// <copyright file="CalendarSystem.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System.Collections.Generic;
using System.Linq;

using MfGames.Culture.Calendars.Cycles;

namespace MfGames.Culture.Calendars
{
    /// <summary>
    /// A dynamic calendar that defines all of the elements via an XML file and
    /// allows for cultures and languages not defined in System.Globalization.
    /// </summary>
    public class CalendarSystem
    {
        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        public CalendarSystem()
        {
            Elements = new CalendarElementCollection<CalendarElement>();
        }

        #endregion

        #region Public Properties

        public CalendarElementCollection<CalendarElement> Elements { get;
            private set; }

        public IEnumerable<OpenCycle> OpenCycles
        {
            get { return Elements.OfType<OpenCycle>(); }
        }

        #endregion

        #region Public Methods and Operators

        public void Add(OpenCycle openCycle)
        {
            openCycle.Calendar = this;
            Elements.Add(openCycle);
        }

        public CalendarPoint Create(decimal julianDate)
        {
            return new CalendarPoint(this, julianDate);
        }

        public CalendarElementValueCollection GetValues(decimal julianDate)
        {
            // Create a collection and then initialized it with all of the
            // open cycles which will cascade down into the rest of the
            // elements.
            var results = new CalendarElementValueCollection();

            foreach (OpenCycle cycle in OpenCycles)
            {
                cycle.Calculate(julianDate, results);
            }

            // Return the resulting value collection.
            return results;
        }

        #endregion
    }
}
