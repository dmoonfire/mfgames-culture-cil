// <copyright file="CalendarFormatTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Formats;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class CalendarFormatTests
	{
		#region Fields

		private GregorianCalendarSystem calendar;

		private CalendarFormat iso;

		private CalendarPoint point;

		#endregion

		#region Public Methods and Operators

		[Test]
		public void IsoParse19871123()
		{
			CalendarPoint results = iso.Parse("1987-11-23", calendar);

			Assert.AreEqual(point.JulianDate, results.JulianDate);
		}

		[Test]
		public void IsoParse20000101()
		{
			CalendarPoint results = iso.Parse("2000-01-01", calendar);
			CalendarPoint year2000 = calendar.Create(new DateTime(2000, 1, 1));

			Assert.AreEqual(year2000.JulianDate, results.JulianDate);
		}

		[Test]
		public void IsoParseValues19871123()
		{
			CalendarElementValueCollection results = iso.ParseValues("1987-11-23");

			Assert.AreEqual(1987, results["Year"]);
			Assert.AreEqual(10, results["Year Month"]);
			Assert.AreEqual(22, results["Month Day"]);
		}

		[Test]
		public void IsoToString19871123()
		{
			string results = iso.ToString(point);

			Assert.AreEqual("1987-11-23", results);
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			calendar = new GregorianCalendarSystem();
			point = calendar.Create(new DateTime(1987, 11, 23));
			iso = new CalendarFormat("$(Year:D4)-$(Year Month:D2+1)-$(Month Day:D2+1");
		}

		#endregion
	}
}
