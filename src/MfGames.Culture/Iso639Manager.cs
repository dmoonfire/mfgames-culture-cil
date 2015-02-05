// <copyright file="Iso639Manager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

namespace MfGames.Culture
{
	/// <summary>
	/// A manager class which ensures only a single instance of a <c>Iso639Code</c>
	/// is created for a given language.
	/// </summary>
	public class Iso639Manager
	{
		#region Static Fields

		private static Iso639Manager instance;

		#endregion

		#region Constructors and Destructors

		static Iso639Manager()
		{
			instance = new Iso639Manager();
			instance.CreateDefaults();
		}

		#endregion

		#region Public Properties

		public static Iso639Manager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Iso639Manager.Instance cannot be assigned a null value.");
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
