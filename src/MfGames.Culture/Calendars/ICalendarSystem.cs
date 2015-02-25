// <copyright file="ICalendarSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

using Fractions;

using MfGames.Culture.Calendars.Cycles;

namespace MfGames.Culture.Calendars
{
	public interface ICalendarSystem
	{
		#region Public Methods and Operators

		CalendarPoint Create(Fraction julianDate);

		CalendarPoint Create(CalendarElementValueCollection values);

		/// <summary>
		/// Recursively retrieves all of the cycles.
		/// </summary>
		/// <returns></returns>
		ICollection<Cycle> GetCycles();

		Fraction GetJulianDate(CalendarElementValueCollection desiredValues);

		CalendarElementValueCollection GetValues(Fraction julianDate);

		#endregion
	}
}
