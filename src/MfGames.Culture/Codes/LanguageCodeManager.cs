﻿// <copyright file="LanguageCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;

using MfGames.Culture.Translations;
using MfGames.Extensions.System;
using MfGames.HierarchicalPaths;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>LanguageCode</c>
	/// is created for a given language.
	/// </summary>
	public class LanguageCodeManager : ITranslationProvider
	{
		#region Static Fields

		private static LanguageCodeManager instance;

		#endregion

		#region Fields

		private readonly HashSet<LanguageCode> codes;

		private readonly MemoryTranslationProvider translations;

		#endregion

		#region Constructors and Destructors

		static LanguageCodeManager()
		{
			// The default is to have the modern world's values.
			instance = new LanguageCodeManager();
			instance.AddDefaults();
		}

		public LanguageCodeManager()
		{
			codes = new HashSet<LanguageCode>();
			translations = new MemoryTranslationProvider();
		}

		#endregion

		#region Public Properties

		public static LanguageCodeManager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"LanguageCodeManager.Instance cannot be assigned a null value.");
				}

				instance = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		[Pure]
		public static HierarchicalPath GetAlpha3ToNameTranslationPath(
			LanguageCode languageCode)
		{
			return GetAlpha3ToNameTranslationPath(languageCode.Alpha3);
		}

		[Pure]
		public static HierarchicalPath GetAlpha3ToNameTranslationPath(
			string languageCode)
		{
			return new HierarchicalPath("/ISO/639/Alpha3/Codes/" + languageCode);
		}

		public void AddDefaults()
		{
			// We have to pre-create English and French since they are used
			// for the translations in the file. However, we need the language
			// codes for the translations, which means we have a special case
			// where we have to inject the translations after the fact.
			var english = new LanguageCode("en", null, "eng", false);
			var french = new LanguageCode("fr", "fre", "fra", false);
			var englishTag = new LanguageTag(english);
			var frenchTag = new LanguageTag(french);

			codes.Add(english);
			codes.Add(french);

			// Add in the initial translations for English and French. We use
			// English for the fallback only because the developer reads
			// English better.
			translations.Add(
				GetAlpha3ToNameTranslationPath(english),
				LanguageTag.All,
				"English");
			translations.Add(
				GetAlpha3ToNameTranslationPath(english),
				englishTag,
				"English");
			translations.Add(
				GetAlpha3ToNameTranslationPath(english),
				frenchTag,
				"anglais");

			translations.Add(
				GetAlpha3ToNameTranslationPath(french),
				LanguageTag.All,
				"French");
			translations.Add(
				GetAlpha3ToNameTranslationPath(french),
				englishTag,
				"French");
			translations.Add(
				GetAlpha3ToNameTranslationPath(french),
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

					// Add the code to the list.
					var code = new LanguageCode(
						alpha2,
						alpha3B,
						alpha3T,
						false);

					codes.Add(code);

					// Add in the translations for these names.
					translations.Add(
						GetAlpha3ToNameTranslationPath(code),
						LanguageTag.All,
						englishName);
					translations.Add(
						GetAlpha3ToNameTranslationPath(code),
						englishTag,
						englishName);
					translations.Add(
						GetAlpha3ToNameTranslationPath(code),
						frenchTag,
						frenchName);
				}
			}
		}

		public LanguageCode Get(string language)
		{
			if (language == "*")
			{
				return LanguageCode.All;
			}

			return GetAlpha3(language);
		}

		public LanguageCode GetAlpha3(string alpha3)
		{
			if (alpha3 == "*")
			{
				return LanguageCode.All;
			}

			return codes.FirstOrDefault(c => c.Alpha3B == alpha3 || c.Alpha3T == alpha3);
		}

		public TranslationResult GetTranslation(
			LanguageTagSelector selector,
			HierarchicalPath path)
		{
			return translations.GetTranslation(selector, path);
		}

		#endregion
	}
}
