// <copyright file="CycleCalculation.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Calculations
{
    public abstract class CycleCalculation : ICycleCalculation
    {
        #region Constructors and Destructors

        protected CycleCalculation(string elementRef, int constant)
        {
            ElementRef = elementRef;
            Constant = constant;
        }

        #endregion

        #region Public Properties

        public int Constant { get; set; }
        public string ElementRef { get; set; }

        #endregion

        #region Public Methods and Operators

        public int GetIndex(CalendarElementValueCollection values)
        {
            int elementValue = values[ElementRef];

            return GetIndex(elementValue);
        }

        #endregion

        #region Methods

        protected abstract int GetIndex(int elementValue);

        #endregion
    }
}
