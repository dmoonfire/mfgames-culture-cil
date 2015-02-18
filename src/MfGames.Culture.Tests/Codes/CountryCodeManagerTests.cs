// <copyright file="CountryCodeManagerTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Codes
{
	[TestFixture]
	public class CountryCodeManagerTests
	{
		#region Public Methods and Operators

		[Test]
		public void AddDefaultsDoesNotBreak()
		{
			// Create the manager and populate the list.
			var manager = new CountryCodeManager();

			manager.AddDefaults();
		}

		#endregion
	}
}
