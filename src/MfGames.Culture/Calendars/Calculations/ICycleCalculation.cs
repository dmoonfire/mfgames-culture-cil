// <copyright file="ICycleCalculation.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars.Calculations
{
    /// <summary>
    /// Defines the signature for a calculation based on values.
    /// </summary>
    public interface ICycleCalculation
    {
        #region Public Methods and Operators

        int GetIndex(CalendarElementValueCollection values);

        #endregion
    }
}
