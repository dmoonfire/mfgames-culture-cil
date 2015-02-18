// <copyright file="CountryCode.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Translations;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// An immutable identifier for an ISO 3166 country code including support for 
	/// unknown countries.
	/// </summary>
	public class CountryCode
	{
		#region Constructors and Destructors

		public CountryCode(
			ITranslationCollection names,
			string alpha2,
			string alpha3,
			short? numeric)
		{
			Names = names;
			Alpha2 = alpha2;
			Alpha3 = alpha3;
			Numeric = numeric;
		}

		#endregion

		#region Public Properties

		public string Alpha2 { get; private set; }
		public string Alpha3 { get; private set; }
		public ITranslationCollection Names { get; private set; }
		public short? Numeric { get; private set; }

		#endregion

		#region Public Methods and Operators

		public override string ToString()
		{
			string name = Names.GetFallback();

			return string.Format(
				"CountryCode({0}, {1})",
				Alpha3,
				name ?? "<Missing>");
		}

		#endregion
	}
}
