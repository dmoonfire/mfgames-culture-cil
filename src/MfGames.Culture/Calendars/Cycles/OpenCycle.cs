// <copyright file="OpenCycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Cycles
{
    using System.Collections.Generic;

    using MfGames.Culture.Calendars.Lengths;

    public class OpenCycle : CalendarElement
    {
        public IList<CycleLength> Lengths { get; private set; }

        public OpenCycle(string id)
            : base(id)
        {
            Lengths = new List<CycleLength>();
        }

        public void Calculate(decimal julianDate, CalendarElementValueCollection values)
        {
            throw new System.NotImplementedException();
        }
    }
}
