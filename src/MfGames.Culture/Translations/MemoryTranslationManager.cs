// <copyright file="MemoryTranslationManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public class MemoryTranslationManager : ITranslationManager
	{
		#region Fields

		private readonly Dictionary<string, MemoryTranslationCollection> translations;

		#endregion

		#region Constructors and Destructors

		public MemoryTranslationManager()
		{
			translations = new Dictionary<string, MemoryTranslationCollection>();
		}

		#endregion

		#region Public Methods and Operators

		public void Add(
			string key,
			LanguageTag languageTag,
			string translation)
		{
			// Add the key, if we don't have it.
			if (!translations.ContainsKey(key))
			{
				translations[key] = new MemoryTranslationCollection();
			}

			// Set the value.
			translations[key][languageTag] = translation;
		}

		public TranslationResult GetTranslationResult(
			string key,
			LanguageTagSelector selector)
		{
			// Make sure we have the key.
			MemoryTranslationCollection pathTranslations;

			if (!translations.TryGetValue(key, out pathTranslations))
			{
				return null;
			}

			// Get the translation from the collection.
			TranslationResult results = pathTranslations.GetTranslation(selector);
			return results;
		}

		#endregion
	}
}
