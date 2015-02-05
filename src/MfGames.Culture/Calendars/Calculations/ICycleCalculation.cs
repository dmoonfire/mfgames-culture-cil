// <copyright file="ICycleCalculation.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

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
