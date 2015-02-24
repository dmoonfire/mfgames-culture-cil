// <copyright file="ScriptCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using CsvHelper;
using CsvHelper.Configuration;

using MfGames.Culture.Translations;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>ScriptCode</c>
	/// is created for a given script.
	/// </summary>
	public class ScriptCodeManager : IScriptCodeManager
	{
		#region Fields

		private readonly HashSet<ScriptCode> codes;

		#endregion

		#region Constructors and Destructors

		public ScriptCodeManager()
		{
			codes = new HashSet<ScriptCode>();
		}

		#endregion

		#region Public Methods and Operators

		public void AddDefaults(ILanguageCodeManager languages)
		{
			// Set up the other translations.
			LanguageCode frenchLanguage = languages.GetIsoAlpha2("fr");
			var frenchTag = new LanguageTag(frenchLanguage);

			// Load the defaults from an embedded resource.
			Assembly assembly = GetType().Assembly;
			const string Name = "MfGames.Culture.Codes.iso15924-utf8-20141115.txt";
			var csvOptions = new CsvConfiguration
			{
				AllowComments = true,
				Delimiter = ";"
			};

			using (Stream stream = assembly.GetManifestResourceStream(Name))
			using (var reader = new StreamReader(stream))
			using (var csv = new CsvReader(reader, csvOptions))
			{
				// Loop through the input file.
				while (csv.Read())
				{
					// Pull out the fields from the row.
					var alpha4 = csv.GetField<string>("Code");
					var numericString = csv.GetField<string>("Numeric");
					var english = csv.GetField<string>("English");
					var french = csv.GetField<string>("French");

					// Convert the numeric to an int?.
					short? numeric = numericString == null
						? (short?)null
						: short.Parse(numericString);

					// Create the translations for this script.
					var translation = new MemoryTranslationCollection();

					translation.Add(LanguageTag.Canonical, english);
					translation.Add(frenchTag, french);

					// Create the script code out of this.
					var code = new ScriptCode(
						translation,
						alpha4,
						numeric);

					codes.Add(code);
				}
			}
		}

		public ScriptCode Get(string scriptCode)
		{
			return codes.FirstOrDefault(code => code.Equals(scriptCode));
		}

		#endregion
	}
}
