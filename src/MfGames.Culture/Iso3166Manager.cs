// <copyright file="Iso3166Manager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

namespace MfGames.Culture
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>Iso3166Code</c>
	/// is created for a given country.
	/// </summary>
	public class Iso3166Manager
	{
		#region Static Fields

		private static Iso3166Manager instance;

		#endregion

		#region Constructors and Destructors

		static Iso3166Manager()
		{
			instance = new Iso3166Manager();
			instance.CreateDefaults();
		}

		#endregion

		#region Public Properties

		public static Iso3166Manager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Iso3166Manager.Instance cannot be assigned a null value.");
				}

				instance = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void CreateDefaults()
		{
		}

		#endregion
	}
}
