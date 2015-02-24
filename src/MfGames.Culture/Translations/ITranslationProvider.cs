// <copyright file="ITranslationProvider.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	/// <summary>
	/// Defines the signature for a class that provides translations for a
	/// a given selector and key.
	/// </summary>
	public interface ITranslationProvider
	{
		#region Public Methods and Operators

		/// <summary>
		/// Retrieves a translation from the provider or null if one cannot be
		/// supplied. The resulting translation will always be the highest one
		/// as chosen by the selector.
		/// </summary>
		/// <param name="key">The key to identify the translation selected.</param>
		/// <param name="selector">The langauges that are acceptable and the preference for each one.</param>
		/// <returns>A translated value or null.</returns>
		TranslationResult GetTranslationResult(
			string key,
			LanguageTagSelector selector);

		#endregion
	}
}
