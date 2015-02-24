// <copyright file="UnitedStatesCultureTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using Fractions;

using MfGames.Culture.Calendars;
using MfGames.Culture.Codes;
using MfGames.Culture.Extensions.System;

using NUnit.Framework;

namespace MfGames.Culture.Tests
{
	/// <summary>
	/// Defines tests around creating a US culture.
	/// </summary>
	[TestFixture]
	public class UnitedStatesCultureTests
	{
		#region Fields

		private CultureSystem culture;

		#endregion

		#region Public Methods and Operators

		[Test]
		public void FormatSlashedDayMonthYear()
		{
			CalendarPoint point = culture.Calendar.Create(new DateTime(1987, 11, 23));
			string results = culture.FormatCalendarPoint("dd/MM/yyyy", point);

			Assert.AreEqual("23/11/1987", results);
		}

		[Test]
		public void ParseIsoYearMonthDay()
		{
			CalendarPoint point = culture.ParseCalendarPoint(
				"yyyy-MM-dd",
				"1987-11-23");
			Fraction julian = new DateTime(1987, 11, 23).ToJulianDateFraction();

			Assert.AreEqual(julian, point.JulianDate);
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			// Create the culture we are populating.
			culture = new CultureSystem
			{
				Selector = new LanguageTagSelector("en-US;q=1.0, en;q=0.8, *;q=0.1")
			};

			// Grab our two base calendars and add them the culture.
			var gregorian = new GregorianCalendarSystem();
			var duodecimal = new DuodecimalCalendarSystem();

			culture.Calendar.Add(gregorian);
			culture.Calendar.Add(duodecimal);

			// Add in the known formats.
			culture.Formats.Add(
				"yyyy-MM-dd",
				"$(Year:D4)-$(Year Month:D2+1)-$(Month Day:D2+1");
			culture.Formats.Add(
				"dd/MM/yyyy",
				"$(Month Day:D2+1)/$(Year Month:D2+1)/$(Year:D4)");
		}

		#endregion
	}
}
