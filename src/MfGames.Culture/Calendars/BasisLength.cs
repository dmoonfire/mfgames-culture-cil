// <copyright file="BasisLength.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    public class BasisLength : IBasisLengthLogic
    {
        public BasisLength()
        {
        }

        public BasisLength(int count)
        {
            this.Count = count;
        }

        public int Count { get; set; }
    }
}
