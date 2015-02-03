// <copyright file="IfModLengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System;

namespace MfGames.Culture.Calendars.Lengths
{
    public class IfModLengthLogic : ILengthLogic
    {
        #region Constructors and Destructors

        public IfModLengthLogic(
            string dividendRef,
            int divisor,
            decimal julianDays)
        {
            if (divisor == 0)
            {
                throw new ArgumentException(
                    "Divisor cannot be zero.",
                    "divisor");
            }

            DividendRef = dividendRef;
            Divisor = divisor;
            JulianDays = julianDays;
        }

        #endregion

        #region Public Properties

        public string DividendRef { get; set; }
        public int Divisor { get; set; }
        public bool IsConstant { get { return false; } }
        public decimal JulianDays { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool CanHandle(CalendarElementValueCollection values)
        {
            int dividend = values[DividendRef];
            int results = dividend % Divisor;
            return results == 0;
        }

        public decimal GetLength(CalendarElementValueCollection values)
        {
            return JulianDays;
        }

        #endregion
    }
}
