﻿// <copyright file="CalendarFormatCollectionTests.cs" company="Moonfire Games">
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
using MfGames.Culture.Translations;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class CalendarFormatCollectionTests
	{
		#region Fields

		private readonly GregorianCalendarSystem calendar;

		private readonly LanguageTagSelector englishSelector;

		private readonly CalendarFormatCollection formats;

		private readonly MemoryTranslationManager translations;

		#endregion

		#region Constructors and Destructors

		public CalendarFormatCollectionTests()
		{
			// Set up some common points.
			englishSelector = new LanguageTagSelector("eng;q=1.0, *;q=0.1");

			calendar = new GregorianCalendarSystem();
			translations = new MemoryTranslationManager();
			formats = new CalendarFormatCollection(calendar, translations);

			calendar.AddTranslations(translations);

			formats.Add("yyyy-MM-dd", "$(Year:D4)-$(Year Month:D2+1)-$(Month Day:D2+1)");
			formats.Add("MM/dd/yyyy", "$(Year Month:D2+1)/$(Month Day:D2+1)/$(Year:D4)");
			formats.Add("dd/MM/yyyy", "$(Month Day:D2+1)/$(Year Month:D2+1)/$(Year:D4)");
		}

		#endregion

		#region Public Methods and Operators

		[Test]
		public void FormatIsoYearMonthDay()
		{
			CalendarPoint point = calendar.Create(new DateTime(1987, 11, 23));
			string results = formats.Format("yyyy-MM-dd", point);

			Assert.AreEqual("1987-11-23", results);
		}

		[Test]
		public void FormatSlashedDayMonthYear()
		{
			CalendarPoint point = calendar.Create(new DateTime(1987, 11, 23));
			string results = formats.Format("dd/MM/yyyy", point);

			Assert.AreEqual("23/11/1987", results);
		}

		[Test]
		public void FormatSlashedMonthDayYear()
		{
			CalendarPoint point = calendar.Create(new DateTime(1987, 11, 23));
			string results = formats.Format("MM/dd/yyyy", point);

			Assert.AreEqual("11/23/1987", results);
		}

		[Test]
		public void IsConflictIso()
		{
			bool results = formats.IsConflict("1987-11-23");
			Assert.IsFalse(results);
		}

		[Test]
		public void IsConflictSlashed()
		{
			bool results = formats.IsConflict("11/23/1987");
			Assert.IsTrue(results);
		}

		[Test]
		public void ParseIsoYearMonthDay()
		{
			CalendarPoint point = formats.Parse(
				englishSelector,
				"yyyy-MM-dd",
				"1987-11-23");
			Fraction julian = new DateTime(1987, 11, 23).ToJulianDateFraction();

			Assert.AreEqual(julian, point.JulianDate);
		}

		[Test]
		public void ParseIsoYearMonthDayWithoutSpecifier()
		{
			CalendarPoint point = formats.Parse(
				englishSelector,
				"1987-11-23");
			Fraction julian = new DateTime(1987, 11, 23).ToJulianDateFraction();

			Assert.AreEqual(julian, point.JulianDate);
		}

		[Test]
		public void ParseSlashedDayMonthYear()
		{
			CalendarPoint point = formats.Parse(
				englishSelector,
				"dd/MM/yyyy",
				"23/11/1987");
			Fraction julian = new DateTime(1987, 11, 23).ToJulianDateFraction();

			Assert.AreEqual(julian, point.JulianDate);
		}

		[Test]
		public void ParseSlashedMonthDayYear()
		{
			CalendarPoint point = formats.Parse(
				englishSelector,
				"MM/dd/yyyy",
				"11/23/1987");
			Fraction julian = new DateTime(1987, 11, 23).ToJulianDateFraction();

			Assert.AreEqual(julian, point.JulianDate);
		}

		[Test]
		public void ParseSlashedMonthDayYearWithoutSpecifier()
		{
			Assert.Throws<InvalidOperationException>(
				() => formats.Parse(
					englishSelector,
					"11/23/1987"));
		}

		#endregion
	}
}
