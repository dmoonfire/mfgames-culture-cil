// <copyright file="Culture.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Calendars;

namespace MfGames.Culture
{
	public class Culture
	{
		#region Constructors and Destructors

		public Culture()
		{
			Calendars = new CalendarSystemCollection();
		}

		#endregion

		#region Public Properties

		public CalendarSystemCollection Calendars { get; private set; }

		#endregion
	}
}
