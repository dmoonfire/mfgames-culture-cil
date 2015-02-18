// <copyright file="ConstantLengthLogic.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using Fractions;

namespace MfGames.Culture.Calendars.Lengths
{
	public class ConstantLengthLogic : ILengthLogic
	{
		#region Constructors and Destructors

		public ConstantLengthLogic(int julianDays)
			: this(new Fraction(julianDays))
		{
		}

		public ConstantLengthLogic(decimal julianDays)
			: this(new Fraction(julianDays))
		{
		}

		public ConstantLengthLogic(Fraction julianDays)
		{
			JulianDays = julianDays;
		}

		#endregion

		#region Public Properties

		public bool IsConstant { get { return true; } }
		public Fraction JulianDays { get; set; }

		#endregion

		#region Public Methods and Operators

		public bool CanHandle(CalendarElementValueCollection values)
		{
			return true;
		}

		public Fraction GetLength(CalendarElementValueCollection values)
		{
			return JulianDays;
		}

		public override string ToString()
		{
			return string.Format("ConstantLengthLogic({0})", JulianDays);
		}

		#endregion
	}
}
