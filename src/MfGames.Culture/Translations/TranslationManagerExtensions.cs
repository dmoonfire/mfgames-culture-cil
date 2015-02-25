// <copyright file="TranslationManagerExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Linq;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public static class TranslationManagerExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Adds a list of translations to the collection using the root key
		/// and with a subkey of the zero-based index. The format must include
		/// a "{0}" for the numerical index.
		/// </summary>
		public static void AddRange(
			this ITranslationManager translations,
			string format,
			LanguageTag languageTag,
			params string[] names)
		{
			// Check our constraints.
			if (!format.Contains("{0}"))
			{
				throw new ArgumentException(
					"Format string must have a '{0}' somewhere in its contents.",
					"format");
			}

			// Loop through all the names and add the translation for each one.
			for (var i = 0; i < names.Length; i++)
			{
				string key = string.Format(format, i);

				translations.Add(key, languageTag, names[i]);
			}
		}

		/// <summary>
		/// Adds a list of translations to the collection using the root key
		/// and with a subkey of the zero-based index.
		/// </summary>
		public static void AddRange(
			this ITranslationManager translations,
			string format,
			LanguageTag languageTag,
			IEnumerable<string> names)
		{
			AddRange(translations, format, languageTag, names.ToArray());
		}

		#endregion
	}
}
