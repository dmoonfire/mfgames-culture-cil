// <copyright file="ITranslationManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	/// <summary>
	/// Describes the interface for something that can set or add translations.
	/// </summary>
	public interface ITranslationManager : ITranslationProvider
	{
		#region Public Methods and Operators

		void Add(
			string key,
			LanguageTag languageTag,
			string translation);

		/// <summary>
		/// Adds a list of translations to the collection using the root key
		/// and with a subkey of the zero-based index. The format must include
		/// a "{0}" for the numerical index.
		/// </summary>
		void AddRange(
			string format,
			LanguageTag languageTag,
			params string[] names);

		/// <summary>
		/// Adds a list of translations to the collection using the root key
		/// and with a subkey of the zero-based index.
		/// </summary>
		void AddRange(
			string format,
			LanguageTag languageTag,
			IEnumerable<string> names);

		#endregion
	}
}
