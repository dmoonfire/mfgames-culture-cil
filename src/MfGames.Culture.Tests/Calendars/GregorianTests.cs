// <copyright file="GregorianTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

using System;
using System.Globalization;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Calendars.Lengths;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Calendars
{
    [TestFixture]
    public class GregorianTests
    {
        #region Fields

        private CalendarSystem calendar;

        private CycleLength yearLength1;

        #endregion

        #region Public Methods and Operators

        [Test]
        public void Test()
        {
            decimal d0 = ToJulianDate(1999, 1, 1);
            decimal d1 = ToJulianDate(2000, 1, 1);
            decimal d2 = ToJulianDate(2001, 1, 1);
            decimal d3 = ToJulianDate(2002, 1, 1);

            Console.WriteLine("1999 - 2000 = " + (d1 - d0));
            Console.WriteLine("2000 - 2001 = " + (d2 - d1));
            Console.WriteLine("2001 - 2002 = " + (d3 - d2));
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            // Everything is hung off the year. We use a number of lengths for
            // this. The first is to "fast forward" every 400 years until we
            // get closer to the point. The second is to figure out the precise
            // length of the year. Since we don't use another element as a
            // reference, it defaults to 1.0m Julian Days.
            var year = new OpenCycle("Year")
            {
                JulianDateOffset = -0.5m - 1721059m
            };
            var yearLength400 = new CycleLength(
                400,
                365m * 400 + 100m - 4m + 1m);
            yearLength1 = new CycleLength(
                new IfModLengthLogic("Year", 400, 366),
                new IfModLengthLogic("Year", 100, 365),
                new IfModLengthLogic("Year", 4, 366),
                new ConstantLengthLogic(365));

            year.Lengths.Add(yearLength400);
            year.Lengths.Add(yearLength1);

            // Create the calendar and add the open cycle which will add
            // everything else.
            calendar = new CalendarSystem();
            calendar.Add(year);
        }

        [Test]
        public void Verify15830101()
        {
            decimal julianDate = ToJulianDate(1583, 1, 1);
            dynamic point = calendar.Create(julianDate);

            Assert.AreEqual(1583, point.Year, "Year");
        }

        [Test]
        public void Verify20000101()
        {
            decimal julianDate = ToJulianDate(2000, 1, 1);
            dynamic point = calendar.Create(julianDate);

            Assert.AreEqual(2000, point.Year, "Year");
        }

        [Test]
        public void Verify20010101()
        {
            decimal julianDate = ToJulianDate(2001, 1, 1);
            dynamic point = calendar.Create(julianDate);

            Assert.AreEqual(2001, point.Year, "Year");
        }

        [Test]
        public void Verify20020101()
        {
            decimal julianDate = ToJulianDate(2002, 1, 1);
            dynamic point = calendar.Create(julianDate);

            Assert.AreEqual(2002, point.Year, "Year");
        }

        [Test]
        public void Verify20030101()
        {
            decimal julianDate = ToJulianDate(2003, 1, 1);
            dynamic point = calendar.Create(julianDate);

            Assert.AreEqual(2003, point.Year, "Year");
        }

        [Test]
        public void Verify20040101()
        {
            decimal julianDate = ToJulianDate(2004, 1, 1);
            dynamic point = calendar.Create(julianDate);

            Assert.AreEqual(2004, point.Year, "Year");
        }

        [Test]
        public void VerifyYearLength0()
        {
            // Calculate the index of the year.
            var values = new CalendarElementValueCollection();

            values["Year"] = 0;
            yearLength1.CalculateIndex("Year", values, 0m);

            // Verify the results.
            Assert.AreEqual(0, values["Year"]);
        }

        [Test]
        public void VerifyYearLength365()
        {
            // Calculate the index of the year.
            var values = new CalendarElementValueCollection();

            values["Year"] = 0;
            yearLength1.CalculateIndex("Year", values, 365m);

            // Verify the results.
            Assert.AreEqual(0, values["Year"]);
        }

        [Test]
        public void VerifyYearLength366()
        {
            // Calculate the index of the year.
            var values = new CalendarElementValueCollection();

            values["Year"] = 0;
            yearLength1.CalculateIndex("Year", values, 366m);

            // Verify the results.
            Assert.AreEqual(1, values["Year"]);
        }

        [Test]
        public void VerifyYearLength367()
        {
            // Calculate the index of the year.
            var values = new CalendarElementValueCollection();

            values["Year"] = 0;
            yearLength1.CalculateIndex("Year", values, 367m);

            // Verify the results.
            Assert.AreEqual(1, values["Year"]);
        }

        #endregion

        #region Methods

        private decimal ToJulianDate(DateTime dateTime)
        {
            // http://stackoverflow.com/questions/5248827/convert-datetime-to-julian-date-in-c-sharp-tooadate-safe
            decimal results = (decimal)dateTime.ToOADate() + 2415018.5m;

            var gregorian = new GregorianCalendar();
            Console.WriteLine(
                "Julian Date: {0} = {1} (Leap {2}, {3})",
                dateTime.ToString("yyyy-MM-dd"),
                results,
                gregorian.IsLeapYear(dateTime.Year),
                (decimal)
                    new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToOADate
                        () + 2415018.5m - results);
            return results;
        }

        private decimal ToJulianDate(int year, int month, int day)
        {
            var dateTime = new DateTime(
                year,
                month,
                day,
                0,
                0,
                0,
                DateTimeKind.Utc);
            return ToJulianDate(dateTime);
        }

        #endregion
    }
}
