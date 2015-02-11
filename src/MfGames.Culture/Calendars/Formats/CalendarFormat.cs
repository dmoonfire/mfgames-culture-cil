// <copyright file="CalendarFormat.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

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

		public CalendarFormat(string format)
		{
			// We use the basics of the macro expansion to pull out the
			// segments. Then, we relace the variable ones with a
			// format-specific version.
			ParseFormat(format);
		}

		#endregion

		#region Public Methods and Operators

		public CalendarPoint Parse(string input, CalendarSystem calendar)
		{
			// First get the values for the input.
			CalendarElementValueCollection values = ParseValues(input);

			// Create a new calendar point from a given value.
			CalendarPoint results = calendar.Create(values);

			return results;
		}

		public CalendarElementValueCollection ParseValues(string input)
		{
			Dictionary<string, string> parsed = macro.Parse(input);
			var values = new CalendarElementValueCollection();

			foreach (KeyValuePair<string, string> pair in parsed)
			{
				values[pair.Key] = Convert.ToInt32(pair.Value);
			}

			return values;
		}

		public string ToString(CalendarPoint point)
		{
			string results = macro.Expand(point.Values);
			return results;
		}

		#endregion

		#region Methods

		private void ParseFormat(string format)
		{
			// Set up the initial macro and use that to parse the segments
			// out of it.
			IMacroExpansionSegment[] segments = MacroExpansion.ParseSegments(format);

			// Loop through the segments and build up a new list with a format-
			// specific segment for the variables.
			var newSegments = new List<IMacroExpansionSegment>();

			foreach (IMacroExpansionSegment oldSegment in segments)
			{
				// If this is a variable segment, then we wrap it.
				IMacroExpansionSegment segment = oldSegment;
				var variable = segment as VariableMacroExpansionSegment;

				if (variable != null)
				{
					segment = new CalendarFormatMacroExpansionSegment(variable);
				}

				// If we got this far, we don't know what to do with it.
				newSegments.Add(segment);
			}

			// Create a new macro expansion with the new format.
			macro = new MacroExpansion(newSegments);
		}

		#endregion
	}
}
