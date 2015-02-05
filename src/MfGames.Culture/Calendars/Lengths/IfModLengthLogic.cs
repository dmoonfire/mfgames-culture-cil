// <copyright file="IfModLengthLogic.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;

using Fractions;

namespace MfGames.Culture.Calendars.Lengths
{
	public class IfModLengthLogic : ILengthLogic
	{
		#region Constructors and Destructors

		public IfModLengthLogic(
			string dividendRef,
			int divisor,
			int julianDays)
			: this(dividendRef, divisor, new Fraction(julianDays))
		{
		}

		public IfModLengthLogic(
			string dividendRef,
			int divisor,
			decimal julianDays)
			: this(dividendRef, divisor, new Fraction(julianDays))
		{
		}

		public IfModLengthLogic(
			string dividendRef,
			int divisor,
			Fraction julianDays)
		{
			if (divisor == 0)
			{
				throw new ArgumentException(
					"Divisor cannot be zero.",
					"divisor");
			}

			DividendRef = dividendRef;
			Divisor = divisor;
			JulianDays = julianDays;
		}

		#endregion

		#region Public Properties

		public string DividendRef { get; set; }
		public int Divisor { get; set; }
		public bool IsConstant { get { return false; } }
		public Fraction JulianDays { get; set; }

		#endregion

		#region Public Methods and Operators

		public bool CanHandle(CalendarElementValueCollection values)
		{
			int dividend = values[DividendRef];
			int results = dividend % Divisor;
			return results == 0;
		}

		public Fraction GetLength(CalendarElementValueCollection values)
		{
			return JulianDays;
		}

		public override string ToString()
		{
			return string.Format(
				"IfModLengthLogic({0} % {1})",
				DividendRef,
				Divisor);
		}

		#endregion
	}
}
