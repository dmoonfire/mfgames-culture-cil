// <copyright file="CalendarSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;

using Fractions;

using MfGames.Extensions.System;
using MfGames.Culture.Calendars.Cycles;

namespace MfGames.Culture.Calendars
{
	/// <summary>
	/// A dynamic calendar that defines all of the elements via an XML file and
	/// allows for cultures and languages not defined in System.Globalization.
	/// </summary>
	public class CalendarSystem
	{
		#region Constructors and Destructors

		/// <summary>
		/// </summary>
		public CalendarSystem()
		{
			Cycles = new CalendarElementCollection<Cycle>();
		}

		#endregion

		#region Public Properties

		public CalendarElementCollection<Cycle> Cycles { get; private set; }

		#endregion

		#region Public Methods and Operators

		public void Add(Cycle cycle)
		{
			cycle.Calendar = this;
			Cycles.Add(cycle);
		}

		public CalendarPoint Create(DateTime date)
		{
			// We can safely convert the year, month, and day to Julian without
			// losing any fidelity.
			var datePart = new DateTime(date.Year, date.Month, date.Day);
			var dateJulian = MfGames.Extensions.System.DateTimeExtensions.ToJulianDate(datePart);
			
			var dateFraction = new Fraction(dateJulian);

			// For the time, we go straight to fractions.
			var timeFraction = new Fraction(date.Hour, 24)
				+ new Fraction(date.Minute, 1440)
				+ new Fraction(date.Second, 86400);

			// Combine the two together and pass it along.
			var dateTimeFraction = dateFraction + timeFraction;
			var results = Create(dateTimeFraction);

			return results;
		}

		public CalendarPoint Create(decimal julianDate)
		{
			var fractionalJulianDate = new Fraction(julianDate);
			return Create(fractionalJulianDate);
		}

		public CalendarPoint Create(Fraction julianDate)
		{
			var values = GetValues(julianDate);
			return new CalendarPoint(this, values, julianDate);
		}

		public CalendarElementValueCollection GetValues(Fraction julianDate)
		{
			// Create a collection and then initialized it with all of the
			// open cycles which will cascade down into the rest of the
			// elements.
			var results = new CalendarElementValueCollection();

			foreach (Cycle cycle in Cycles)
			{
				cycle.Calculate(julianDate, results);
			}

			// Return the resulting value collection.
			return results;
		}

		#endregion
	}
}
