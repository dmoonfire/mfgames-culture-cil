// <copyright file="CodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using MfGames.Culture.Translations;

namespace MfGames.Culture.Codes
{
	public class CodeManager
	{
		#region Static Fields

		private static CodeManager instance;

		#endregion

		#region Fields

		private ICountryCodeManager countries;

		private ILanguageCodeManager languages;

		private ScriptCodeManager scripts;

		private ITranslationManager translations;

		#endregion

		#region Constructors and Destructors

		static CodeManager()
		{
			// Create the default code manager.
			instance = new CodeManager
			{
				Languages = new LanguageCodeManager(),
				Countries = new CountryCodeManager(),
				Scripts = new ScriptCodeManager(),
				Translations = new MemoryTranslationProvider()
			};

			// Add in the default values.
			instance.Languages.AddDefaults(instance.Translations);
			instance.Countries.AddDefaults(instance.Languages);
			instance.Scripts.AddDefaults(instance.Languages);
		}

		#endregion

		#region Public Properties

		public static CodeManager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"CodeManager.Instance cannot be assigned a null value.");
				}

				instance = value;
			}
		}

		public ICountryCodeManager Countries
		{
			get { return countries; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Countries cannot be assigned a null value.");
				}

				countries = value;
			}
		}

		public ILanguageCodeManager Languages
		{
			get { return languages; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Languages cannot be assigned a null value.");
				}

				languages = value;
			}
		}

		public ScriptCodeManager Scripts
		{
			get { return scripts; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Scripts cannot be assigned a null value.");
				}

				scripts = value;
			}
		}

		public ITranslationManager Translations
		{
			get { return translations; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Translations cannot be assigned a null value.");
				}

				translations = value;
			}
		}

		#endregion
	}
}
