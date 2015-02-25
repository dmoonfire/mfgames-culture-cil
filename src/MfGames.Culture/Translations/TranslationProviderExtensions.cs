// <copyright file="TranslationProviderExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public static class TranslationProviderExtensions
	{
		#region Public Methods and Operators

		public static string GetTranslation(
			this ITranslationProvider translationProvider,
			string key,
			LanguageTagSelector selector,
			string fallback = null)
		{
			// Get the translation result.
			TranslationResult result = translationProvider.GetTranslationResult(
				key,
				selector);

			if (result == null)
			{
				return fallback;
			}

			return result.Result;
		}

		#endregion
	}
}
