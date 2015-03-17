// <copyright file="LanguageTagTests.cs" company="Moonfire Games">
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
	public class LanguageTagTests
	{
		#region Fields

		private readonly ICountryCodeManager countries;

		private readonly LanguageCode english;

		private readonly ILanguageCodeManager languages;

		private readonly CountryCode unitedStates;

		#endregion

		#region Constructors and Destructors

		public LanguageTagTests()
		{
			languages = CodeManager.Instance.Languages;
			english = languages.Get("eng");

			countries = CodeManager.Instance.Countries;
			unitedStates = countries.Get("US");
		}

		#endregion

		#region Public Methods and Operators

		[Test]
		public void ParsePrimaryLanguageAndCountry()
		{
			var tag = new LanguageTag("en-US");

			Assert.AreEqual(english, tag.Language, "Language is unexpected.");
			Assert.AreEqual(unitedStates, tag.Country, "Country is unexpected.");
			Assert.AreEqual("en-US", tag.ToString(), "ToString is unexpected.");
		}

		[Test]
		public void ParsePrimaryLanguageOnly()
		{
			var tag = new LanguageTag("en");

			Assert.AreEqual(english, tag.Language, "Language is unexpected.");
			Assert.AreEqual(null, tag.Country, "Country is unexpected.");
			Assert.AreEqual("en", tag.ToString(), "ToString is unexpected.");
		}

		#endregion
	}
}

/*
 * a single primary language subtag based on a two-letter language code from ISO 639-1 (2002)
 * or a three-letter code from ISO 639-2 (1998), ISO 639-3 (2007) or ISO 639-5
 * (2008), or registered through the BCP 47 process and composed of five to eight letters;
 *
 * up to three optional extended language subtags composed of three letters each,
 * separated by hyphens; (There is currently no extended language subtag
 * registered in the Language Subtag Registry without an equivalent and preferred
 * primary language subtag. This component of language tags is preserved for
 * backwards compatibility and to allow for future parts of ISO 639.)
 *
 * an optional script subtag, based on a four-letter script code from ISO
 * 15924 (usually written in title case);
 *
 * an optional region subtag based on a two-letter country code from ISO
 * 3166-1 alpha-2 (usually written in upper case), or a three-digit code from
 * UN M.49 for geographical regions;
 *
 * optional variant subtags, separated by hyphens, each composed of five to
 * eight letters, or of four characters starting with a digit; (Variant subtags
 * are registered with IANA and not associated with any external standard.)
 *
 * optional extension subtags, separated by hyphens, each composed of a single
 * character, with the exception of the letter x, and a hyphen followed by one
 * or more subtags of two to eight characters each, separated by hyphens;
 * Examples: t-Language.
 *
 * an optional private-use subtag, composed of the letter x and a hyphen
 * followed by subtags of one to eight characters each, separated by hyphens.
*/
