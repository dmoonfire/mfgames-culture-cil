// <copyright file="ClosedCycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    /// <summary>
    /// Defines a cycle that is repeated within the context of another cycle. For example,
    /// a month inisde a year, a day inside the month.
    /// </summary>
    public class ClosedCycle : Cycle
    {
        public ClosedCycle(string id)
            : base(id)
        {
        }

        public ClosedCycle(string id, Basis basis)
            : base(id, basis)
        {
        }

        public override bool IsValueElement { get { return false; } }

        public override void CalculateIndex(
            CalendarElementValueDictionary values,
            decimal julianDayNumber)
        {
            // Closed cycles don't really have an ID.
            values[Id] = 0;

            // Pass it directly into the basis.
            Basis.CalculateIndex(values, julianDayNumber);
        }
    }
}
