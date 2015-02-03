// <copyright file="ConstantLengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Lengths
{
    public class ConstantLengthLogic : ILengthLogic
    {
        public decimal JulianDays { get; set; }

        public ConstantLengthLogic(decimal julianDays)
        {
            JulianDays = julianDays;
        }
    }
}
