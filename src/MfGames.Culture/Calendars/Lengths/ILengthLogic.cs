// <copyright file="ILengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Lengths
{
    /// <summary>
    /// Defines the signatures for something that calculates the length of a
    /// cycle.
    /// </summary>
    public interface ILengthLogic
    {
        #region Public Properties

        /// <summary>
        /// Gets a flag whether the logic is a constant value.
        /// </summary>
        bool IsConstant { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines if the logic is appliable to the current value. This is
        /// used to filter in specific months or years.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        bool CanHandle(CalendarElementValueCollection values);

        /// <summary>
        /// Calculates the length based on the values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        decimal GetLength(CalendarElementValueCollection values);

        #endregion
    }
}
