// <copyright file="LengthCycle.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
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

		public override Fraction CalculateJulianDate(
			CalendarElementValueCollection desiredValues,
			CalendarElementValueCollection currentValues)
		{
			// Copy the Julian Date so we can add to it. We subtract the
			// offset that we will add back when calculating the proper date.
			Fraction julianDate = JulianDateOffset * -1;

			// Loop through the lengths until we match our index.
			foreach (CycleLength length in Lengths)
			{
				// This will return zero if the length cannot handle the
				// difference between the julian date and the calculated
				// value.
				Fraction julianLength = length.CalculateLength(
					Id,
					desiredValues,
					currentValues);

				julianDate += julianLength;
			}

			// Call the base version to get the rest of the related dates.
			Fraction childLength = base.CalculateJulianDate(desiredValues, currentValues);
			Fraction results = childLength + julianDate;

			return results;
		}

		public override void CalculateValues(
			Fraction julianDate,
			CalendarElementValueCollection values)
		{
			// Apply the offset so we can normalize the start point as "zero"
			// for this calendar.
			Fraction relativeDay = julianDate + JulianDateOffset;

			// If we are stripping off whole days, we need to remove them now.
			if (StripWholeDays)
			{
				// Remove the whole number part from the fraction.
				relativeDay = new Fraction(
					relativeDay.Numerator % relativeDay.Denominator,
					relativeDay.Denominator);
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
			base.CalculateValues(relativeDay, values);
		}

		#endregion
	}
}
