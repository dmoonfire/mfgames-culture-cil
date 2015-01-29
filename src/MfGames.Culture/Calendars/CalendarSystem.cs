﻿// <copyright file="CalendarSystem.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A dynamic calendar that defines all of the elements via an XML file and
    /// allows for cultures and languages not defined in System.Globalization.
    /// </summary>
    public class CalendarSystem
    {
        /// <summary>
        /// </summary>
        public CalendarSystem()
        {
            this.Variables = new Dictionary<string, string>();
            this.Elements = new CalendarElementCollection<CalendarElement>();
        }

        /// <summary>
        /// Contains a list of all known elements for the calendar.
        /// </summary>
        public CalendarElementCollection<CalendarElement> Elements { get; set; }

        /// <summary>
        /// Gets the list of open cycles within the calendar.
        /// </summary>
        public IEnumerable<OpenCycle> OpenCycles
        {
            get { return Elements.OfType<OpenCycle>(); }
        }

        /// <summary>
        /// Contains a list of string variables, which are expanded using "$(variableName)" and
        /// additional formatting.
        /// </summary>
        public Dictionary<string, string> Variables { get; private set; }

        public CalendarPoint CreatePoint(decimal julianDayNumber)
        {
            return new CalendarPoint(this, julianDayNumber);
        }
    }
}
