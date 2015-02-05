// <copyright file="ConstantLengthLogic.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Culture.Calendars.Lengths
{
	public class ConstantLengthLogic : ILengthLogic
	{
		#region Constructors and Destructors

		public ConstantLengthLogic(decimal julianDays)
		{
			JulianDays = julianDays;
		}

		#endregion

		#region Public Properties

		public bool IsConstant { get { return true; } }
		public decimal JulianDays { get; set; }

		#endregion

		#region Public Methods and Operators

		public bool CanHandle(CalendarElementValueCollection values)
		{
			return true;
		}

		public decimal GetLength(CalendarElementValueCollection values)
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
