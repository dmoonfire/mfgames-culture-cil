// <copyright file="GregorianTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Tests.Calendars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MfGames.Culture.Calendars;

    using NUnit.Framework;

    [TestFixture]
    public class GregorianTests
    {
        private CalendarSystem calendar;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            // Everything is based on the day.
            var day = new ClosedCycle("Day", new JulianDateBasis());

            // Add in the simple calculations.
            var centuryOfMillennium = new CalculatedCycle("Century of Millennium", "$(Century) mod 10");
            var millennium = new CalculatedCycle("Millennium", "$(Century) div 10");
            var century = new CalculatedCycle(
                "Century",
                "$(Decade) div 10")
            {
                CalculatedCycles =
                    new CalendarElementCollection<CalendarElement>
                    {
                        centuryOfMillennium,
                        millennium
                    }
            };
            var decadeOfCentury = new CalculatedCycle("Decade of Century", "$(Decade) mod 10");
            var decade = new CalculatedCycle("Decade", "$(Year) div 10")
            {
                CalculatedCycles =
                    new CalendarElementCollection<CalendarElement>
                    {
                        decadeOfCentury,
                        century
                    }
            };
            var yearOfDecade = new CalculatedCycle("Year of Decade", "$(Year) mod 10");

            // Handle the sub-year elements.
            var dayOfMonth = new CountedCycleBasis("Day of Month", day)
            {
                BasisLengthLogics =
                    new BasisLengthLogicCollection
                    {
                        new IfBasisLengthLogic(
                            "$(Month of Year) in $(Feb)",
                            new IfBasisLengthLogic("$(Year) mod 400", 29),
                            new IfBasisLengthLogic("$(Year) mod 100", 28),
                            new IfBasisLengthLogic("$(Year) mod 4", 29),
                            new CountBasisLengthLogic(28)),
                        new IfBasisLengthLogic(
                            "$(Month of Year) in $(Month31)",
                            31),
                        new IfBasisLengthLogic(
                            "$(Month of Year) in $(Month30)",
                            30)
                    }
            };
            var month = new ClosedCycle("Month", dayOfMonth);
            var monthOfYear = new CountedCycleBasis("Month of Year", month, 12);

            // Create the basis for the year which should be as fast as
            // a calculation as possible.
            var dayOfYear = new CountedCycleBasis("Day of Year", day)
            {
                BasisLengthLogics =
                    new BasisLengthLogicCollection
                    {
                        new IfBasisLengthLogic("$(Year) mod 400", 366),
                        new IfBasisLengthLogic("$(Year) mod 100", 365),
                        new IfBasisLengthLogic("$(Year) mod 4", 366),
                        new CountBasisLengthLogic(365)
                    }
            };
            var year = new OpenCycle("Year", dayOfYear)
            {
                JulianDateOffset = JulianDateOffset,
                CalculatedCycles =
                    new CalendarElementCollection<CalendarElement>
                    {
                        decade,
                        yearOfDecade,
                        monthOfYear
                    }
            };

            calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement>
                    {
                        day,
                        dayOfMonth,
                        month,
                        monthOfYear,
                        dayOfYear,
                        year,
                        yearOfDecade,
                        decade,
                        decadeOfCentury,
                        century,
                        centuryOfMillennium,
                        millennium
                    },
                Variables =
                    new Dictionary<string, object>
                    {
                        { "Jan", "0" },
                        { "Feb", "1" },
                        { "Mar", "2" },
                        { "Apr", "3" },
                        { "May", "4" },
                        { "Jun", "5" },
                        { "Jul", "6" },
                        { "Aug", "7" },
                        { "Sep", "8" },
                        { "Oct", "9" },
                        { "Nov", "10" },
                        { "Dec", "11" },
                        {
                            "Month31",
                            "$(Jan),$(Mar),$(May),$(Jul),$(Aug),$(Oct),$(Dec)"
                        },
                        { "Month30", "$(Apr),$(Jun),$(Sep),$(Nov)" }
                    }
            };
        }

        [Test]
        public void IsLeapYearIn2000()
        {
            var dayOfMonth = (Basis)calendar.Elements["Day of Month"];
            decimal count =
                dayOfMonth.GetLength(
                    new CalendarElementValueDictionary()
                    {
                        { "Year", 2000 },
                        { "Month of Year", 1 },
                    });
            Assert.AreEqual(29m, count, "The number of days in the month is unexpected.");
        }
        [Test]
        public void IsLeapYearIn1900()
        {
            var dayOfMonth = (Basis)calendar.Elements["Day of Month"];
            decimal count =
                dayOfMonth.GetLength(
                    new CalendarElementValueDictionary()
                    {
                        { "Year", 1900 },
                        { "Month of Year", 1 },
                    });
            Assert.AreEqual(28m, count, "The number of days in the month is unexpected.");
        }

        [Test]
        public void IsLeapYearIn1901()
        {
            var dayOfMonth = (Basis)calendar.Elements["Day of Month"];
            decimal count =
                dayOfMonth.GetLength(
                    new CalendarElementValueDictionary()
                    {
                        { "Year", 1901 },
                        { "Month of Year", 1 },
                    });
            Assert.AreEqual(28m, count, "The number of days in the month is unexpected.");
        }

        [Test]
        public void TestCalculations()
        {
            var start = 2299238.500000m;
            var count = 10;
            for (decimal i = start; i < start + count; i += 1m)
            {
                CalendarPoint point = calendar.CreatePoint(i);
                WritePoint(calendar, point);
            }
        }
        [Test]
        public void Point20150131()
        {
            // Create the calendar and get the date.
            CalendarPoint point = calendar.CreatePoint(2457053.500000m);

            // Report the state of the point.
            WritePoint(calendar, point);

            // Assert the state of the point. 
            Assert.AreEqual(
                2,
                point.Get("Millennium"),
                "Millennium was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Century of Millennium"),
                "Century Millennium was unexpected.");
            Assert.AreEqual(
                1,
                point.Get("Decade of Century"),
                "Decade of Century was unexpected.");
            Assert.AreEqual(
                5,
                point.Get("Year of Decade"),
                "Year of Decade was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Month of Year"),
                "Month of Year was unexpected.");
            Assert.AreEqual(
                30,
                point.Get("Day of Month"),
                "Day of Month was unexpected.");
        }

        private const decimal JulianDateOffset = 1721090.5m - 31m;

        private static void WritePoint(CalendarSystem calendar, CalendarPoint point)
        {
            const int OneBasedOffset = 1;

            string dateString =
                string.Format(
                    "{0}-{1}-{2}",
                    point.Get("Year").ToString().PadLeft(4, '0'),
                    (point.Get("Month of Year") + OneBasedOffset).ToString()
                        .PadLeft(2, '0'),
                    (point.Get("Day of Month") + OneBasedOffset).ToString()
                        .PadLeft(2, '0'));
            DateTime date = DateTime.Parse(dateString);
            decimal julian = ToJulianDate(date);

            Console.WriteLine("{0} ({1}-{2})) {3}",
                dateString,
                point.Get("Year").ToString().PadLeft(4, '0'),
                point.Get("Day of Year").ToString().PadLeft(3, '0'),
                julian);


        }

        public static decimal ToJulianDate(DateTime date)
        {
            return (decimal)date.ToOADate() + 2415018.5m;
        }


        [Test]
        public void Point18530101()
        {
            // Create the calendar.
            CalendarPoint point = calendar.CreatePoint(2299238.500000m);

            // Report the state of the point.
            WritePoint(calendar, point);

            // Assert the state of the point. 
            Assert.AreEqual(
                1,
                point.Get("Millennium"),
                "Millennium was unexpected.");
            Assert.AreEqual(
                5,
                point.Get("Century of Millennium"),
                "Century Millennium was unexpected.");
            Assert.AreEqual(
                8,
                point.Get("Decade of Century"),
                "Decade of Century was unexpected.");
            Assert.AreEqual(
                3,
                point.Get("Year of Decade"),
                "Year of Decade was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Month of Year"),
                "Month of Year was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Day of Month"),
                "Day of Month was unexpected.");
        }

        [Test]
        public void Point18530201()
        {
            // Create the calendar.
            CalendarPoint point = calendar.CreatePoint(2299238.500000m + 31);

            // Report the state of the point.
            WritePoint(calendar, point);

            // Assert the state of the point. 
            Assert.AreEqual(
                1,
                point.Get("Millennium"),
                "Millennium was unexpected.");
            Assert.AreEqual(
                5,
                point.Get("Century of Millennium"),
                "Century Millennium was unexpected.");
            Assert.AreEqual(
                8,
                point.Get("Decade of Century"),
                "Decade of Century was unexpected.");
            Assert.AreEqual(
                3,
                point.Get("Year of Decade"),
                "Year of Decade was unexpected.");
            Assert.AreEqual(
                1,
                point.Get("Month of Year"),
                "Month of Year was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Day of Month"),
                "Day of Month was unexpected.");
        }

        [Test]
        public void Point20000101()
        {
            // Create the calendar.
            CalendarPoint point = calendar.CreatePoint(2451544.500000m);

            // Report the state of the point.
            WritePoint(calendar, point);

            // Assert the state of the point. 
            Assert.AreEqual(
                2,
                point.Get("Millennium"),
                "Millennium was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Century of Millennium"),
                "Century Millennium was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Decade of Century"),
                "Decade of Century was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Year of Decade"),
                "Year of Decade was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Month of Year"),
                "Month of Year was unexpected.");
            Assert.AreEqual(
                0,
                point.Get("Day of Month"),
                "Day of Month was unexpected.");
        }
    }
}
