// <copyright file="ConstantLengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Lengths
{
    public class ConstantLengthLogic : ILengthLogic
    {
        #region Constructors and Destructors

        public ConstantLengthLogic(decimal julianDays)
        {
            JulianDays = julianDays;
        }

        #endregion

        #region Public Properties

        public decimal JulianDays { get; set; }

        #endregion
    }
}
