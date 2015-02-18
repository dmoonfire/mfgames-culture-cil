// <copyright file="DateTimeExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using Fractions;

using MfGames.Extensions.System;

namespace MfGames.Culture.Extensions.System
{
	public static class DateTimeExtensions
	{
		#region Public Methods and Operators

		public static Fraction ToJulianDateFraction(this DateTime dateTime)
		{
			// Getting the date for the year, month, and day is easier via
			// the MfGames library.
			var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
			decimal dateJulian = date.ToJulianDateDecimal();
			var dateFraction = new Fraction(dateJulian);

			// The fractional days are easier via a fraction.
			Fraction timeFraction = new Fraction(dateTime.Hour, 24)
				+ new Fraction(dateTime.Minute, 24 * 60)
				+ new Fraction(dateTime.Second, 24 * 60 * 60);

			// Combine them together for the fractional Julian Date.
			Fraction results = dateFraction + timeFraction;

			return results;
		}

		#endregion
	}
}
