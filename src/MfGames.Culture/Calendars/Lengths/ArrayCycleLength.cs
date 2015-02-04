// <copyright file="ArrayCycleLength.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System;
using System.Linq;

namespace MfGames.Culture.Calendars.Lengths
{
    public class ArrayCycleLength : CycleLength
    {
        #region Constructors and Destructors

        public ArrayCycleLength(
            params ILengthLogic[][] lengths)
        {
            Lengths = lengths;
        }

        #endregion

        #region Public Properties

        public string Id { get; set; }
        public ILengthLogic[][] Lengths { get; set; }

        #endregion

        #region Public Methods and Operators

        public override decimal CalculateIndex(
            string id,
            CalendarElementValueCollection values,
            decimal relativeJulianDay)
        {
            // Figure out the index based on the relative day.
            for (var index = 0; index < Lengths.Count(); index++)
            {
                // Set the index in the values container.
                values[id] = index;

                // If we are at zero days, stop processing.
                if (relativeJulianDay == 0m)
                {
                    break;
                }

                // For non-constants, we have iterate through the logic until we
                // run out of dates. Every time we do, we increment the index
                // to calculate the next one (this allows us to handle leap years).
                while (relativeJulianDay > 0m)
                {
                    // Calculate the index for the current element. This uses the
                    // local version which handles multiple logic fields.
                    decimal length = GetFirstValidLength(values, Lengths[index]);

                    if (length <= relativeJulianDay)
                    {
                        // There is enough days to completely have this element.
                        relativeJulianDay -= length;

                        // We are going to loop, so increment the count.
                        values[id]++;
                        continue;
                    }

                    // The length exceeds the amount of time, which means that
                    // the point in time is somewhere inside this cycle.
                    index = Lengths.Count();
                    break;
                }
            }

            // Return the remaining relative.
            return relativeJulianDay;
        }

        #endregion

        #region Methods

        private decimal GetFirstValidLength(
            CalendarElementValueCollection values,
            ILengthLogic[] lengthLogics)
        {
            // Loop through the logics until we find one that is good.
            foreach (ILengthLogic lengthLogic in lengthLogics)
            {
                if (lengthLogic.CanHandle(values))
                {
                    decimal results = lengthLogic.GetLength(values);
                    return results;
                }
            }

            // If we get out of the loop, we need to fail quickly.
            throw new IndexOutOfRangeException(
                "Cannot find a valid length logic.");
        }

        #endregion
    }
}
