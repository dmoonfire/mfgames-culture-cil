// <copyright file="CompositeCalendarSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

using Fractions;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Translations;
using MfGames.HierarchicalPaths;

namespace MfGames.Culture.Tests.Calendars
{
	/// <summary>
	/// Contains zero or more other CalendarSystems, providing a unified
	/// interface to all of them.
	/// </summary>
	public class CompositeCalendarSystem : ICalendarSystem
	{
		#region Fields

		private readonly List<ICalendarSystem> calendars;

		#endregion

		#region Constructors and Destructors

		public CompositeCalendarSystem()
		{
			calendars = new List<ICalendarSystem>();
		}

		#endregion

		#region Public Properties

		public string CannonicalName { get; set; }
		public HierarchicalPath TranslationPath { get; set; }

		public ITranslationProvider Translations
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

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
			throw new NotImplementedException();
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
