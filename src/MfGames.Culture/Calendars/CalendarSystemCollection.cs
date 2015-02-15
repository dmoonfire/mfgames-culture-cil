// <copyright file="CalendarSystemCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections;
using System.Collections.Generic;

using Fractions;

using MfGames.Culture.Calendars.Cycles;

namespace MfGames.Culture.Calendars
{
	public class CalendarSystemCollection : ICollection<CalendarSystem>
	{
		#region Fields

		private readonly List<CalendarSystem> calendars;

		#endregion

		#region Constructors and Destructors

		public CalendarSystemCollection()
		{
			calendars = new List<CalendarSystem>();
		}

		#endregion

		#region Public Properties

		public int Count { get { return calendars.Count; } }
		public bool IsReadOnly { get { return false; } }

		#endregion

		#region Public Methods and Operators

		public void Add(CalendarSystem item)
		{
			calendars.Add(item);
		}

		public void Clear()
		{
			calendars.Clear();
		}

		public bool Contains(CalendarSystem item)
		{
			return calendars.Contains(item);
		}

		public void CopyTo(CalendarSystem[] array, int arrayIndex)
		{
			calendars.CopyTo(array, arrayIndex);
		}

		public CalendarPoint Create(Fraction julianDate)
		{
			// Create a master collection of values.
			var values = new CalendarElementValueCollection();
			var cycles = new List<Cycle>();

			// Go through each calendar in the collection and add their values to the main one.
			foreach (CalendarSystem calendar in calendars)
			{
				// Get all the cycles from the calendar.
				ICollection<Cycle> calendarCycles = calendar.GetCycles();

				cycles.AddRange(calendarCycles);

				// Get all the values from the calendar.
				CalendarElementValueCollection calendarValues =
					calendar.GetValues(julianDate);

				foreach (KeyValuePair<string, int> pair in calendarValues)
				{
					values[pair.Key] = pair.Value;
				}
			}

			// Create a calendar point from the resulting collection.
			//return new CalendarPoint(cycles, values, julianDate);
			throw new NotImplementedException();
		}

		public IEnumerator<CalendarSystem> GetEnumerator()
		{
			return calendars.GetEnumerator();
		}

		public bool Remove(CalendarSystem item)
		{
			return calendars.Remove(item);
		}

		#endregion

		#region Explicit Interface Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
