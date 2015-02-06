// <copyright file="LanguageCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>LanguageCode</c>
	/// is created for a given language.
	/// </summary>
	public class LanguageCodeManager
	{
		#region Static Fields

		private static LanguageCodeManager instance;

		#endregion

		#region Fields

		private readonly HashSet<LanguageCode> codes;

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

		public void AddDefaults()
		{
			// We have to pre-create English and French since they are used
			// for the translations in the file. However, we need the language
			// codes for the translations, which means we have a special case
			// where we have to inject the translations after the fact.
			var englishTranslation = new Translation();
			var frenchTranslation = new Translation();
			var english = new LanguageCode(englishTranslation, "en", null, "eng", false);
			var french = new LanguageCode(frenchTranslation, "fr", "fre", "fra", false);

			englishTranslation.AddIntern(english, "English");
			englishTranslation.AddIntern(french, "anglais");

			frenchTranslation.AddIntern(english, "French");
			frenchTranslation.AddIntern(french, "français");

			codes.Add(english);
			codes.Add(french);

			// Load the defaults from an embedded resource.
			Assembly assembly = GetType().Assembly;

			using (
				Stream stream =
					assembly.GetManifestResourceStream(
						"MfGames.Culture.Codes.ISO-639-2_utf-8.txt"))
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
					string alpha3B = GetNullIfBlank(parts[0]);
					string alpha3T = GetNullIfBlank(parts[1]);
					string alpha2 = GetNullIfBlank(parts[2]);
					string englishName = GetNullIfBlank(parts[3]);
					string frenchName = GetNullIfBlank(parts[4]);

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

					// Add in the translations. We don't have to worry about
					// changing the underlying translations in the code because
					// as soon as this goes out of scope, the immutable verison
					// in the code will be the only way to access it.
					var translation = new Translation();
					translation.AddIntern(english, englishName);
					translation.AddIntern(french, frenchName);

					// Add the code to the list.
					var code = new LanguageCode(
						translation,
						alpha2,
						alpha3B,
						alpha3T,
						false);

					codes.Add(code);
				}
			}
		}

		#endregion

		#region Methods

		private string GetNullIfBlank(string input)
		{
			return string.IsNullOrWhiteSpace(input) ? null : input;
		}

		#endregion
	}
}
