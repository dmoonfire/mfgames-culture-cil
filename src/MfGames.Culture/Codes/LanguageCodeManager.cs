// <copyright file="LanguageCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using MfGames.Culture.Translations;
using MfGames.Extensions.System;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>LanguageCode</c>
	/// is created for a given language.
	/// </summary>
	public class LanguageCodeManager : ILanguageCodeManager
	{
		#region Fields

		private readonly HashSet<LanguageCode> codes;

		private string translationKeyFormat;

		#endregion

		#region Constructors and Destructors

		public LanguageCodeManager()
		{
			codes = new HashSet<LanguageCode>();

			TranslationKeyFormat = "/ISO/639/IsoAlpha3/Codes/{0}";
		}

		#endregion

		#region Public Properties

		public int Count { get { return codes.Count; } }

		public string TranslationKeyFormat
		{
			get { return translationKeyFormat; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Cannot assign a null translation key format.");
				}

				if (!value.Contains("{0}"))
				{
					throw new ArgumentException(
						"The translation key format must have a '{0}' in the format.",
						"value");
				}

				translationKeyFormat = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Add(LanguageCode languageCode)
		{
			if (languageCode == null)
			{
				throw new ArgumentNullException("languageCode");
			}

			codes.Add(languageCode);
		}

		public void Add(string isoAlpha3T)
		{
			var languageCode = new LanguageCode(isoAlpha3T);

			Add(languageCode);
		}

		public void AddDefaults(CodeManager codeManager)
		{
			// We have to pre-create English and French since they are used
			// for the translations in the file. However, we need the language
			// codes for the translations, which means we have a special case
			// where we have to inject the translations after the fact.
			var english = new LanguageCode("eng", "en", null, false);
			var french = new LanguageCode("fra", "fr", "fre", false);
			var frenchTag = new LanguageTag(french);

			codes.Add(english);
			codes.Add(french);

			// Add in the initial translations for English and French for
			// Englisha nd French.
			ITranslationManager translations = codeManager.Translations;

			AddLanguageNameTranslation(
				translations,
				english,
				"English",
				frenchTag,
				"anglais");
			AddLanguageNameTranslation(
				translations,
				french,
				"French",
				frenchTag,
				"français");

			// Load the defaults from an embedded resource.
			Assembly assembly = GetType().Assembly;
			const string Name = "MfGames.Culture.Codes.ISO-639-2_utf-8.txt";

			using (Stream stream = assembly.GetManifestResourceStream(Name))
			using (var reader = new StreamReader(stream))
			{
				// Loop through all the lines in the file.
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					// Ignore comments and blank lines, we added those for documentation.
					if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
					{
						continue;
					}

					// Split the line on the pipe characters and assign the
					// parts into symbolic names.
					string[] parts = line.Split('|');
					string alpha3B = parts[0].NullIfBlank();
					string alpha3T = parts[1].NullIfBlank();
					string alpha2 = parts[2].NullIfBlank();
					string englishName = parts[3].NullIfBlank();
					string frenchName = parts[4].NullIfBlank();

					// Ignore English and French since we've already added them.
					if (alpha3B == "eng" || alpha3B == "fre")
					{
						continue;
					}

					// As per http://en.wikipedia.org/wiki/ISO_639-2, the
					// T-codes are preferred over the B-codes, but this file
					// has the B-codes given if they are identical.
					if (alpha3T == null)
					{
						alpha3T = alpha3B;
						alpha3B = null;
					}

					// We can't handle some codes.
					if (alpha3T.Length != 3)
					{
						continue;
					}

					// Add the code to the list.
					var code = new LanguageCode(
						alpha3T,
						alpha2,
						alpha3B,
						false);

					codes.Add(code);

					// Add in the translations for these names.
					AddLanguageNameTranslation(
						translations,
						code,
						englishName,
						frenchTag,
						frenchName);
				}
			}
		}

		public LanguageCode Get(string language)
		{
			// Check for canonical first.
			if (language == "*")
			{
				return LanguageCode.Canonical;
			}

			// Check ISO 639 codes.
			LanguageCode code = GetIsoAlpha3(language);

			if (code != null)
			{
				return code;
			}

			code = GetIsoAlpha2(language);

			if (code != null)
			{
				return code;
			}

			// We can't find it.
			return null;
		}

		public IEnumerator<LanguageCode> GetEnumerator()
		{
			return codes.GetEnumerator();
		}

		public LanguageCode GetIsoAlpha2(string alpha2)
		{
			return alpha2 == "*"
				? LanguageCode.Canonical
				: codes.FirstOrDefault(c => c.IsoAlpha2 == alpha2);
		}

		public LanguageCode GetIsoAlpha3(string alpha3)
		{
			return alpha3 == "*"
				? LanguageCode.Canonical
				: codes.FirstOrDefault(
					c => c.IsoAlpha3B == alpha3 || c.IsoAlpha3T == alpha3);
		}

		public LanguageCode GetIsoAlpha3B(string alpha3)
		{
			return alpha3 == "*"
				? LanguageCode.Canonical
				: codes.FirstOrDefault(c => c.IsoAlpha3B == alpha3);
		}

		public LanguageCode GetIsoAlpha3T(string alpha3)
		{
			return alpha3 == "*"
				? LanguageCode.Canonical
				: codes.FirstOrDefault(c => c.IsoAlpha3T == alpha3);
		}

		public TranslationResult GetTranslationResult(
			string key,
			LanguageTagSelector selector)
		{
			return translations.GetTranslationResult(key, selector);
		}

		#endregion

		#region Explicit Interface Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a translated name for a code into the translation.
		/// </summary>
		/// <remarks>
		/// This uses English as the canonical name simply because the
		/// developer speaks English better than French.</remarks>
		/// <param name="translations"></param>
		/// <param name="languageCode"></param>
		/// <param name="englishName"></param>
		/// <param name="frenchName"></param>
		private void AddLanguageNameTranslation(
			ITranslationManager translations,
			LanguageCode languageCode,
			string englishName,
			LanguageTag french,
			string frenchName)
		{
			// Figure out the path.
			string key = string.Format(translationKeyFormat, languageCode.IsoAlpha3);

			translations.Add(
				key,
				LanguageTag.Canonical,
				englishName);
			translations.Add(
				key,
				french,
				frenchName);
		}

		#endregion
	}
}
