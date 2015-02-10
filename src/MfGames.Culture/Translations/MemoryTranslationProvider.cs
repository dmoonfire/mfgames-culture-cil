// <copyright file="MemoryTranslationProvider.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

using MfGames.Culture.Codes;
using MfGames.HierarchicalPaths;

namespace MfGames.Culture.Translations
{
	public class MemoryTranslationProvider : ITranslationProvider
	{
		#region Fields

		private readonly Dictionary<HierarchicalPath, Dictionary<LanguageTag, string>>
			translations;

		#endregion

		#region Constructors and Destructors

		public MemoryTranslationProvider()
		{
			translations =
				new Dictionary<HierarchicalPath, Dictionary<LanguageTag, string>>();
		}

		#endregion

		#region Public Methods and Operators

		public void Add(
			string path,
			LanguageTag languageTag,
			string translation)
		{
			var hierarchicalPath = new HierarchicalPath(path);
			Add(hierarchicalPath, languageTag, translation);
		}

		public void Add(
			HierarchicalPath path,
			LanguageTag languageTag,
			string translation)
		{
			// Add the path, if we don't have it.
			if (!translations.ContainsKey(path))
			{
				translations[path] = new Dictionary<LanguageTag, string>();
			}

			// Set the value.
			translations[path][languageTag] = translation;
		}

		public TranslationResult GetTranslation(
			LanguageTagSelector selector,
			HierarchicalPath path)
		{
			// Make sure we have the path.
			Dictionary<LanguageTag, string> pathTranslations;

			if (!translations.TryGetValue(path, out pathTranslations))
			{
				return null;
			}

			// Loop through the language choices and find the first one that
			// matches a translation we have.
			foreach (LanguageTagQuality quality in selector)
			{
				string translation;

				if (pathTranslations.TryGetValue(quality.LanguageTag, out translation))
				{
					var results = new TranslationResult(quality, translation);
					return results;
				}
			}

			// If we got through the loop, then we don't have anything.
			return null;
		}

		#endregion
	}
}
