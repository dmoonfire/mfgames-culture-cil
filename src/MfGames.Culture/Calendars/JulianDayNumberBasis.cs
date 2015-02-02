// <copyright file="JulianDateBasis.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    public class JulianDateBasis : Basis
    {
        public JulianDateBasis()
            : this(1.0m)
        {
        }

        public JulianDateBasis(decimal length)
            : base("Julian Date Number")
        {
            Length = length;
        }

        public decimal Length { get; set; }

        public override void CalculateIndex(CalculateIndexArguments args)
        {
            args.JulianDate -= Length;
        }

        public override decimal GetLength(CalendarElementValueDictionary values)
        {
            return Length;
        }
    }
}
