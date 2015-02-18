// <copyright file="TranslationResult.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public class TranslationResult
	{
		#region Constructors and Destructors

		public TranslationResult(LanguageTagQuality quality, string result)
		{
			Quality = quality;
			Result = result;
		}

		#endregion

		#region Public Properties

		public LanguageTagQuality Quality { get; private set; }
		public string Result { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static bool operator >(
			TranslationResult result1,
			TranslationResult result2)
		{
			return result1.Quality.Quality > result2.Quality.Quality;
		}

		public static bool operator <(
			TranslationResult result1,
			TranslationResult result2)
		{
			return result1.Quality.Quality < result2.Quality.Quality;
		}

		#endregion
	}
}
