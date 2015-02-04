// <copyright file="DivCycleCalculation.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Calculations
{
    /// <summary>
    /// A cycle calculation that takes a given value and does an integer
    /// division on it.
    /// </summary>
    public class DivCycleCalculation : CycleCalculation
    {
        #region Constructors and Destructors

        public DivCycleCalculation(string elementRef, int constant)
            : base(elementRef, constant)
        {
        }

        #endregion

        #region Methods

        protected override int GetIndex(int elementValue)
        {
            return elementValue / Constant;
        }

        #endregion
    }
}
