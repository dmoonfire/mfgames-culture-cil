// <copyright file="DaysOfMonthTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Tests.Calendars
{
    using MfGames.Culture.Calendars;

    using NUnit.Framework;

    [TestFixture]
    public class DaysOfMonthTests
    {
        [Test]
        public void CreateSimpleThirtyDayMonth()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new ClosedCycle("D", new JulianDayNumberBasis());
            var dayOfMonthBasis = new CountedCycleBasis("DM", dayCycle, 30);
            var monthCycle = new OpenCycle("M", dayOfMonthBasis);

            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement>
                    {
                        dayCycle,
                        dayOfMonthBasis,
                        monthCycle
                    }
            };

            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(0.0m);

            // Verify the resulting cycle.
            Assert.AreEqual(0.0m, date.JulianDayNumber, "JDN is unexpected.");
            Assert.AreEqual(0, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(0, date.Get("DM"), "DM is unexpected (Get).");
        }

        [Test]
        public void CreateAnonymousDateFromThirtyDayMonth()
        {
            // Create the calendar.
            CalendarSystem calendar = CreateThirtyDayCalendar();

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
            CalendarSystem calendar = CreateThirtyDayCalendar();

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
            CalendarSystem calendar = CreateThirtyDayCalendar();

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
