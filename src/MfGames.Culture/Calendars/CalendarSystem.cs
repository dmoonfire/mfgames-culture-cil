// <copyright file="CalendarSystem.cs" company="Moonfire Games">
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
using MfGames.HierarchicalPaths;

namespace MfGames.Culture.Calendars
{
	/// <summary>
	/// A dynamic calendar that defines all of the elements via an XML file and
	/// allows for cultures and languages not defined in System.Globalization.
	/// </summary>
	public class CalendarSystem : ICalendarSystem
	{
		#region Constructors and Destructors

		/// <summary>
		/// </summary>
		public CalendarSystem()
		{
			Cycles = new CalendarElementCollection<Cycle>();
		}

		public CalendarSystem(ITranslationProvider translations)
			: this()
		{
			if (translations == null)
			{
				throw new ArgumentNullException("translations");
			}

			Translations = translations;
		}

		#endregion

		#region Public Properties

		public string CannonicalName { get; set; }
		public CalendarElementCollection<Cycle> Cycles { get; private set; }
		public HierarchicalPath TranslationPath { get; set; }
		public ITranslationProvider Translations { get; set; }

		#endregion

		#region Public Methods and Operators

		public void Add(Cycle cycle)
		{
			cycle.Calendar = this;
			Cycles.Add(cycle);
		}

		public CalendarPoint Create(Fraction julianDate)
		{
			CalendarElementValueCollection values = GetValues(julianDate);
			var results = new CalendarPoint(this, values, julianDate);

			return results;
		}

		public CalendarPoint Create(CalendarElementValueCollection values)
		{
			Fraction julianDate = GetJulianDate(values);
			CalendarPoint point = Create(julianDate);
			return point;
		}

		/// <summary>
		/// Recursively retrieves all of the cycles.
		/// </summary>
		/// <returns></returns>
		public ICollection<Cycle> GetCycles()
		{
			var list = new List<Cycle>();

			foreach (Cycle cycle in Cycles)
			{
				GetCycles(list, cycle);
			}

			return list;
		}

		public Fraction GetJulianDate(CalendarElementValueCollection desiredValues)
		{
			// Go through the open cycles and use the first one we can find
			// in the values collection.
			foreach (Cycle cycle in Cycles)
			{
				// If we aren't in the collection, then move to the next one.
				if (!desiredValues.ContainsKey(cycle.Id))
				{
					continue;
				}

				// We are going to use this cycle as the basis for the
				// calculation. We also need a second set of values to help
				// the system calculate it's lengths.
				var currentValues = new CalendarElementValueCollection();

				Fraction julianDate = cycle.CalculateJulianDate(
					desiredValues,
					currentValues);

				return julianDate;
			}

			// If we break out of the loop, we don't have anything to calculate.
			return new Fraction();
		}

		public CalendarElementValueCollection GetValues(Fraction julianDate)
		{
			// Create a collection and then initialized it with all of the
			// open cycles which will cascade down into the rest of the
			// elements.
			var results = new CalendarElementValueCollection();

			foreach (Cycle cycle in Cycles)
			{
				cycle.CalculateValues(julianDate, results);
			}

			// Return the resulting value collection.
			return results;
		}

		#endregion

		#region Methods

		private void GetCycles(List<Cycle> list, Cycle cycle)
		{
			// If we are blank, then skip it.
			if (cycle == null)
			{
				return;
			}

			// Add this cycle to the list.
			list.Add(cycle);

			// Loop through the child cycles.
			foreach (Cycle child in cycle.Cycles)
			{
				GetCycles(list, child);
			}
		}

		#endregion
	}
}
