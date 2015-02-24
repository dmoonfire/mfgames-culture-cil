// <copyright file="MemoryTranslationProvider.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Linq;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public class MemoryTranslationProvider : ITranslationManager
	{
		#region Fields

		private readonly Dictionary<string, MemoryTranslationCollection> translations;

		#endregion

		#region Constructors and Destructors

		public MemoryTranslationProvider()
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

		/// <summary>
		/// Adds a list of translations to the collection using the root key
		/// and with a subkey of the zero-based index. The format must include
		/// a "{0}" for the numerical index.
		/// </summary>
		public void AddRange(
			string format,
			LanguageTag languageTag,
			params string[] names)
		{
			// Check our constraints.
			if (!format.Contains("{0}"))
			{
				throw new ArgumentException(
					"Format string must have a '{0}' somewhere in its contents.",
					"format");
			}

			// Loop through all the names and add the translation for each one.
			for (var i = 0; i < names.Length; i++)
			{
				string key = string.Format(format, i);

				Add(key, languageTag, names[i]);
			}
		}

		/// <summary>
		/// Adds a list of translations to the collection using the root key
		/// and with a subkey of the zero-based index.
		/// </summary>
		public void AddRange(
			string format,
			LanguageTag languageTag,
			IEnumerable<string> names)
		{
			AddRange(format, languageTag, names.ToArray());
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
