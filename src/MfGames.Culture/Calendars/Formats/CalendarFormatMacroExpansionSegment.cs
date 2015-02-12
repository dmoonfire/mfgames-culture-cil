// <copyright file="CalendarFormatMacroExpansionSegment.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MfGames.Text;

namespace MfGames.Culture.Calendars.Formats
{
	public class CalendarFormatMacroExpansionSegment : IMacroExpansionSegment
	{
		#region Constructors and Destructors

		public CalendarFormatMacroExpansionSegment(
			VariableMacroExpansionSegment segment)
		{
			// Save the components.
			Field = segment.Field;
			Format = segment.Format;
			MacroIndex = segment.MacroIndex;

			// If there is a "+1" at the end of the format, then change the
			// offset to match.
			if (Format.EndsWith("+1"))
			{
				Format = Format.Replace("+1", "");
				Offset = 1;
			}

			// Save the pattern.
			Pattern = VariableMacroExpansionSegment.GetRegex(Format);
		}

		#endregion

		#region Public Properties

		public string Field { get; set; }
		public string Format { get; set; }
		public int MacroIndex { get; set; }
		public int Offset { get; set; }
		public string Pattern { get; set; }

		#endregion

		#region Public Methods and Operators

		public string Expand(IDictionary<string, object> macros)
		{
			// Get the value from inside the macro.
			int value = (int)macros[Field] + Offset;

			// Format it.
			return value.ToString(Format);
		}

		public string GetRegex()
		{
			return Pattern;
		}

		public void Match(Dictionary<string, string> results, Match match)
		{
			// Try to parse the value as an integer. If we can, then apply the
			// offset to normalize the values.
			int index;
			string value = match.Groups[MacroIndex].Value;

			if (Int32.TryParse(value, out index))
			{
				value = (index - Offset).ToString();
			}

			// Save the value in the results hash.
			results[Field] = value;
		}

		#endregion
	}
}
