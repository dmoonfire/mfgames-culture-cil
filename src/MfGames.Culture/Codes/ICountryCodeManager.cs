// <copyright file="ICountryCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Culture.Codes
{
	public interface ICountryCodeManager
	{
		#region Public Methods and Operators

		void AddDefaults(ILanguageCodeManager languages);

		CountryCode Get(string countryCode);

		#endregion
	}
}
