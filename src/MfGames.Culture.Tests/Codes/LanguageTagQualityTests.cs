// <copyright file="LanguageTagQualityTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using MfGames.Culture.Codes;

using NUnit.Framework;

namespace MfGames.Culture.Tests.Codes
{
	[TestFixture]
	public class LanguageTagQualityTests
	{
		#region Fields

		private readonly LanguageTag english;

		#endregion

		#region Constructors and Destructors

		public LanguageTagQualityTests()
		{
			LanguageCode eng = CodeManager.Instance.Languages.GetIsoAlpha3("eng");

			english = new LanguageTag(eng);
		}

		#endregion

		#region Public Methods and Operators

		[Test]
		public void DefaultQuality()
		{
			var quality = new LanguageTagQuality(english);

			Assert.AreEqual(1.0f, quality.Quality, "Quality is unexpected.");
		}

		[Test]
		public void NegativeQuality()
		{
			Assert.Throws<ArgumentOutOfRangeException>(
				delegate { var l = new LanguageTagQuality(english, -1f); });
		}

		[Test]
		public void NoLanguage()
		{
			Assert.Throws<ArgumentNullException>(
				delegate { var l = new LanguageTagQuality(null, 0); });
		}

		[Test]
		public void ParseLanguageAlpha3()
		{
			var quality = new LanguageTagQuality("eng");

			Assert.AreEqual(english, quality.LanguageTag, "LanguageTag is unexpected.");
			Assert.AreEqual(1.0f, quality.Quality, "Quality is unexpected.");
		}

		#endregion
	}
}
