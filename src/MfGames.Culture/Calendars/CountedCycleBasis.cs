// <copyright file="CountedCycleBasis.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;

    public class CountedCycleBasis : Basis
    {
        public CountedCycleBasis(string id, ClosedCycle cycle, int count)
            : base(id)
        {
            Cycle = cycle;
            Count = count;
        }

        public ClosedCycle Cycle { get; set; }
        public int Count { get; set; }

        public override decimal GetLength(CalendarElementValueDictionary values)
        {
            throw new NotImplementedException();
        }
    }
}
