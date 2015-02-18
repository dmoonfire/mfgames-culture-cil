// <copyright file="CalendarSystemExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using Fractions;

using MfGames.Culture.Extensions.System;

namespace MfGames.Culture.Calendars
{
	public static class CalendarSystemExtensions
	{
		#region Public Methods and Operators

		public static CalendarPoint Create(
			this ICalendarSystem calendar,
			DateTime dateTime)
		{
			Fraction julianDate = dateTime.ToJulianDateFraction();
			return calendar.Create(julianDate);
		}

		public static CalendarPoint Create(
			this ICalendarSystem calendar,
			decimal julianDate)
		{
			var julianDateFraction = new Fraction(julianDate);
			return calendar.Create(julianDateFraction);
		}

		#endregion
	}
}
