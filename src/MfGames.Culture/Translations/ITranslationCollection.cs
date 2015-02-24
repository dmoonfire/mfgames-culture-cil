// <copyright file="ITranslationCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public interface ITranslationCollection
	{
		#region Public Methods and Operators

		TranslationResult GetTranslation(LanguageTagSelector selector);

		#endregion
	}
}
