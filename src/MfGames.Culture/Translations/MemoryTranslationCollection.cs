// <copyright file="MemoryTranslationCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;
using System.Linq;

using MfGames.Culture.Codes;
using MfGames.HierarchicalPaths;

namespace MfGames.Culture.Translations
{
	public class MemoryTranslationCollection : ITranslationCollection,
		ITranslationProvider
	{
		#region Fields

		private readonly Dictionary<LanguageTag, string> translations;

		private LanguageTag defaultLanguageTag;

		#endregion

		#region Constructors and Destructors

		public MemoryTranslationCollection()
		{
			translations = new Dictionary<LanguageTag, string>();
		}

		#endregion

		#region Public Indexers

		public string this[LanguageTag languageTag]
		{
			get { return translations[languageTag]; }
			set
			{
				translations[languageTag] = value;

				if (defaultLanguageTag == null)
				{
					defaultLanguageTag = languageTag;
				}
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Add(LanguageTag tag, string translation)
		{
			this[tag] = translation;
		}

		public string GetFallback()
		{
			// Try to get the "all" name.
			TranslationResult result = GetTranslation(LanguageTagSelector.All);

			if (result != null)
			{
				return result.Result;
			}

			// If we don't, then just grab one.
			return translations.Values.FirstOrDefault();
		}

		public TranslationResult GetTranslation(LanguageTagSelector selector)
		{
			// Loop through the language choices and find the first one that
			// matches a translation we have.
			foreach (LanguageTagQuality quality in selector)
			{
				string translation;

				if (translations.TryGetValue(quality.LanguageTag, out translation))
				{
					var results = new TranslationResult(quality, translation);
					return results;
				}
			}

			// If we got through the loop, then we don't have anything.
			return null;
		}

		public TranslationResult GetTranslationResult(
			LanguageTagSelector selector,
			HierarchicalPath path)
		{
			return GetTranslation(selector);
		}

		#endregion
	}
}
