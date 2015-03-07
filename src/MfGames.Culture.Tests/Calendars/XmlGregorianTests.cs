// <copyright file="XmlGregorianTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Globalization;
using System.IO;

using Fractions;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Lengths;
using MfGames.Culture.Extensions.System;
using MfGames.Culture.IO;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class XmlGregorianTests
	{
		#region Fields

		private ICalendarSystem calendar;

		private CycleLength yearLength1;

		#endregion

		#region Public Methods and Operators

		[Test]
		public void CalculateJulianDate19870101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 1987;
			values["Year Month"] = 0;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(1987, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate19870123()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 1987;
			values["Year Month"] = 0;
			values["Month Day"] = 22;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(1987, 1, 23);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate19871101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 1987;
			values["Year Month"] = 10;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(1987, 11, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate19871123()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 1987;
			values["Year Month"] = 10;
			values["Month Day"] = 22;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(1987, 11, 23);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate2000()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2000;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2000, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate200001()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2000;
			values["Year Month"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2000, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20000101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2000;
			values["Year Month"] = 0;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2000, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20010101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2001;
			values["Year Month"] = 0;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2001, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20010102()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2001;
			values["Year Month"] = 0;
			values["Month Day"] = 1;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2001, 1, 2);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20010202()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2001;
			values["Year Month"] = 1;
			values["Month Day"] = 1;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2001, 2, 2);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20020101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2002;
			values["Year Month"] = 0;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2002, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20030101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2003;
			values["Year Month"] = 0;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2003, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20040101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2004;
			values["Year Month"] = 0;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2004, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void CalculateJulianDate20050101()
		{
			// Common setup.
			Setup();

			// Run the test.
			// Create the values we're matching.
			var values = new CalendarElementValueCollection();

			values["Year"] = 2005;
			values["Year Month"] = 0;
			values["Month Day"] = 0;

			// Create the point.
			Fraction julianDate = calendar.GetJulianDate(values);

			// Compare the dates.
			var date = new DateTime(2005, 1, 1);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, julianDate);
		}

		[Test]
		public void Verify15830101()
		{
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
		public void Verify19871123()
		{
			// Common setup.
			Setup();

			// Run the test.
			decimal julianDate = ToJulianDate(1987, 11, 23);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(1987, point.Year, "Year");
			Assert.AreEqual(326, point.YearDay, "YearDay");
			Assert.AreEqual(1, point.Millennium, "Millennium");
			Assert.AreEqual(19, point.Century, "Century");
			Assert.AreEqual(198, point.Decade, "Decade");
			Assert.AreEqual(9, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(8, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(7, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(10, point.YearMonth, "YearMonth");
			Assert.AreEqual(22, point.MonthDay, "MonthDay");
		}

		[Test]
		public void Verify20000101()
		{
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
			// Common setup.
			Setup();

			// Run the test.
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
		public void Verify20050101()
		{
			// Common setup.
			Setup();

			// Run the test.
			decimal julianDate = ToJulianDate(2005, 1, 1);
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(2005, point.Year, "Year");
			Assert.AreEqual(0, point.YearDay, "YearDay");
			Assert.AreEqual(2, point.Millennium, "Millennium");
			Assert.AreEqual(20, point.Century, "Century");
			Assert.AreEqual(200, point.Decade, "Decade");
			Assert.AreEqual(0, point.MillenniumCentury, "MillenniumCentury");
			Assert.AreEqual(0, point.CenturyDecade, "CenturyDecade");
			Assert.AreEqual(5, point.DecadeYear, "DecadeYear");
			Assert.AreEqual(0, point.YearMonth, "YearMonth");
			Assert.AreEqual(0, point.MonthDay, "MonthDay");
		}

		#endregion

		#region Methods

		private void Setup()
		{
			// Read the calendar into memory.
			const string Path = "..\\..\\data\\calendars\\gregorian.xml";
			var reader = new CalendarSystemXmlReader();

			using (FileStream stream = File.OpenRead(Path))
				calendar = reader.Read(stream);
		}

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
