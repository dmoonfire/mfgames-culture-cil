// <copyright file="CalendarFormatMacroContext.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Codes;
using MfGames.Culture.Translations;
using MfGames.Text;

namespace MfGames.Culture.Calendars.Formats
{
	public class CalendarFormatMacroContext : MacroExpansionContext
	{
		#region Constructors and Destructors

		public CalendarFormatMacroContext()
		{
			ElementValues = new CalendarElementValueCollection();
		}

		public CalendarFormatMacroContext(CalendarElementValueCollection values)
		{
			ElementValues = values;
		}

		public CalendarFormatMacroContext(
			CalendarSystem calendar,
			ITranslationProvider translations,
			LanguageTagSelector selector)
			: this()
		{
			Calendar = calendar;
			Translations = translations;
			Selector = selector;
		}

		#endregion

		#region Public Properties

		public ICalendarSystem Calendar { get; set; }
		public CalendarElementValueCollection ElementValues { get; private set; }
		public LanguageTagSelector Selector { get; set; }
		public ITranslationProvider Translations { get; set; }

		#endregion
	}
}
