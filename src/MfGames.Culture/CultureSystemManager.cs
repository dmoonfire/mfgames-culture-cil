// <copyright file="CultureSystemManager.cs" company="Moonfire Games">
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
	public class CultureSystemManager
	{
		#region Static Fields

		private static CultureSystemManager instance;

		#endregion

		#region Constructors and Destructors

		static CultureSystemManager()
		{
			instance = new CultureSystemManager();
			instance.AddDefaults();
		}

		public CultureSystemManager()
		{
			Calendars = new ConcurrentDictionary<string, CalendarSystem>();
		}

		#endregion

		#region Public Properties

		public static CultureSystemManager Instance
		{
			get { return instance; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"CultureSystemManager.Instance cannot be assigned a null value.");
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
