// <copyright file="ITranslationManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	/// <summary>
	/// Describes the interface for something that can set or add translations.
	/// </summary>
	public interface ITranslationManager : ITranslationProvider
	{
		#region Public Methods and Operators

		void Add(
			string key,
			LanguageTag languageTag,
			string translation);

		#endregion
	}
}
