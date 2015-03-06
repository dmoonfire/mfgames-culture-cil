// <copyright file="ModCycleCalculation.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Culture.Calendars.Calculations
{
	/// <summary>
	/// A cycle calculation that takes a given value and does a modulus
	/// against it.
	/// </summary>
	public class ModCycleCalculation : CycleCalculation
	{
		#region Constructors and Destructors

		public ModCycleCalculation(string elementRef, int constant)
			: base(elementRef, constant)
		{
		}

		#endregion

		#region Methods

		protected override int GetIndex(int elementValue)
		{
			return elementValue % Constant;
		}

		#endregion
	}
}
