// <copyright file="LogicCycleLength.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System;

namespace MfGames.Culture.Calendars.Lengths
{
    public class LogicCycleLength : CycleLength
    {
        #region Constructors and Destructors

        public LogicCycleLength(int number, decimal julianDays)
            : this(number, new ConstantLengthLogic(julianDays))
        {
        }

        public LogicCycleLength(params ILengthLogic[] lengthLogics)
            : this(1, lengthLogics)
        {
        }

        public LogicCycleLength(int number, params ILengthLogic[] lengthLogics)
        {
            Number = number;
            LengthLogics = lengthLogics;
        }

        #endregion

        #region Public Properties

        public ILengthLogic[] LengthLogics { get; set; }
        public int Number { get; private set; }

        #endregion

        #region Public Methods and Operators

        public override decimal CalculateIndex(
            string id,
            CalendarElementValueCollection values,
            decimal relativeJulianDay)
        {
            // Make sure we have the ID.
            if (!values.ContainsKey(id))
            {
                values[id] = 0;
            }

            // If we have a single length logic and its a constant, then we
            // can easily "fast-forward" through the process.
            if (LengthLogics.Length == 1 && LengthLogics[0].IsConstant)
            {
                // Get the length of the constant.
                decimal length = LengthLogics[0].GetLength(values);

                // Figure out the index from the relative. We have to convert
                // to longs because division with decimals gives us partials
                // and we want full indexes.
                long count = (long)relativeJulianDay / (long)length;

                // For every count items, we add this.Number to the index
                // and subtract length from the RJD.
                values[id] += (int)(Number * count);
                relativeJulianDay -= length * count;

                // Return the resulting days.
                return relativeJulianDay;
            }

            // For non-constants, we have iterate through the logic until we
            // run out of dates. Every time we do, we increment the index
            // to calculate the next one (this allows us to handle leap years).
            while (relativeJulianDay > 0m)
            {
                // Calculate the index for the current element. This uses the
                // local version which handles multiple logic fields.
                decimal length = GetFirstValidLength(values);

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
                break;
            }

            // Return the resulting relative days.
            return relativeJulianDay;
        }

        #endregion

        #region Methods

        private decimal GetFirstValidLength(
            CalendarElementValueCollection values)
        {
            // Loop through the logics until we find one that is good.
            foreach (ILengthLogic lengthLogic in LengthLogics)
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
