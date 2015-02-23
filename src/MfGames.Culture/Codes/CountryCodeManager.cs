// <copyright file="CountryCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using CsvHelper;
using CsvHelper.Configuration;

using MfGames.Culture.Translations;
using MfGames.Extensions.System;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>CountryCode</c>
	/// is created for a given country.
	/// </summary>
	public class CountryCodeManager : ICountryCodeManager
	{
		#region Static Fields

		private static ICountryCodeManager instance;

		#endregion

		#region Fields

		private readonly HashSet<CountryCode> codes;

		#endregion

		#region Constructors and Destructors

		static CountryCodeManager()
		{
			instance = new CountryCodeManager();
			instance.AddDefaults();
		}

		public CountryCodeManager()
		{
			codes = new HashSet<CountryCode>();
		}

		#endregion

		#region Public Properties

		public static ICountryCodeManager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"CountryCodeManager.Instance cannot be assigned a null value.");
				}

				instance = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void AddDefaults()
		{
			AddDefaults(LanguageCodeManager.Instance);
		}

		public void AddDefaults(ILanguageCodeManager languages)
		{
			// Set up the English language code.
			LanguageCode english = languages.GetIsoAlpha3("eng");
			var englishTag = new LanguageTag(english);

			// Load the defaults from an embedded resource.
			Assembly assembly = GetType().Assembly;
			const string Name = "MfGames.Culture.Codes.iso_3166_2_countries.csv";
			var csvOptions = new CsvConfiguration { AllowComments = true };

			using (Stream stream = assembly.GetManifestResourceStream(Name))
			using (var reader = new StreamReader(stream))
			using (var csv = new CsvReader(reader, csvOptions))
			{
				// Loop through the input file.
				while (csv.Read())
				{
					// Pull out the fields from the row.
					var commonName = csv.GetField<string>("Common Name");
					var formalName = csv.GetField<string>("Formal Name");
					string alpha2 = csv.GetField<string>("ISO 3166-1 2 Letter Code")
						.NullIfBlank();
					string alpha3 = csv.GetField<string>("ISO 3166-1 3 Letter Code")
						.NullIfBlank();
					string numericString = csv.GetField<string>("ISO 3166-1 Number")
						.NullIfBlank();

					// Convert the numeric to an int?.
					short? numeric = numericString == null
						? (short?)null
						: short.Parse(numericString);

					// Create the translations for this country.
					var translation = new MemoryTranslationCollection();

					translation.Add(LanguageTag.Canonical, commonName ?? formalName);

					if (!string.IsNullOrWhiteSpace(commonName))
					{
						translation.Add(englishTag, commonName);
					}

					if (!string.IsNullOrWhiteSpace(formalName))
					{
						translation.Add(englishTag, formalName);
					}

					// Create the country code out of this.
					var code = new CountryCode(
						translation,
						alpha2,
						alpha3,
						numeric);

					codes.Add(code);
				}
			}
		}

		#endregion
	}
}
