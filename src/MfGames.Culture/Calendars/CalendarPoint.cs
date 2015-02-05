// <copyright file="CalendarPoint.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using Fractions;

using MfGames.Culture.Calendars.Cycles;

namespace MfGames.Culture.Calendars
{
	/// <summary>
	/// Represents a single temporal point within a specific calendar system.
	/// </summary>
	public class CalendarPoint : DynamicObject
	{
		#region Constructors and Destructors

		public CalendarPoint(
			CalendarSystem calendar,
			CalendarElementValueCollection values,
			Fraction julianDate)
		{
			// Establish the contracts.
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}

			// Save the member variables.
			Calendar = calendar;
			JulianDate = julianDate;
			Values = values;
		}

		#endregion

		#region Public Properties

		public CalendarSystem Calendar { get; private set; }
		public Fraction JulianDate { get; private set; }
		public CalendarElementValueCollection Values { get; private set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Retrieves the zero-based index for an element (basis or cycle) represented
		/// by the current point.
		/// </summary>
		/// <param name="elementId"></param>
		/// <returns></returns>
		public int Get(string elementId)
		{
			if (!Values.ContainsKey(elementId))
			{
				throw new KeyNotFoundException(
					"Cannot find calendar element: " + elementId + ".");
			}

			return Values[elementId];
		}

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return Calendar.Cycles.Select(e => e.PascalId);
		}

		public override bool TryGetMember(
			GetMemberBinder binder,
			out object result)
		{
			// See if we can find the element.
			CalendarElement element = GetElement(binder.Name, Calendar.Cycles);

			if (element == null)
			{
				throw new IndexOutOfRangeException(
					"Cannot find calendar element: " + binder.Name + ".");
			}

			// See if we are missing an element.
			if (Values.ContainsKey(element.Id))
			{
				// Return the resulting ID.
				result = Values[element.Id];
				return true;
			}

			// If we can't find it, blow up.
			throw new InvalidOperationException(
				"Cannot find " + element.Id + " inside calendar point.");
		}

		#endregion

		#region Methods

		private CalendarElement GetElement(
			string name,
			CalendarElementCollection<Cycle> cycles)
		{
			// Look through all the elements in the calendar.
			foreach (Cycle cycle in cycles)
			{
				// First check this value.
				if (cycle.PascalId == name)
				{
					return cycle;
				}

				// Check inner cycles.
				CalendarElement element = GetElement(name, cycle.Cycles);

				if (element != null)
				{
					return element;
				}
			}

			// If we get out of the loop, we can't find it.
			return null;
		}

		#endregion
	}
}
