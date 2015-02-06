// <copyright file="Translation.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public class Translation : ITranslation

	{
		#region Fields

		private readonly List<TranslationEntry> entries;

		#endregion

		#region Constructors and Destructors

		public Translation()
		{
			entries = new List<TranslationEntry>();
		}

		#endregion

		#region Public Properties

		public int Count { get { return entries.Count; } }
		public string First { get { return entries[0].Translation; } }
		public bool IsImmutable { get { return false; } }

		#endregion

		#region Public Methods and Operators

		public void Add(LanguageTag languageTag, string translation)
		{
			var entry = new TranslationEntry(languageTag, translation);

			entries.Add(entry);
		}

		public void AddIntern(LanguageTag languageTag, string translation)
		{
			Add(languageTag, string.Intern(translation));
		}

		public override string ToString()
		{
			if (entries.Count == 0)
			{
				return "Translation(0)";
			}

			TranslationEntry entry = entries[0];

			return string.Format(
				"Translation({2:N0}, {0}, {1})",
				entry.LanguageTag,
				entry.Translation,
				entries.Count);
		}

		#endregion

		private class TranslationEntry
		{
			#region Constructors and Destructors

			public TranslationEntry(LanguageTag languageTag, string translation)
			{
				LanguageTag = languageTag;
				Translation = translation;
			}

			#endregion

			#region Public Properties

			public LanguageTag LanguageTag { get; private set; }
			public string Translation { get; private set; }

			#endregion

			#region Public Methods and Operators

			public override string ToString()
			{
				return string.Format(
					"[{0}, {1}]",
					LanguageTag,
					Translation);
			}

			#endregion
		}
	}
}
