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
        public decimal JulianDateOffset { get; set; }

        public void CalculateIndex(
            CalendarElementValueDictionary values,
            decimal julianDate)
        {
            // Reset the elements to zero.
            foreach (CalendarElement element in Calendar.ValueElements)
            {
                values[element.Id] = 0;
            }

            // To avoid going through the elements twice, we recurse into the various cycles
            // until we get down to the Julian Day calculation which handles subtracting the
            // actual days.

            // Start by adjusting by the offset to ensure correct calculations.
            julianDate -= JulianDateOffset;

            // Call ourselves with a referenced version.
            var args = new CalculateIndexArguments(values, julianDate);
            CalculateIndex(args);
        }

        public override void CalculateIndex(CalculateIndexArguments args)
        {
            // Loop through the open cycle until we run out of days to process.
            while (args.JulianDate > 0)
            {
                // Calculate the index of the basis.
                Basis.CalculateIndex(args);

                // Increment the index for the loop only if we are going to run
                // again or if we exceeded our cycle.
                if (args.JulianDate > 0m || args.ExceededCycle)
                {
                    args.Elements[Id]++;
                }

                // If we've run out of days, then stop processing.
                if (args.JulianDate <= 0m)
                {
                    break;
                }
            }

            // Calculate the additional cycles.
            base.CalculateIndex(args);
        }
    }
}
