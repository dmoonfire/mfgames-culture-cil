// <copyright file="LanguageCodeTests.cs" company="Moonfire Games">
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
	public class LanguageCodeTests
	{
		#region Public Methods and Operators

		[Test]
		public void ArmenianCodes()
		{
			var armenian = new LanguageCode("hye", "hy", "arm");

			Assert.AreEqual("hye", armenian.IsoAlpha3);
			Assert.AreEqual("hye", armenian.IsoAlpha3T);
			Assert.AreEqual("arm", armenian.IsoAlpha3B);
			Assert.AreEqual("hy", armenian.IsoAlpha2);
			Assert.AreEqual("hy", armenian.ToString());
		}

		[Test]
		public void TwoCodesAreIdentical()
		{
			var english1 = new LanguageCode("eng");
			var english2 = new LanguageCode("eng", "en");

			Assert.AreEqual(english1, english2);
		}

		#endregion
	}
}
