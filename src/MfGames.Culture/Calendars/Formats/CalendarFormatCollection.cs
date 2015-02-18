// <copyright file="CalendarFormatCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Linq;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Calendars.Formats
{
	public class CalendarFormatCollection
	{
		#region Fields

		private readonly List<CalendarFormatEntry> formats;

		#endregion

		#region Constructors and Destructors

		public CalendarFormatCollection(ICalendarSystem calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}

			Calendar = calendar;
			formats = new List<CalendarFormatEntry>();
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
			var entry = new CalendarFormatEntry(name, format);

			formats.Add(entry);
		}

		public string ToString(string formatName, CalendarPoint point)
		{
			return ToString(formatName, LanguageTagSelector.All, point);
		}

		public string ToString(
			string formatName,
			LanguageTagSelector selector,
			CalendarPoint point)
		{
			CalendarFormat format = GetFormat(formatName);

			return format.ToString(Calendar, Calendar.Translations, selector, point);
		}

		#endregion

		#region Methods

		private CalendarFormat GetFormat(string formatName)
		{
			return formats.First(e => e.Name == formatName).Format;
		}

		#endregion

		private class CalendarFormatEntry
		{
			#region Constructors and Destructors

			public CalendarFormatEntry(string name, CalendarFormat format)
			{
				Name = name;
				Format = format;
			}

			#endregion

			#region Public Properties

			public CalendarFormat Format { get; set; }
			public string Name { get; set; }

			#endregion
		}
	}
}
