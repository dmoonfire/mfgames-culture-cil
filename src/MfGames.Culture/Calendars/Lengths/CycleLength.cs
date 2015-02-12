// <copyright file="CycleLength.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using Fractions;

namespace MfGames.Culture.Calendars.Lengths
{
	public abstract class CycleLength
	{
		#region Public Methods and Operators

		public abstract Fraction CalculateIndex(
			string id,
			CalendarElementValueCollection values,
			Fraction relativeJulianDay);

		public abstract Fraction CalculateLength(
			string id,
			CalendarElementValueCollection desiredValues,
			CalendarElementValueCollection currentValues);

		#endregion
	}
}
