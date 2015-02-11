// <copyright file="CalendarFormatCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

namespace MfGames.Culture.Calendars.Formats
{
	public class CalendarFormatCollection
	{
		#region Fields

		private readonly List<CalendarFormat> formats;

		#endregion

		#region Constructors and Destructors

		public CalendarFormatCollection()
		{
			formats = new List<CalendarFormat>();
		}

		#endregion

		#region Public Methods and Operators

		public void Add(string name, string format)
		{
			var calendarFormat = new CalendarFormat(format);

			Add(name, calendarFormat);
		}

		public void Add(string name, CalendarFormat format)
		{
			formats.Add(format);
		}

		#endregion
	}
}
