// <copyright file="CountryCodeManagerTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;
using MfGames.Culture.Translations;

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
			// Create the languages with defaults.
			var languages = new LanguageCodeManager();

			languages.AddDefaults(new NullTranslationManager());

			// Create the country code manager.
			var countries = new CountryCodeManager();

			countries.AddDefaults(languages);
		}

		#endregion
	}
}
