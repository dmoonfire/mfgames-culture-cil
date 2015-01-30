// <copyright file="CountedCycleBasis.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
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

        public override void CalculateIndex(
            CalendarElementValueDictionary values,
            decimal julianDayNumber)
        {
            // Iterate through various permutations of the given open cycle until
            // we find the cycle that the JDN is contained within. This is done
            // by starting with the first one and then calculating the length.
            values[Id] = 0;

            while (julianDayNumber > 0)
            {
                // We need to get the length of the next element.
                decimal length = Cycle.GetLength(values);

                // Check to see if the length is greater than the JDN. If it is, then
                // previous index is the correct one. Otherwise, we increment.
                if (julianDayNumber < length)
                {
                    break;
                }

                // We have to advance it, so reduce the JDN by our length and repeat.
                julianDayNumber -= length;
                values[Id]++;
            }

            // Now that we have the counted cycle, add in the cycle itself.
            Cycle.CalculateIndex(values, julianDayNumber);
        }

        public override decimal GetLength(CalendarElementValueDictionary values)
        {
            // Build up the length of the total amount.
            var length = 0.0m;

            for (var index = 0; index < Count; index++)
            {
                values[Cycle.Id] = index;
                length += Cycle.GetLength(values);
            }

            // Return the resulting length.
            return length;
        }
    }
}
