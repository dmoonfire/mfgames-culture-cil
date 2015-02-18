// <copyright file="LanguageCodeManagerTests.cs" company="Moonfire Games">
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
	public class LanguageCodeManagerTests
	{
		#region Public Methods and Operators

		[Test]
		public void AddDefaultsDoesNotBreak()
		{
			// Create the manager and populate the list.
			var manager = new LanguageCodeManager();

			manager.AddDefaults();
		}

		[Test]
		public void CreateCanonical()
		{
			LanguageCode canonical = LanguageCode.Canonical;

			Assert.AreEqual("*", canonical.IsoAlpha3);
		}

		[Test]
		public void CreateCustomCodes()
		{
			LanguageCodeManager.Instance = new LanguageCodeManager
			{
				new LanguageCode("xmi"),
				"xlo"
			};

			Assert.AreEqual(2, LanguageCodeManager.Instance.Count);

			foreach (LanguageCode lc in LanguageCodeManager.Instance)
			{
				Assert.IsNull(lc.IsoAlpha2);
			}
		}

		[Test]
		public void TestSingleton()
		{
			var manager = new LanguageCodeManager();
			LanguageCode english1 = manager.Get("eng");
			LanguageCode english2 = manager.Get("en");
			LanguageCode english3 = manager.GetIsoAlpha3("eng");
			LanguageCode english4 = manager.GetIsoAlpha3T("eng");

			Assert.AreEqual(english1, english2);
			Assert.AreEqual(english1, english3);
			Assert.AreEqual(english1, english4);
		}

		#endregion
	}
}
