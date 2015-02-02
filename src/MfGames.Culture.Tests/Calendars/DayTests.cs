// <copyright file="DayTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Tests.Calendars
{
    using MfGames.Culture.Calendars;

    using NUnit.Framework;

    [TestFixture]
    public class DayTests
    {
        [Test]
        public void ZeroPoint()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new OpenCycle("D", new JulianDateBasis());
            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement> { dayCycle }
            };

            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(0.0m);

            // Verify the resulting cycle.
            Assert.AreEqual(0.0m, date.JulianDate, "JDN is unexpected.");
            Assert.AreEqual(0, date.Get("D"), "D is unexpected (Get).");
        }

        [Test]
        public void LaterPoint()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new OpenCycle("D", new JulianDateBasis());
            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement> { dayCycle }
            };

            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(10.0m);

            // Verify the resulting cycle.
            Assert.AreEqual(10.0m, date.JulianDate, "JDN is unexpected.");
            Assert.AreEqual(9, date.Get("D"), "D is unexpected (Get).");
        }

        [Test]
        public void NonstandardLaterPoint()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new OpenCycle("D", new JulianDateBasis(1.5m));
            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement> { dayCycle }
            };

            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(10.0m);

            // Verify the resulting cycle.
            Assert.AreEqual(10.0m, date.JulianDate, "JDN is unexpected.");
            Assert.AreEqual(6, date.Get("D"), "D is unexpected (Get).");
        }
    }
}
