// <copyright file="CalendarSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

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

		public CalendarPoint Create(decimal julianDate)
		{
			return new CalendarPoint(this, julianDate);
		}

		public CalendarElementValueCollection GetValues(decimal julianDate)
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
