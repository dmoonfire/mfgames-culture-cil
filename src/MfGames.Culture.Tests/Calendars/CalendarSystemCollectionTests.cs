// <copyright file="CalendarSystemCollectionTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using Fractions;

using MfGames.Culture.Calendars;
using MfGames.Culture.Extensions.System;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class CalendarSystemCollectionTests
	{
		#region Fields

		private CalendarSystemCollection calendars;

		#endregion

		#region Public Methods and Operators

		[TestFixtureSetUp]
		public void SetUp()
		{
			var gregorian = new GregorianCalendarSystem();
			var duodecimal = new DuodecimalCalendarSystem();

			calendars = new CalendarSystemCollection
			{
				gregorian,
				duodecimal
			};
		}

		[Test]
		public void SimplePoint()
		{
			// Create the date.
			var date = new DateTime(1987, 4, 5, 6, 2, 3);
			Fraction julianDate = date.ToJulianDateFraction();
			dynamic point = calendars.Create(julianDate);

			// Verify the resulting point.
			Assert.AreEqual(1987, point.Year, "Year");
			Assert.AreEqual(3, point.YearMonth, "Year Month");
			Assert.AreEqual(4, point.MonthDay, "Month Day");
			Assert.AreEqual(6, point.Hour, "Hour");
			Assert.AreEqual(2, point.HourMinute, "Hour Minute");
			Assert.AreEqual(3, point.MinuteSecond, "Minute Second");
		}

		#endregion
	}
}
