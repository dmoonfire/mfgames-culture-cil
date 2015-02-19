// <copyright file="CultureSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Formats;
using MfGames.Culture.Codes;

namespace MfGames.Culture
{
	public class CultureSystem
	{
		#region Constructors and Destructors

		public CultureSystem()
		{
			Calendar = new CompositeCalendarSystem();
			Formats = new CalendarFormatCollection(Calendar);
			Selector = LanguageTagSelector.All;
		}

		#endregion

		#region Public Properties

		public ICalendarSystem Calendar { get; set; }
		public CalendarFormatCollection Formats { get; private set; }
		public LanguageTagSelector Selector { get; set; }

		#endregion

		#region Public Methods and Operators

		public string FormatCalendarPoint(string formatName, CalendarPoint point)
		{
			string results = Formats.Format(formatName, Selector, point);
			return results;
		}

		public CalendarPoint ParseCalendarPoint(string input)
		{
			CalendarPoint results = Formats.Parse(Selector, input);
			return results;
		}

		public CalendarPoint ParseCalendarPoint(string formatName, string input)
		{
			CalendarPoint results = Formats.Parse(Selector, formatName, input);
			return results;
		}

		#endregion
	}
}
