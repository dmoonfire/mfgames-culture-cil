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

		private readonly Dictionary<HierarchicalPath, MemoryTranslationCollection>
			translations;

		#endregion

		#region Constructors and Destructors

		public MemoryTranslationProvider()
		{
			translations =
				new Dictionary<HierarchicalPath, MemoryTranslationCollection>();
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
				translations[path] = new MemoryTranslationCollection();
			}

			// Set the value.
			translations[path][languageTag] = translation;
		}

		public TranslationResult GetTranslation(
			LanguageTagSelector selector,
			HierarchicalPath path)
		{
			// Make sure we have the path.
			MemoryTranslationCollection pathTranslations;

			if (!translations.TryGetValue(path, out pathTranslations))
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
