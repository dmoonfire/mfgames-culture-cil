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
            decimal openJulianDate = args.JulianDate;

            while (args.JulianDate > 0)
            {
                // Update the julian date for this cycle.
                openJulianDate = args.JulianDate;

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

            // Calculate the additional cycles. We have to use a new set of arguments
            // because the Julian Date has been modified by the inner cycles.
            CalculateIndexArguments args2 = new CalculateIndexArguments(args.Elements, openJulianDate);
            base.CalculateIndex(args2);
        }
    }
}
