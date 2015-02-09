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

		#endregion
	}
}
