// <copyright file="CalendarFormat.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Text;

namespace MfGames.Culture.Calendars.Formats
{
	/// <summary>
	/// Represents a single format of one or more calendars and provides
	/// functionality for formatting a CalendarPoint into the given value or
	/// parsing it to generate a resulting point.
	/// </summary>
	public class CalendarFormat
	{
		#region Fields

		private MacroExpansion macro;

		#endregion

		#region Constructors and Destructors

		public CalendarFormat(string macroFormat)
			: this(new MacroExpansion(macroFormat))
		{
		}

		public CalendarFormat(MacroExpansion macro)
		{
		}

		#endregion
	}
}
