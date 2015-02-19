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

		public string Format(string formatName, CalendarPoint point)
		{
			return Format(formatName, LanguageTagSelector.All, point);
		}

		public string Format(
			string formatName,
			LanguageTagSelector selector,
			CalendarPoint point)
		{
			CalendarFormat format = GetFormat(formatName);

			return format.Format(Calendar, Calendar.Translations, selector, point);
		}

		public bool IsConflict(string input)
		{
			// Go through the formats and find all of them that apply for
			// the input. Once we have that, find out if they apply to the
			// same values. If they do and they have different field keys,
			// then there is a potential conflict.
			int count = formats
				.Where(f => f.Format.IsMatch(input))
				.Select(f => f.Format.GetMatchKey())
				.OrderBy(r => r)
				.Distinct()
				.Count();

			return count > 1;
		}

		public CalendarPoint Parse(
			LanguageTagSelector selector,
			string formatName,
			string input)
		{
			CalendarFormat format = GetFormat(formatName);
			var context = new CalendarFormatContext(
				Calendar,
				Calendar.Translations,
				selector);
			CalendarPoint results = format.Parse(context, input);
			return results;
		}

		public CalendarPoint Parse(LanguageTagSelector selector, string input)
		{
			// If we have a conflict, throw an exception.
			if (IsConflict(input))
			{
				throw new InvalidOperationException(
					"There are multiple calendar formats that " +
						"apply to the given input.");
			}

			// Grab the first one that applies.
			CalendarFormat format = formats
				.Where(f => f.Format.IsMatch(input))
				.Select(f => f.Format)
				.First();

			// Format using this one.
			CalendarPoint results = format.Parse(
				Calendar,
				Calendar.Translations,
				selector,
				input);

			return results;
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
