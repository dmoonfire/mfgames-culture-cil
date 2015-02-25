// <copyright file="CompositeCalendarSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

using Fractions;

using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Translations;

namespace MfGames.Culture.Calendars
{
	/// <summary>
	/// Contains zero or more other CalendarSystems, providing a unified
	/// interface to all of them.
	/// </summary>
	public class CompositeCalendarSystem : ICalendarSystem
	{
		#region Fields

		private readonly List<ICalendarSystem> calendars;

		private readonly CompositeTranslationProvider translations;

		#endregion

		#region Constructors and Destructors

		public string TranslationFormat { get { throw new NotImplementedException(); } }

		public CompositeCalendarSystem()
		{
			calendars = new List<ICalendarSystem>();
			translations = new CompositeTranslationProvider();
		}

		#endregion

		#region Public Properties

		public string CannonicalName { get; set; }
		public ITranslationProvider Translations { get { return translations; } }

		#endregion

		#region Public Methods and Operators

		public void Add(ICalendarSystem calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}

			calendars.Remove(calendar);
			calendars.Add(calendar);

			translations.Providers.Remove(calendar.Translations);
			translations.Providers.Add(calendar.Translations);
		}

		public CalendarPoint Create(Fraction julianDate)
		{
			// Go through and add each calendar to a single collection.
			var values = new CalendarElementValueCollection();

			foreach (ICalendarSystem calendar in calendars)
			{
				CalendarPoint calendarPoint = calendar.Create(julianDate);

				values.AddRange(calendarPoint.Values);
			}

			// Create the resulting point and return it.
			var point = new CalendarPoint(this, values, julianDate);
			return point;
		}

		public CalendarPoint Create(CalendarElementValueCollection values)
		{
			Fraction julianDate = GetJulianDate(values);
			CalendarPoint point = Create(julianDate);
			return point;
		}

		public ICollection<Cycle> GetCycles()
		{
			var list = new List<Cycle>();

			foreach (ICalendarSystem calendar in calendars)
			{
				list.AddRange(calendar.GetCycles());
			}

			return list;
		}

		public Fraction GetJulianDate(CalendarElementValueCollection desiredValues)
		{
			var julianDate = new Fraction();

			foreach (ICalendarSystem calendar in calendars)
			{
				Fraction calendarJulianDate = calendar.GetJulianDate(desiredValues);

				julianDate += calendarJulianDate;
			}

			return julianDate;
		}

		public CalendarElementValueCollection GetValues(Fraction julianDate)
		{
			var values = new CalendarElementValueCollection();

			foreach (ICalendarSystem calendar in calendars)
			{
				CalendarElementValueCollection calendarValues =
					calendar.GetValues(julianDate);

				values.AddRange(calendarValues);
			}

			return values;
		}

		#endregion
	}
}
