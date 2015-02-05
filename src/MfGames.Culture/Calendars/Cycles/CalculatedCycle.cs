// <copyright file="CalculatedCycle.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using Fractions;

using MfGames.Culture.Calendars.Calculations;

namespace MfGames.Culture.Calendars.Cycles
{
	/// <summary>
	/// A calendar cycle that's index is based on a single calculation.
	/// </summary>
	public class CalculatedCycle : Cycle
	{
		#region Constructors and Destructors

		public CalculatedCycle(string id)
			: base(id)
		{
		}

		public CalculatedCycle(string id, ICycleCalculation calculation)
			: this(id)
		{
			Calculation = calculation;
		}

		#endregion

		#region Public Properties

		public ICycleCalculation Calculation { get; set; }

		#endregion

		#region Public Methods and Operators

		public override void Calculate(
			Fraction julianDate,
			CalendarElementValueCollection values)
		{
			// Calculate the index.
			values[Id] = Calculation.GetIndex(values);

			// Call the base implementation to handle inner cycles.
			base.Calculate(julianDate, values);
		}

		#endregion
	}
}
