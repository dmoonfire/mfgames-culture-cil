// <copyright file="OpenCycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    /// <summary>
    /// Defines an open-ended cycle that doesn't have a containing cycle and continually
    /// increases without resetting or cycling.
    /// </summary>
    public class OpenCycle : Cycle
    {
        public OpenCycle(string id)
            : base(id)
        {
        }

        public OpenCycle(string id, Basis basis)
            : base(id, basis)
        {
        }

        public override bool IsValueElement { get { return true; } }
        public decimal JulianDayNumberOffset { get; set; }

        public override void CalculateIndex(
            CalendarElementValueDictionary values,
            decimal julianDayNumber)
        {
            // Adjust by the offset for the calendar.
            julianDayNumber -= JulianDayNumberOffset;

            // Reset the elements to zero.
            foreach (CalendarElement element in Calendar.ValueElements)
            {
                values[element.Id] = 0;
            }

            // Iterate through various permutations of the given open cycle until
            // we find the cycle that the JDN is contained within. This is done
            // by starting with the first one and then calculating the length.
            while (julianDayNumber > 0)
            {
                // We need to get the length of the next element.
                decimal length = GetLength(values);

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

            // Now that we have the open cycle index, we can calculate the remaining
            // elements with the remaining JDN.
            Basis.CalculateIndex(values, julianDayNumber);
        }
    }
}
