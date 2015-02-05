// <copyright file="CultureManager.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

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
			// Create a default instance.
			instance = new CultureManager();

			// Insert in the known calendars.
			instance.Calendars["Gregorian"] = new GregorianCalendarSystem();
			instance.Calendars["Duodecimal"] = new DuodecimalCalendarSystem();
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
				if (value != null)
				{
					instance = value;
				}
			}
		}

		public IDictionary<string, CalendarSystem> Calendars { get; private set; }

		#endregion
	}
}
