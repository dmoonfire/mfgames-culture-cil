// <copyright file="IfBasisLengthLogicTests.cs" company="Moonfire Games">
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
    public class IfBasisLengthLogicTests
    {
        [Test]
        public void ZeroModFive()
        {
            // Create the logic with a simple condition.
            var logic = new IfBasisLengthLogic("0 mod 5", 1);

            // Execute the logic and get the result and count.
            var variables = new Dictionary<string, object>();
            var values = new CalendarElementValueDictionary();
            int count;

            bool results = logic.GetCount(variables, values, out count);

            // Verify the results.
            Assert.AreEqual(true, results, "The results were unexpected.");
            Assert.AreEqual(1, count, "The count was unexpected.");
        }

        [Test]
        public void OneModFive()
        {
            // Create the logic with a simple condition.
            var logic = new IfBasisLengthLogic("1 mod 5", 1);

            // Execute the logic and get the result and count.
            var variables = new Dictionary<string, object>();
            var values = new CalendarElementValueDictionary();
            int count;

            bool results = logic.GetCount(variables, values, out count);

            // Verify the results.
            Assert.AreEqual(false, results, "The results were unexpected.");
            Assert.AreEqual(0, count, "The count was unexpected.");
        }

        [Test]
        public void FiveModFive()
        {
            // Create the logic with a simple condition.
            var logic = new IfBasisLengthLogic("5 mod 5", 1);

            // Execute the logic and get the result and count.
            var variables = new Dictionary<string, object>();
            var values = new CalendarElementValueDictionary();
            int count;

            bool results = logic.GetCount(variables, values, out count);

            // Verify the results.
            Assert.AreEqual(true, results, "The results were unexpected.");
            Assert.AreEqual(1, count, "The count was unexpected.");
        }
    }
}
