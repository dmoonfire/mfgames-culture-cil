﻿// <copyright file="ILanguageCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

using MfGames.Culture.Translations;

namespace MfGames.Culture.Codes
{
	public interface ILanguageCodeManager : IEnumerable<LanguageCode>,
		ITranslationProvider
	{
		#region Public Properties

		int Count { get; }

		#endregion

		#region Public Methods and Operators

		void Add(LanguageCode languageCode);

		void Add(string isoAlpha3T);

		void AddDefaults();

		LanguageCode Get(string language);

		LanguageCode GetIsoAlpha3(string alpha3);

		LanguageCode GetIsoAlpha3B(string alpha3);

		LanguageCode GetIsoAlpha3T(string alpha3);

		#endregion
	}
}