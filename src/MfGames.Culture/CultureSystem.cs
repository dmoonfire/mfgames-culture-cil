// <copyright file="CultureSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Formats;

namespace MfGames.Culture
{
	public class CultureSystem
	{
		#region Constructors and Destructors

		public CultureSystem()
		{
			Calendars = new CalendarSystemCollection();
			Formats = new CalendarFormatCollection();
		}

		#endregion

		#region Public Properties

		public CalendarSystemCollection Calendars { get; private set; }
		public CalendarFormatCollection Formats { get; private set; }

		#endregion
	}
}
