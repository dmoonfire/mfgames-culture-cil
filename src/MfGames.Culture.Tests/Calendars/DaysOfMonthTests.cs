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
        public void ZeroPoint()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new ClosedCycle("D", new JulianDateBasis());
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
            Assert.AreEqual(0.0m, date.JulianDate, "JDN is unexpected.");
            Assert.AreEqual(0, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(0, date.Get("DM"), "DM is unexpected (Get).");
        }

        [Test]
        public void LaterPoint()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new ClosedCycle("D", new JulianDateBasis());
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
            dynamic date = calendar.CreatePoint(65.123456789m);

            // Verify the resulting cycle.
            Assert.AreEqual(
                65.123456789m,
                date.JulianDate,
                "JDN is unexpected.");
            Assert.AreEqual(2, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(5, date.Get("DM"), "DM is unexpected (Get).");
        }
    }
}
