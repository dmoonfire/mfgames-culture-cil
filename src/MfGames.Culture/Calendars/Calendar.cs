// --------------------------------------------------------------------------------
// <copyright file="Calendar.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------

namespace MfGames.Culture.Calendars
{
    using System.Collections.Generic;

    /// <summary>
    /// A dynamic calendar that defines all of the elements via an XML file and
    /// allows for cultures and languages not defined in System.Globalization.
    /// </summary>
    public class Calendar
    {
        /// <summary>
        /// </summary>
        public Calendar()
        {
            this.Variables = new Dictionary<string, string>();
            this.Cycles = new CycleCollection();
        }

        /// <summary>
        /// Contains a list of all known cycles for the calendar.
        /// </summary>
        public CycleCollection Cycles { get; set; }

        /// <summary>
        /// Contains a list of string variables, which are expanded using "$(variableName)" and
        /// additional formatting.
        /// </summary>
        public Dictionary<string, string> Variables { get; private set; }
    }
}