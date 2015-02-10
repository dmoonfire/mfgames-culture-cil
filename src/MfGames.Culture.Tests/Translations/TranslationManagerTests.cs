// <copyright file="TranslationManagerTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;
using MfGames.Culture.Translations;
using MfGames.HierarchicalPaths;

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
			var manager = new TranslationManager();
			var selector = new LanguageTagSelector("*");
			var path = new HierarchicalPath("/");
			string results = manager.GetTranslation(selector, path, "fallback");

			Assert.AreEqual("fallback", results, "Results are unexpected.");
		}

		[Test]
		public void FoundTranslation()
		{
			var path = new HierarchicalPath("/");

			var memoryProvider = new MemoryTranslationProvider();
			memoryProvider.Add(path, english, "English A");

			var manager = new TranslationManager { memoryProvider };
			var selector = new LanguageTagSelector("eng;q=1.0");
			string results = manager.GetTranslation(selector, path, "fallback");

			Assert.AreEqual("English A", results, "Results are unexpected.");
		}

		[Test]
		public void LostTranslationWithOneProvided()
		{
			var path = new HierarchicalPath("/");

			var memoryProvider = new MemoryTranslationProvider();
			memoryProvider.Add(path, english, "English A");

			var manager = new TranslationManager { memoryProvider };
			var selector = new LanguageTagSelector("fra;q=1.0");
			string results = manager.GetTranslation(selector, path, "fallback");

			Assert.AreEqual("fallback", results, "Results are unexpected.");
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			LanguageCode englishLanguage = LanguageCodeManager.Instance.GetAlpha3("eng");

			english = new LanguageTag(englishLanguage);
		}

		#endregion
	}
}
