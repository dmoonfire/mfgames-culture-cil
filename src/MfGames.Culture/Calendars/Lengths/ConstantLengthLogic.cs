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

        public bool IsConstant { get { return true; } }
        public decimal JulianDays { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool CanHandle(CalendarElementValueCollection values)
        {
            return true;
        }

        public decimal GetLength(CalendarElementValueCollection values)
        {
            return JulianDays;
        }

        #endregion
    }
}
