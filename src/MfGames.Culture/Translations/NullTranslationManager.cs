// <copyright file="NullTranslationManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public class NullTranslationManager : ITranslationManager
	{
		#region Public Methods and Operators

		public void Add(string key, LanguageTag languageTag, string translation)
		{
		}

		public TranslationResult GetTranslationResult(
			string key,
			LanguageTagSelector selector)
		{
			return null;
		}

		#endregion
	}
}
