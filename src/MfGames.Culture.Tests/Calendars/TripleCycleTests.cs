namespace MfGames.Culture.Tests.Calendars
{
    using System;

    using MfGames.Culture.Calendars;

    using NUnit.Framework;

    [TestFixture]
    public class TripleCycleTests
    {
        private CalendarSystem calendar;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Everything is based on the dCycle.
            var dCycle = new ClosedCycle("D", new JulianDateBasis());

            // Create the subcycle.
            var dwBasis = new CountedCycleBasis("DW", dCycle, 2);
            var wCycle = new ClosedCycle("W", dwBasis);
            var wkBasis = new CountedCycleBasis("WK", wCycle, 2);
            var kCycle = new ClosedCycle("K", wkBasis);
            var kmBasis = new CountedCycleBasis("KM", kCycle, 2);

            // Create the calendar with a single open-ended cycle.
            var dmBasis = new CountedCycleBasis("DM", dCycle, 8);
            var mCycle = new OpenCycle("M", dmBasis)
            {
                CalculatedCycles =
                    new CalendarElementCollection<CalendarElement>() { kmBasis }
            };

            // Create the calendar.
            this.calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement>
                    {
                        dCycle,
                        dmBasis,
                        mCycle,
                        dwBasis,
                        wCycle,
                        wkBasis,
                        kCycle,
                        kmBasis
                    }
            };
        }

        [Test]
        public void Test00()
        {
            var point = calendar.CreatePoint(0m);
            Verify(
                "DM 0 M 0 DW 0 WK 0 KM 0",
                point);
        }

        [Test]
        public void Test01()
        {
            var point = calendar.CreatePoint(1m);
            Verify(
                "DM 1 M 0 DW 1 WK 0 KM 0",
                point);
        }

        [Test]
        public void Test02()
        {
            var point = calendar.CreatePoint(2m);
            Verify(
                "DM 2 M 0 DW 0 WK 1 KM 0",
                point);
        }

        [Test]
        public void Test03()
        {
            var point = calendar.CreatePoint(3m);
            Verify(
                "DM 3 M 0 DW 1 WK 1 KM 0",
                point);
        }

        [Test]
        public void Test04()
        {
            var point = calendar.CreatePoint(4m);
            Verify(
                "DM 4 M 0 DW 0 WK 0 KM 1",
                point);
        }
        [Test]
        public void Test05()
        {
            var point = calendar.CreatePoint(5m);
            Verify(
                "DM 5 M 0 DW 1 WK 0 KM 1",
                point);
        }

        [Test]
        public void Test06()
        {
            var point = calendar.CreatePoint(6m);
            Verify(
                "DM 6 M 0 DW 0 WK 1 KM 1",
                point);
        }

        [Test]
        public void Test07()
        {
            var point = calendar.CreatePoint(7m);
            Verify(
                "DM 0 M 1 DW 1 WK 1 KM 1",
                point);
        }

        [Test]
        public void Test08()
        {
            var point = calendar.CreatePoint(8m);
            Verify("DM 1 M 1 DW 0 WK 0 KM 2", point);
        }

        [Test]
        public void Test09()
        {
            var point = calendar.CreatePoint(9m);
            Verify("DM 1 M 1 DW 0 WK 0 KM 2", point);
        }


        private void Verify(string expected, CalendarPoint point)
        {
            string format = string.Format(
                "DM {0} M {1} DW {2} WK {3} KM {4}",
                point.Get("DM"),
                point.Get("M"),
                point.Get("DW"),
                point.Get("WK"),
                point.Get("KM"));
            Console.WriteLine("Expected: " + expected);
            Console.WriteLine("Actual:   " + format);
            Assert.AreEqual(expected, format);
        }
    }
}