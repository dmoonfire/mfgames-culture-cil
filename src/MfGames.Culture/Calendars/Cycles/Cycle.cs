// <copyright file="Cycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Cycles
{
    public abstract class Cycle : CalendarElement
    {
        #region Constructors and Destructors

        protected Cycle(string id)
            : base(id)
        {
            Cycles = new CalendarElementCollection<Cycle>();
        }

        #endregion

        #region Public Properties

        public CalendarElementCollection<Cycle> Cycles { get; private set; }

        /// <summary>
        /// Gets or sets an offset for Julian Dates and the "zero point" of
        /// the cycle.
        /// </summary>
        public decimal JulianDateOffset { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void Calculate(
            decimal julianDate,
            CalendarElementValueCollection values)
        {
            // Now that we have a relative day, we can calculate the inner
            // cycles to get their values.
            foreach (Cycle cycle in Cycles)
            {
                cycle.Calculate(julianDate, values);
            }
        }

        #endregion
    }
}
