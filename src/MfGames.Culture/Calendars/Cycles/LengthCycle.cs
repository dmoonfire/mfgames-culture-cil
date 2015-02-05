// <copyright file="LengthCycle.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

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

		#endregion

		#region Public Methods and Operators

		public override void Calculate(
			decimal julianDate,
			CalendarElementValueCollection values)
		{
			// We go through the length logics to figure out the points,
			// starting with a seed value of zero. The value coming out with
			// be the relative Julian Day from the beginning of the cycle.
			values[Id] = 0;

			decimal relativeDay = julianDate + JulianDateOffset;

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
