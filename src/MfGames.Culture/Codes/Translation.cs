// <copyright file="Translation.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

namespace MfGames.Culture.Codes
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

		public void Add(LanguageCode languageCode, string translation)
		{
			var entry = new TranslationEntry(languageCode, translation);

			entries.Add(entry);
		}

		public void AddIntern(LanguageCode languageCode, string translation)
		{
			Add(languageCode, string.Intern(translation));
		}

		#endregion

		private class TranslationEntry
		{
			#region Constructors and Destructors

			public TranslationEntry(LanguageCode languageCode, string translation)
			{
				LanguageCode = languageCode;
				Translation = translation;
			}

			#endregion

			#region Public Properties

			public LanguageCode LanguageCode { get; set; }
			public string Translation { get; set; }

			#endregion
		}
	}
}
