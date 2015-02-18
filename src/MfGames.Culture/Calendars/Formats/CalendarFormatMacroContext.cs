// <copyright file="CalendarFormatMacroContext.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Text;

namespace MfGames.Culture.Calendars.Formats
{
	public class CalendarFormatMacroContext : MacroExpansionContext
	{
		#region Constructors and Destructors

		public CalendarFormatMacroContext(
			CalendarFormatContext context,
			CalendarElementValueCollection values)
		{
			Context = context;
			ElementValues = values;
		}

		#endregion

		#region Public Properties

		public CalendarFormatContext Context { get; set; }
		public CalendarElementValueCollection ElementValues { get; private set; }

		#endregion
	}
}
