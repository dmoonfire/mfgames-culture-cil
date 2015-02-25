// <copyright file="TranslationManagerTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;
using MfGames.Culture.Translations;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Translations
{
	[TestFixture]
	public class TranslationManagerTests
	{
		#region Fields

		private LanguageTag english;

		#endregion

		#region Public Methods and Operators

		[Test]
		public void FallbackTranslation()
		{
			var manager = new CompositeTranslationProvider();
			LanguageTagSelector selector = LanguageTagSelector.Canonical;
			var key = "Some Random Key";
			string results = manager.GetTranslation(key, selector, "fallback");

			Assert.AreEqual("fallback", results, "Results are unexpected.");
		}

		[Test]
		public void FoundTranslation()
		{
			var key = "Lost in Translation";
			var memoryProvider = new MemoryTranslationProvider();

			memoryProvider.Add(key, english, "English A");

			var selector = new LanguageTagSelector("eng;q=1.0");
			string results = memoryProvider.GetTranslation(key, selector);

			Assert.AreEqual("English A", results, "Results are unexpected.");
		}

		[Test]
		public void LostTranslationWithOneProvided()
		{
			var key = "Lost in Translation";

			var memoryProvider = new MemoryTranslationProvider();

			memoryProvider.Add(key, english, "English A");

			var selector = new LanguageTagSelector("fra;q=1.0");
			string results = memoryProvider.GetTranslation(key, selector, "fallback");

			Assert.AreEqual("fallback", results, "Results are unexpected.");
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			LanguageCode englishLanguage =
				CodeManager.Instance.Languages.GetIsoAlpha3("eng");

			english = new LanguageTag(englishLanguage);
		}

		#endregion
	}
}
