// <copyright file="DuodecimalTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Globalization;

using MfGames.Culture.Calendars;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
	[TestFixture]
	public class DuodecimalTests
	{
		#region Fields

		private readonly CalendarSystem calendar;

		#endregion

		#region Constructors and Destructors

		public DuodecimalTests()
		{
			calendar = new DuodecimalCalendarSystem();
		}

		#endregion

		#region Public Methods and Operators

		[Test]
		public void VerifyHour1()
		{
			decimal julianDate = 0.5m + 1m / 24m + 0m / 1440m + 0m / 86400m;
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(1, point.Hour, "Hour");
			Assert.AreEqual(0, point.HourMinute, "HourMinute");
			Assert.AreEqual(0, point.MinuteSecond, "MinuteSecond");
			Assert.AreEqual(0, point.Meridiem, "Meridiem");
			Assert.AreEqual(1, point.MeridiemHour, "MeridiemHour");
		}

		[Test]
		public void VerifyHour15()
		{
			// We have to add "1m / 864000m" because of repeating decimals.
			decimal julianDate = 0.5m + 15m / 24m + 0m / 1440m + 0m / 86400m
				+ 1m / 864000m;
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(15, point.Hour, "Hour");
			Assert.AreEqual(0, point.HourMinute, "HourMinute");
			Assert.AreEqual(0, point.MinuteSecond, "MinuteSecond");
			Assert.AreEqual(1, point.Meridiem, "Meridiem");
			Assert.AreEqual(3, point.MeridiemHour, "MeridiemHour");
		}

		[Test]
		public void VerifyHour8()
		{
			// We have to add "1m / 864000m" because of repeating decimals.
			decimal julianDate = 0.5m + 8m / 24m + 0m / 1440m + 0m / 86400m
				+ 1m / 864000m;
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(8, point.Hour, "Hour");
			Assert.AreEqual(0, point.HourMinute, "HourMinute");
			Assert.AreEqual(0, point.MinuteSecond, "MinuteSecond");
			Assert.AreEqual(0, point.Meridiem, "Meridiem");
			Assert.AreEqual(8, point.MeridiemHour, "MeridiemHour");
		}

		[Test]
		public void VerifyHour8Minute13()
		{
			dynamic point = calendar.Create(new DateTime(2000, 1, 1, 8, 12, 0));

			Assert.AreEqual(8, point.Hour, "Hour");
			Assert.AreEqual(12, point.HourMinute, "HourMinute");
			Assert.AreEqual(0, point.MinuteSecond, "MinuteSecond");
			Assert.AreEqual(0, point.Meridiem, "Meridiem");
			Assert.AreEqual(8, point.MeridiemHour, "MeridiemHour");
		}

		[Test]
		public void VerifyNoon()
		{
			var julianDate = 0m;
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(12, point.Hour, "Hour");
			Assert.AreEqual(0, point.HourMinute, "HourMinute");
			Assert.AreEqual(0, point.MinuteSecond, "MinuteSecond");
			Assert.AreEqual(1, point.Meridiem, "Meridiem");
			Assert.AreEqual(0, point.MeridiemHour, "MeridiemHour");
		}

		[Test]
		public void VerifyNoonWithOffset()
		{
			decimal julianDate = 2348234m + 0m / 24m + 0m / 1440m + 0m / 86400m;
			dynamic point = calendar.Create(julianDate);

			Assert.AreEqual(12, point.Hour, "Hour");
			Assert.AreEqual(0, point.HourMinute, "HourMinute");
			Assert.AreEqual(0, point.MinuteSecond, "MinuteSecond");
			Assert.AreEqual(1, point.Meridiem, "Meridiem");
			Assert.AreEqual(0, point.MeridiemHour, "MeridiemHour");
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
