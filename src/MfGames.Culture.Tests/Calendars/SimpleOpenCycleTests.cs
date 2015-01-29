// <copyright file="SimpleOpenCycleTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Tests.Calendars
{
    using MfGames.Culture.Calendars;

    using NUnit.Framework;

    [TestFixture]
    public class SimpleOpenCycleTests
    {
        [Test]
        public void CreateCalendarSystem()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new OpenCycle("Test 1", new JulianDayNumberBasis());
            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement> { dayCycle }
            };

            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(0.0m);

            // Verify the resulting cycle.
            Assert.AreEqual(0.0m, date.JulianDayNumber, "JDN is unexpected.");
            Assert.AreEqual(
                0,
                date.Get("Test 1"),
                "Test 1 is unexpected (Get).");
        }

        [Test]
        public void CreateCalendarSystemDaysIn()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new OpenCycle("Test 1", new JulianDayNumberBasis());
            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement> { dayCycle }
            };

            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(10.0m);

            // Verify the resulting cycle.
            Assert.AreEqual(10.0m, date.JulianDayNumber, "JDN is unexpected.");
            Assert.AreEqual(
                10,
                date.Get("Test 1"),
                "Test 1 is unexpected (Get).");
        }

        [Test]
        public void CreateCalendarSystemNonstandardDaysIn()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new OpenCycle(
                "Test 1",
                new JulianDayNumberBasis(1.5m));
            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement> { dayCycle }
            };

            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(10.0m);

            // Verify the resulting cycle.
            Assert.AreEqual(10.0m, date.JulianDayNumber, "JDN is unexpected.");
            Assert.AreEqual(
                6,
                date.Get("Test 1"),
                "Test 1 is unexpected (Get).");
        }
    }
}
