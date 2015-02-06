// <copyright file="CountryCodeManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>CountryCode</c>
	/// is created for a given country.
	/// </summary>
	public class CountryCodeManager
	{
		#region Static Fields

		private static CountryCodeManager instance;

		#endregion

		#region Constructors and Destructors

		static CountryCodeManager()
		{
			instance = new CountryCodeManager();
			instance.AddDefaults();
		}

		#endregion

		#region Public Properties

		public static CountryCodeManager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"CountryCodeManager.Instance cannot be assigned a null value.");
				}

				instance = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void AddDefaults()
		{
		}

		#endregion
	}
}
