// <copyright file="CycleLength.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Lengths
{
    public class CycleLength
    {
        public ILengthLogic[] LengthLogics { get; set; }

        public CycleLength(int number, decimal julianDays)
            : this(number, new ConstantLengthLogic(julianDays))
        {
        }

        public CycleLength(params ILengthLogic[] lengthLogics)
            : this(1, lengthLogics)
        {
        }

        public CycleLength(int number, params ILengthLogic[] lengthLogics)
        {
            Number = number;
            LengthLogics = lengthLogics;
        }

        public int Number { get; private set; }
    }
}
