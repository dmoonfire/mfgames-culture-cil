// <copyright file="TranslationManager.cs" company="Moonfire Games">
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
	public class TranslationManager : List<ITranslationProvider>
	{
		#region Static Fields

		private static TranslationManager instance;

		#endregion

		#region Constructors and Destructors

		static TranslationManager()
		{
			instance = new TranslationManager();
		}

		#endregion

		#region Public Properties

		public static TranslationManager Instance
		{
			get { return instance; }
			set { instance = value ?? new TranslationManager(); }
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Retrieves a translation for the given selector and path. If one
		/// cannot be found, then it returns the fallback value.
		/// </summary>
		/// <param name="selector">The langauges to accept and their weights.</param>
		/// <param name="path">The identifier to the translation.</param>
		/// <param name="fallback">The fallback if a translation cannot be found.</param>
		/// <returns>A translated value or null.</returns>
		public string GetTranslation(
			LanguageTagSelector selector,
			HierarchicalPath path,
			string fallback = null)
		{
			// Start with the fallback as the "best".
			LanguageTag fallbackLanguage = LanguageTag.All;
			var fallbackQuality = new LanguageTagQuality(fallbackLanguage, 0);
			var result = new TranslationResult(
				fallbackQuality,
				fallback);

			// Loop through the translations and try to find a better one.
			foreach (ITranslationProvider provider in this)
			{
				TranslationResult providerResult = provider.GetTranslation(
					selector,
					path);

				if (providerResult != null && providerResult > result)
				{
					result = providerResult;
				}
			}

			// Whatever we have at the end is it.
			return result.Result;
		}

		#endregion
	}
}
