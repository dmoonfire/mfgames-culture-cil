// <copyright file="CultureSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Formats;
using MfGames.Culture.Codes;
using MfGames.Culture.Translations;

namespace MfGames.Culture
{
	public class CultureSystem
	{
		#region Fields

		private ITranslationManager translations;

		#endregion

		#region Constructors and Destructors

		public CultureSystem()
		{
			Calendar = new CompositeCalendarSystem();
			Translations = new MemoryTranslationManager();
			Formats = new CalendarFormatCollection(Calendar, Translations);
			Selector = LanguageTagSelector.Canonical;
		}

		#endregion

		#region Public Properties

		public CompositeCalendarSystem Calendar { get; private set; }
		public CalendarFormatCollection Formats { get; private set; }
		public LanguageTagSelector Selector { get; set; }

		public ITranslationManager Translations
		{
			get { return translations; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(
						"value",
						"Cannot assign a null translation.");
				}

				translations = value;
				Formats.Translations = value;
			}
		}

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
