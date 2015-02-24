// <copyright file="CalendarFormatContext.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;
using MfGames.Culture.Translations;

namespace MfGames.Culture.Calendars.Formats
{
	/// <summary>
	/// Contains the context for parsing and formatting calendar points.
	/// </summary>
	public class CalendarFormatContext
	{
		#region Constructors and Destructors

		public CalendarFormatContext(
			ICalendarSystem calendar,
			ITranslationProvider translations,
			LanguageTagSelector selector)
		{
			Calendar = calendar;
			Translations = translations;
			Selector = selector;
		}

		#endregion

		#region Public Properties

		public ICalendarSystem Calendar { get; set; }
		public LanguageTagSelector Selector { get; set; }
		public ITranslationProvider Translations { get; set; }

		#endregion

		#region Public Methods and Operators

		public CalendarFormatMacroContext CreateMacroContext()
		{
			var macroContext = new CalendarFormatMacroContext(
				this,
				new CalendarElementValueCollection());

			return macroContext;
		}

		public CalendarFormatMacroContext CreateMacroContext(CalendarPoint point)
		{
			var macroContext = new CalendarFormatMacroContext(
				this,
				point.Values);

			return macroContext;
		}

		#endregion
	}
}
