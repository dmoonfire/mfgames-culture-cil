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
        [Test]
        public void ZeroPoint()
        {
            CalendarSystem calendar = CreateCalendar();
            CalendarPoint point = calendar.CreatePoint(0.0m);

            Assert.AreEqual(null, point);
        }

        //private const decimal JulianDayNumberOffset = 1719899.5m;
        private const decimal JulianDayNumberOffset = 1721425.5m;

        [Test]
        public void Point20150131()
        {
            // Create the calendar and get the date.
            CalendarSystem calendar = CreateCalendar();
            CalendarPoint point = calendar.CreatePoint(2457053.5m);

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
                4,
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

        private static void WritePoint(CalendarSystem calendar, CalendarPoint point)
        {
            var elements =
                calendar.Elements.Where(c => c.IsValueElement)
                    .Select(c => c.Id)
                    .OrderBy(c => c)
                    .ToList();

            foreach (var id in elements)
            {
                Console.WriteLine("{0}: {1}", id.PadLeft(25), point.Get(id));
            }
        }

        [Test]
        public void Point20010101()
        {
            // Create the calendar.
            CalendarSystem calendar = CreateCalendar();
            CalendarPoint point = calendar.CreatePoint(2451910.5m);

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

        private CalendarSystem CreateCalendar()
        {
            // Create the calendar with a single open-ended cycle.
            var day = new ClosedCycle("Day", new JulianDayNumberBasis());
            var dayOfMonth = new CountedCycleBasis("Day of Month", day)
            {
                BasisLengthLogics =
                    new BasisLengthLogicCollection
                    {
                        new IfBasisLengthLogic(
                            "$(Month) in $(Feb)",
                            new IfBasisLengthLogic(
                                "$(Millennium)$(Century)$(Decade)$(Year) mod 400",
                                29),
                            new IfBasisLengthLogic(
                                "$(Millennium)$(Century)$(Decade)$(Year) mod 100",
                                28),
                            new IfBasisLengthLogic(
                                "$(Millennium)$(Century)$(Decade)$(Year) mod 4",
                                29),
                            new CountBasisLengthLogic(28)),
                        new IfBasisLengthLogic("$(Month) in $(Month31)", 31),
                        new IfBasisLengthLogic("$(Month) in $(Month30)", 30)
                    }
            };
            var month = new ClosedCycle("Month", dayOfMonth);
            var monthOfYear = new CountedCycleBasis("Month of Year", month, 12);
            var year = new ClosedCycle("Year", monthOfYear);
            var yearOfDecade = new CountedCycleBasis("Year of Decade", year, 10);
            var decade = new ClosedCycle("Decade", yearOfDecade);
            var decadeOfCentury = new CountedCycleBasis(
                "Decade of Century",
                decade,
                10);
            var century = new ClosedCycle("Century", decadeOfCentury);
            var centuryOfMillennium =
                new CountedCycleBasis("Century of Millennium", century, 10);
            var millennium = new OpenCycle("Millennium", centuryOfMillennium)
            {
                // 0001-01-01 00:00:00
                JulianDayNumberOffset = JulianDayNumberOffset
            };

            var calendar = new CalendarSystem
            {
                Elements =
                    new CalendarElementCollection<CalendarElement>
                    {
                        day,
                        dayOfMonth,
                        month,
                        monthOfYear,
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
            return calendar;
        }
    }
}
