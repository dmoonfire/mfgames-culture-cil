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
		private GregorianCalendarSystem calendar;

		private CalendarFormat iso;

		private CalendarPoint point;

		[TestFixtureSetUp]
		public void SetUp()
		{
			calendar = new GregorianCalendarSystem();
			point = calendar.Create(new DateTime(1987, 11, 23));
			iso = new CalendarFormat("$(Year:D4)-$(Year Month:D2+1)-$(Month Day:D2+1");
		}

		[Test]
		public void IsoToString()
		{
			var results = iso.ToString(point);

			Assert.AreEqual("1987-11-23", results);
		}
	}
}