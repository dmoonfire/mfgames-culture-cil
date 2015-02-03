// <copyright file="OpenCycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System.Collections.Generic;
using System.Diagnostics;

using MfGames.Culture.Calendars.Lengths;

namespace MfGames.Culture.Calendars.Cycles
{
    public class OpenCycle : CalendarElement
    {
        #region Constructors and Destructors

        public OpenCycle(string id)
            : base(id)
        {
            Lengths = new List<CycleLength>();
        }

        #endregion

        #region Public Properties

        public IList<CycleLength> Lengths { get; private set; }

        /// <summary>
        /// Gets or sets an offset for Julian Dates and the "zero point" of
        /// the cycle.
        /// </summary>
        public decimal JulianDateOffset { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Calculate(
            decimal julianDate,
            CalendarElementValueCollection values)
        {
            // We go through the length logics to figure out the points,
            // starting with a seed value of zero. The value coming out with
            // be the relative Julian Day from the beginning of the cycle.
            values[Id] = 0;

            decimal relativeDay = julianDate + JulianDateOffset;

            foreach (CycleLength length in Lengths)
            {
                relativeDay = length.CalculateIndex(Id, values, relativeDay);
            }

            // Coming out of the loop is the final relative Julian Day from
            // the index set internally in the values object.
            Debug.WriteLine("Relative Day: {0}", relativeDay);
        }

        #endregion
    }
}
