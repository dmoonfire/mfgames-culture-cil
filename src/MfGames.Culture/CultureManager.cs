// <copyright file="CultureManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using MfGames.Culture.Calendars;

namespace MfGames.Culture
{
	/// <summary>
	/// Defines a common place to retrieve known cultures, calendars, and other elements.
	/// </summary>
	public class CultureManager
	{
		#region Static Fields

		private static CultureManager instance;

		#endregion

		#region Constructors and Destructors

		static CultureManager()
		{
			instance = new CultureManager();
			instance.AddDefaults();
		}

		public CultureManager()
		{
			Calendars = new ConcurrentDictionary<string, CalendarSystem>();
		}

		#endregion

		#region Public Properties

		public static CultureManager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"CultureManager.Instance cannot be assigned a null value.");
				}

				instance = value;
			}
		}

		public IDictionary<string, CalendarSystem> Calendars { get; private set; }

		#endregion

		#region Public Methods and Operators

		public void AddDefaults()
		{
			// Insert in the known calendars.
			Calendars["Gregorian"] = new GregorianCalendarSystem();
			Calendars["Duodecimal"] = new DuodecimalCalendarSystem();
		}

		#endregion
	}
}
