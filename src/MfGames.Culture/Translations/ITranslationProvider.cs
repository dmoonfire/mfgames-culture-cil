// <copyright file="ITranslationProvider.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;
using MfGames.HierarchicalPaths;

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
		/// supplied.
		/// </summary>
		/// <param name="selector">The langauges to accept and their weights.</param>
		/// <param name="path">The identifier to the translation.</param>
		/// <returns>A translated value or null.</returns>
		TranslationResult GetTranslationResult(
			LanguageTagSelector selector,
			HierarchicalPath path);

		#endregion
	}
}
