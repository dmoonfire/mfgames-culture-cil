// <copyright file="GregorianTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Tests.Calendars
{
    using System.Collections.Generic;

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

        [Test]
        public void RandomPoint()
        {
            CalendarSystem calendar = CreateCalendar();
            CalendarPoint point = calendar.CreatePoint(2457053.74037m);

            Assert.AreEqual(null, point);
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
                            "Month in $(Feb)",
                            new IfBasisLengthLogic("Year mod 400", 29),
                            new IfBasisLengthLogic("Year mod 100", 28),
                            new IfBasisLengthLogic("Year mod 4", 29),
                            new CountBasisLengthLogic(28)),
                        new IfBasisLengthLogic("Month in $(Month31)", 31),
                        new IfBasisLengthLogic("Month in $(Month30)", 30)
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
                JulianDayNumberOffset = 1721423.5m
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
                    new Dictionary<string, string>
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
