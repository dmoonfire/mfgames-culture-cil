// <copyright file="CalendarFormatCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Calendars.Formats
{
	public class CalendarFormatCollection
	{
		#region Fields

		private readonly List<CalendarFormat> formats;

		#endregion

		#region Constructors and Destructors

		public CalendarFormatCollection(ICalendarSystem calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}

			Calendar = calendar;
			formats = new List<CalendarFormat>();
		}

		#endregion

		#region Public Properties

		public ICalendarSystem Calendar { get; set; }

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

		public string ToString(CalendarPoint point, LanguageTagSelector selector)
		{
			return formats[0].ToString(point, Calendar, Calendar.Translations, selector);
		}

		#endregion
	}
}
