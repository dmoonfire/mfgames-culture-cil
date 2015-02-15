// <copyright file="CalendarFormatCollectionTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Formats;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class CalendarFormatCollectionTests
	{
		#region Fields

		private GregorianCalendarSystem calendar;

		private CalendarFormatCollection formats;

		#endregion

		#region Public Methods and Operators

		[Test]
		public void FormatIso()
		{
			Assert.Pass("Placeholder.");
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			calendar = new GregorianCalendarSystem();
			formats = new CalendarFormatCollection(calendar);
			formats.Add("yyyy-MM-dd", "$(Year:D4)-$(Year Month:D2+1)-$(Month Day:D2+1)");
		}

		#endregion
	}
}
