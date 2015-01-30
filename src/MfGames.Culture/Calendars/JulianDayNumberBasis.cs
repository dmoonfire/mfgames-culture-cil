// <copyright file="JulianDayNumberBasis.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    public class JulianDayNumberBasis : Basis
    {
        public JulianDayNumberBasis()
            : this(1.0m)
        {
        }

        public JulianDayNumberBasis(decimal length)
            : base("Julian Date Number")
        {
            Length = length;
        }

        public decimal Length { get; set; }

        public override void CalculateIndex(
            CalendarElementValueDictionary values,
            decimal julianDayNumber)
        {
        }

        public override decimal GetLength(CalendarElementValueDictionary values)
        {
            return Length;
        }
    }
}
