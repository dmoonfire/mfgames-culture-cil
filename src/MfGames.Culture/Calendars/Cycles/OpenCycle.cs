// <copyright file="OpenCycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System;
using System.Collections.Generic;

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

        #endregion

        #region Public Methods and Operators

        public void Calculate(
            decimal julianDate,
            CalendarElementValueCollection values)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
