﻿// <copyright file="UnitedStatesCultureTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Calendars;

using NUnit.Framework;

namespace MfGames.Culture.Tests
{
	/// <summary>
	/// Defines tests around creating a US culture.
	/// </summary>
	[TestFixture]
	public class UnitedStatesCultureTests
	{
		#region Fields

		private CultureSystem culture;

		#endregion

		#region Public Methods and Operators

		[TestFixtureSetUp]
		public void SetUp()
		{
			// Create the culture we are populating.
			culture = new CultureSystem();

			// Grab our two base calendars.
			var gregorian = new GregorianCalendarSystem();
			var duodecimal = new DuodecimalCalendarSystem();

			culture.Calendars.Add(gregorian);
			culture.Calendars.Add(duodecimal);

			// Add in the known formats.
			culture.Formats.Add(
				"yyyy-MM-dd",
				"$(Year:0000)-$(Year Month:00)-$(Month Day:00");
		}

		#endregion
	}
}
