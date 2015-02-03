// <copyright file="DaysOfMonthTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Tests.Calendars
{
    using System;

    using MfGames.Culture.Calendars;

    using NUnit.Framework;

    [TestFixture]
    public class DaysOfMonthTests
    {
        private CalendarSystem calendar;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Create the calendar with a single open-ended cycle.
            var dayCycle = new ClosedCycle("D", new JulianDateBasis());
            var dayOfMonthBasis = new CountedCycleBasis("DM", dayCycle, 30);
            var monthCycle = new OpenCycle("M", dayOfMonthBasis);

            calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement>
                    {
                        dayCycle,
                        dayOfMonthBasis,
                        monthCycle
                    }
            };
        }

        [Test]
        public void ZeroPoint()
        {
            // Create a point (date) on the calendar based on Julian Day Number.
            CalendarPoint date = calendar.CreatePoint(0.0m);

            Write(date);

            // Verify the resulting cycle.
            Assert.AreEqual(0.0m, date.JulianDate, "JDN is unexpected.");
            Assert.AreEqual(0, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(0, date.Get("DM"), "DM is unexpected (Get).");
        }

        [Test]
        public void LaterPoint()
        {
            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(65m);

            Write(date);

            // Verify the resulting cycle.
            Assert.AreEqual(65m, date.JulianDate, "JDN is unexpected.");
            Assert.AreEqual(2, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(5, date.Get("DM"), "DM is unexpected (Get).");
        }

        [Test]
        public void Day29()
        {
            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(29m);

            Write(date);

            // Verify the resulting cycle.
            Assert.AreEqual(0, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(29, date.Get("DM"), "DM is unexpected (Get).");
        }

        [Test]
        public void Day30()
        {
            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(30m);

            Write(date);

            // Verify the resulting cycle.
            Assert.AreEqual(1, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(0, date.Get("DM"), "DM is unexpected (Get).");
        }

        [Test]
        public void Day31()
        {
            // Create a point (date) on the calendar based on Julian Day Number.
            dynamic date = calendar.CreatePoint(31m);

            Write(date);

            // Verify the resulting cycle.
            Assert.AreEqual(1, date.Get("M"), "M is unexpected (Get).");
            Assert.AreEqual(1, date.Get("DM"), "DM is unexpected (Get).");
        }

        private void Write(CalendarPoint point)
        {
            Console.WriteLine(
                "{0}-{1}",
                point.Get("M"),
                point.Get("DM").ToString().PadLeft(2, '0'));
        }
    }
}
