// <copyright file="CalendarFormatCollectionTests.cs" company="Moonfire Games">
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
	public class CalendarFormatCollectionTests
	{
		#region Fields

		private GregorianCalendarSystem calendar;

		private LanguageTagSelector englishSelector;

		private CalendarFormatCollection formats;

		private Fraction julian19871123;

		#endregion

		#region Public Methods and Operators

		[Test]
		public void FormatIso()
		{
			CalendarPoint point = calendar.Create(new DateTime(1987, 11, 23));
			string results = formats.ToString(englishSelector, point);

			Assert.AreEqual("1987-11-23", results);
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			julian19871123 = new DateTime(1987, 11, 23).ToJulianDateFraction();
			calendar = new GregorianCalendarSystem();
			formats = new CalendarFormatCollection(calendar);
			formats.Add("yyyy-MM-dd", "$(Year:D4)-$(Year Month:D2+1)-$(Month Day:D2+1)");
			englishSelector = new LanguageTagSelector("eng;q=1.0, *;q=0.1");
		}

		#endregion
	}
}
