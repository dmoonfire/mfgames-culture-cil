// <copyright file="DuodecimalCalendarSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using Fractions;

using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Calendars.Lengths;

namespace MfGames.Culture.Calendars
{
	public class DuodecimalCalendarSystem : CalendarSystem
	{
		#region Constructors and Destructors

		public DuodecimalCalendarSystem()
		{
			// With the 24-hour calendar, we start with a normalizer that gets
			// rid of everything to the left of the decimal place (so we only
			// have dates of 0.0m to 1.0m.
			var hour = new LengthCycle("Hour")
			{
				JulianDateOffset = new Fraction(0.5m),
				StripWholeDays = true
			};
			var hourLength = new LogicCycleLength(1, new Fraction(1, 24));

			hour.Lengths.Add(hourLength);

			// Add in the minutes.
			var minute = new LengthCycle("Minute");
			var minuteLength = new LogicCycleLength(1, new Fraction(1, 24 * 60));

			minute.Lengths.Add(minuteLength);
			hour.Cycles.Add(minute);

			// Add in the seconds.
			var second = new LengthCycle("Second");
			var secondLength = new LogicCycleLength(1, new Fraction(1, 24 * 60 * 60));

			second.Lengths.Add(secondLength);
			minute.Cycles.Add(second);

			// Create the calendar and add the open cycle which will add
			// everything else.
			Add(hour);
		}

		#endregion
	}
}
