// <copyright file="SimpleCalendarTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Tests.Calendars
{
    using MfGames.Culture.Calendars;

    using NUnit.Framework;

    [TestFixture]
    public class SimpleCalendarTests
    {
        [Test]
        public void CreateSimpleThirtyDayMonth()
        {
            // Create the calendar.
            var calendar = CreateThirtyDayCalendar();
            var monthCycle = calendar.Cycles["Month"];
        }

        [Test]
        public void CreateAnonymousDateFromThirtyDayMonth()
        {
            // Create the calendar.
            var calendar = CreateThirtyDayCalendar();

            //// Create two dates from the calendar using an anonymous structure.
            //var initialDate = calendar.GetCalendarPoint(
            //    new
            //    {
            //        Month = 0,
            //        Day = 0
            //    });
            //var monthEightDayFourDate = calendar.GetCalendarPoint(
            //    new
            //    {
            //        Month = 8,
            //        Day = 4
            //    });

            //// Verify the results.
            //Assert.AreEqual(
            //    100.0,
            //    initialDate.JulianDate,
            //    "Initial date is unexpected.");
            //Assert.AreEqual(
            //    344.0,
            //    monthEightDayFourDate.JulianDate,
            //    "8-4 date is unexpected.");
        }

        [Test]
        public void CreateJulianDateFromThirtyDayMonthInitialDate()
        {
            // Create the calendar.
            var calendar = CreateThirtyDayCalendar();

            //// Create two dates from the calendar using an anonymous structure.
            //var initialDate = calendar.GetCalendarPoint(100.0m);

            //// Verify the results.
            //Assert.AreEqual(
            //    100.0,
            //    initialDate.JulianDate,
            //    "Initial date is unexpected.");
            //Assert.AreEqual(
            //    344.0,
            //    monthEightDayFourDate.JulianDate,
            //    "8-4 date is unexpected.");
        }

        [Test]
        public void CreateJulianDateFromThirtyDayMonthMonthEightDayFourDaet()
        {
            // Create the calendar.
            var calendar = CreateThirtyDayCalendar();

            //// Create two dates from the calendar using an anonymous structure.
            //var monthEightDayFourDate = calendar.GetCalendarPoint(344.0m);

            //// Verify the results.
            //Assert.AreEqual(
            //    100.0,
            //    initialDate.JulianDate,
            //    "Initial date is unexpected.");
            //Assert.AreEqual(
            //    344.0,
            //    monthEightDayFourDate.JulianDate,
            //    "8-4 date is unexpected.");
        }

        private static CalendarSystem CreateThirtyDayCalendar()
        {
            //// Define the various cycles and basis of the calendar.
            //var dayCycle = new Cycle("Day", Basis.JulianDay);
            //var dayOfMonthBasis = new Basis("Day of Month", dayCycle, 30);
            //var monthCycle = new Cycle("Month", dayOfMonthBasis);

            //// Create the calendar pulling everything together.
            //var calendar = new CalendarSystem
            //{
            //    Cycles = new CalendarElementCollection
            //    {
            //        dayCycle,
            //        monthCycle
            //    }
            //};

            //// The calendar has to be anchored against a Julian Day. This is done
            //// against the outermost cycle of the calendar.
            //calendar.OuterCycle.Anchors[0, 100.0m];

            //// Return the resulting calendar.
            //return calendar;
            return null;
        }
    }
}
