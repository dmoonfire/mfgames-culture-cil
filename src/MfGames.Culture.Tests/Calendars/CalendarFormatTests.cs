// <copyright file="CalendarFormatTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using Fractions;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Formats;
using MfGames.Culture.Codes;
using MfGames.Culture.Extensions.System;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class CalendarFormatTests
	{
		#region Fields

		private CalendarFormat alpha3;

		private GregorianCalendarSystem calendar;

		private LanguageTagSelector englishSelector;

		private CalendarFormat iso;

		#endregion

		#region Public Methods and Operators

		[Test]
		public void Alpha3Parse19870304()
		{
			CalendarPoint results = alpha3.Parse(
				calendar,
				englishSelector,
				"Mar 4, 1987");
			var date = new DateTime(1987, 3, 4);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, results.JulianDate);
		}

		[Test]
		public void Alpha3Parse19871123()
		{
			CalendarPoint results = alpha3.Parse(
				calendar,
				englishSelector,
				"Nov 23, 1987");
			var date = new DateTime(1987, 11, 23);
			Fraction julian = date.ToJulianDateFraction();

			Assert.AreEqual(julian, results.JulianDate);
		}

		[Test]
		public void Alpha3ToString19870304()
		{
			CalendarPoint point = calendar.Create(new DateTime(1987, 3, 4));
			string results = alpha3.ToString(point, calendar, englishSelector);

			Assert.AreEqual("Mar 4, 1987", results);
		}

		[Test]
		public void IsoParse19871123()
		{
			CalendarPoint point = calendar.Create(new DateTime(1987, 11, 23));
			CalendarPoint results = iso.Parse(
				calendar,
				englishSelector,
				"1987-11-23");

			Assert.AreEqual(point.JulianDate, results.JulianDate);
		}

		[Test]
		public void IsoParse20000101()
		{
			CalendarPoint results = iso.Parse(
				calendar,
				englishSelector,
				"2000-01-01");
			CalendarPoint year2000 = calendar.Create(new DateTime(2000, 1, 1));

			Assert.AreEqual(year2000.JulianDate, results.JulianDate);
		}

		[Test]
		public void IsoParseValues19871123()
		{
			CalendarElementValueCollection results = iso.ParseValues(
				calendar,
				englishSelector,
				"1987-11-23");

			Assert.AreEqual(1987, results["Year"]);
			Assert.AreEqual(10, results["Year Month"]);
			Assert.AreEqual(22, results["Month Day"]);
		}

		[Test]
		public void IsoToString19871123()
		{
			CalendarPoint point = calendar.Create(new DateTime(1987, 11, 23));
			string results = iso.ToString(point, calendar, englishSelector);

			Assert.AreEqual("1987-11-23", results);
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			calendar = new GregorianCalendarSystem();
			iso = new CalendarFormat("$(Year:D4)-$(Year Month:D2+1)-$(Month Day:D2+1");
			alpha3 = new CalendarFormat(
				"$(Year Month:S3/Short) $(Month Day:G0+1), $(Year:D4)");
			englishSelector = new LanguageTagSelector("eng;q=1.0, *;q=0.1");
		}

		#endregion
	}
}
