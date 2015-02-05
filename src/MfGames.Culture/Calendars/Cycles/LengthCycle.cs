// <copyright file="LengthCycle.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

using Fractions;

using MfGames.Culture.Calendars.Lengths;

namespace MfGames.Culture.Calendars.Cycles
{
	/// <summary>
	/// A calendar cycle that's index is based on the length of elements.
	/// </summary>
	public class LengthCycle : Cycle
	{
		#region Constructors and Destructors

		public LengthCycle(string id)
			: base(id)
		{
			Lengths = new List<CycleLength>();
		}

		#endregion

		#region Public Properties

		public IList<CycleLength> Lengths { get; private set; }

		/// <summary>
		/// Gets or sets a flag whether whole days (numbers left of the
		/// decimal) are stripped off after applying the offset.
		/// </summary>
		public bool StripWholeDays { get; set; }

		#endregion

		#region Public Methods and Operators

		public override void Calculate(
			Fraction julianDate,
			CalendarElementValueCollection values)
		{
			// Apply the offset so we can normalize the start point as "zero"
			// for this calendar.
			Fraction relativeDay = julianDate + JulianDateOffset;

			// If we are stripping off whole days, we need to remove them now.
			if (StripWholeDays)
			{
				// TODO: This loses some fidelity.
				relativeDay = new Fraction(relativeDay.ToDecimal() % 1.0m);
			}

			// We go through the length logics to figure out the points,
			// starting with a seed value of zero. The value coming out with
			// be the relative Julian Day from the beginning of the cycle.
			values[Id] = 0;

			foreach (CycleLength length in Lengths)
			{
				relativeDay = length.CalculateIndex(Id, values, relativeDay);
			}

			// Call the base implementation to handle inner cycles.
			base.Calculate(relativeDay, values);
		}

		#endregion
	}
}
