// <copyright file="GregorianTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Globalization;

using Fractions;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Calendars.Lengths;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class GregorianTests
	{
		#region Fields

		private CalendarSystem calendar;

		private CycleLength yearLength1;

		#endregion

		#region Public Methods and Operators

		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			calendar = new GregorianCalendarSystem();
			yearLength1 = ((LengthCycle) calendar.Cycles["Year"]).Lengths[1];
		}

		[Test]
		public void Verify15830101()
		{
			decimal julianDate = ToJulianDate(1583, 1, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(1583, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(1, point.Millennium, "Millennium");
			Assert.AreEqual(15, point.Century, "Century");
			Assert.AreEqual(158, point.Decade, "Decade");
			Assert.AreEqual(5, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(8, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(3, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify15830101PlusHour()
		{
			decimal julianDate = ToJulianDate(1583, 1, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(1583, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(1, point.Millennium, "Millennium");
			Assert.AreEqual(15, point.Century, "Century");
			Assert.AreEqual(158, point.Decade, "Decade");
			Assert.AreEqual(5, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(8, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(3, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20000101()
		{
			decimal julianDate = ToJulianDate(2000, 1, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2000, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(0, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20000101PlusHour()
		{
			decimal julianDate = ToJulianDate(2000, 1, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2000, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(0, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20000102()
		{
			decimal julianDate = ToJulianDate(2000, 1, 2);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2000, point.Year, "Year");
			Assert.AreEqual(1, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(0, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20000102PlusHour()
		{
			decimal julianDate = ToJulianDate(2000, 1, 2, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2000, point.Year, "Year");
			Assert.AreEqual(1, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(0, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20000201()
		{
			decimal julianDate = ToJulianDate(2000, 2, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2000, point.Year, "Year");
			Assert.AreEqual(31, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(0, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(1, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20000201PlusHour()
		{
			decimal julianDate = ToJulianDate(2000, 2, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2000, point.Year, "Year");
			Assert.AreEqual(31, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(0, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(1, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010101()
		{
			decimal julianDate = ToJulianDate(2001, 1, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010101PlusHour()
		{
			decimal julianDate = ToJulianDate(2001, 1, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010102()
		{
			decimal julianDate = ToJulianDate(2001, 1, 2);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(1, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010102PlusHour()
		{
			decimal julianDate = ToJulianDate(2001, 1, 2, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(1, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010201()
		{
			decimal julianDate = ToJulianDate(2001, 2, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(31, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(1, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010201PlusHour()
		{
			decimal julianDate = ToJulianDate(2001, 2, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(31, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(1, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010202()
		{
			decimal julianDate = ToJulianDate(2001, 2, 2);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(32, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(1, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010202PlusHour()
		{
			decimal julianDate = ToJulianDate(2001, 2, 2, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(32, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(1, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010302()
		{
			decimal julianDate = ToJulianDate(2001, 3, 2);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(60, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(2, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20010302PlusHour()
		{
			decimal julianDate = ToJulianDate(2001, 3, 2, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2001, point.Year, "Year");
			Assert.AreEqual(60, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(1, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(2, point.YearMonth, "YearMonth");
			Assert.AreEqual(1, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20020101()
		{
			decimal julianDate = ToJulianDate(2002, 1, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2002, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(2, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20020101PlusHour()
		{
			decimal julianDate = ToJulianDate(2002, 1, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2002, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(2, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20030101()
		{
			decimal julianDate = ToJulianDate(2003, 1, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2003, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(3, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20030101PlusHour()
		{
			decimal julianDate = ToJulianDate(2003, 1, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2003, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(3, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20040101()
		{
			decimal julianDate = ToJulianDate(2004, 1, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2004, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(4, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20040101PlusHour()
		{
			decimal julianDate = ToJulianDate(2004, 1, 1, 1, 0, 0);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2004, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(4, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		[Test]
		public void VerifyYearLength0()
		{
			// Calculate the index of the year.
			var values = new CalendarElementValueCollection();

			values["Year"] = 0;
			yearLength1.CalculateIndex("Year", values, new Fraction(0m));

			// Verify the results.
			Assert.AreEqual(0, values["Year"]);
		}

		[Test]
		public void VerifyYearLength365()
		{
			// Calculate the index of the year.
			var values = new CalendarElementValueCollection();

			values["Year"] = 0;
			yearLength1.CalculateIndex("Year", values, new Fraction(365m));

			// Verify the results.
			Assert.AreEqual(0, values["Year"]);
		}

		[Test]
		public void VerifyYearLength366()
		{
			// Calculate the index of the year.
			var values = new CalendarElementValueCollection();

			values["Year"] = 0;
			yearLength1.CalculateIndex("Year", values, new Fraction(366m));

			// Verify the results.
			Assert.AreEqual(1, values["Year"]);
		}

		[Test]
		public void VerifyYearLength367()
		{
			// Calculate the index of the year.
			var values = new CalendarElementValueCollection();

			values["Year"] = 0;
			yearLength1.CalculateIndex("Year", values, new Fraction(367m));

			// Verify the results.
			Assert.AreEqual(1, values["Year"]);
		}

		#endregion

		#region Methods

		private decimal ToJulianDate(DateTime dateTime)
		{
			// http://stackoverflow.com/questions/5248827/convert-datetime-to-julian-date-in-c-sharp-tooadate-safe
			decimal results = (decimal)dateTime.ToOADate() + 2415018.5m;

			var gregorian = new GregorianCalendar();
			Console.WriteLine(
				"Julian Date: {0} = {1} (Leap {2}, {3})",
				dateTime.ToString("yyyy-MM-dd"),
				results,
				gregorian.IsLeapYear(dateTime.Year),
				(decimal)
					new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToOADate
						() + 2415018.5m - results);
			return results;
		}

		private decimal ToJulianDate(
			int year,
			int month,
			int day)
		{
			return ToJulianDate(year, month, day, 0, 0, 0);
		}

		private decimal ToJulianDate(
			int year,
			int month,
			int day,
			int hour,
			int minute,
			int second)
		{
			// Calculate the Julian Date for the date.
			var date = new DateTime(
				year,
				month,
				day,
				0,
				0,
				0,
				DateTimeKind.Utc);
			decimal julianDate = ToJulianDate(date);

			// Add in the time. We do this instead of passing it into the date
			// because there is a weird issue with calculating hours for the
			// 1583 date.
			julianDate += hour * (1m / 24m);
			julianDate += minute * (1m / (24m * 60m));
			julianDate += second * (1m / (24m * 60m * 60m));

			// Return the resulting time.
			return julianDate;
		}

		#endregion
	}
}
