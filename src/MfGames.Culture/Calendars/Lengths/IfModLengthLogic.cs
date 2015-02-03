// <copyright file="IfModLengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Lengths
{
    using System;

    public class IfModLengthLogic : ILengthLogic
    {
        public string DividendRef { get; set; }
        public int Divisor { get; set; }
        public decimal JulianDays { get; set; }

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
    }
}
