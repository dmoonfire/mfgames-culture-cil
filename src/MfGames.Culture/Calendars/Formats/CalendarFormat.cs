﻿// <copyright file="CalendarFormat.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;
using System.Linq;

using MfGames.Culture.Codes;
using MfGames.Culture.Translations;
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

		public string Format(
			ICalendarSystem calendar,
			ITranslationProvider translations,
			LanguageTagSelector selector,
			CalendarPoint point)
		{
			var context = new CalendarFormatContext(
				calendar,
				translations,
				selector);

			return Format(context, point);
		}

		public string Format(
			CalendarFormatContext context,
			CalendarPoint point)
		{
			string results = macro.Expand(context.CreateMacroContext(point));

			return results;
		}

		public string GetMatchKey()
		{
			string results = string.Join(
				":",
				macro
					.Segments
					.OfType<CalendarFormatMacroExpansionSegment>()
					.Select(s => s.Field)
					.ToArray());
			return results;
		}

		public bool IsMatch(string input)
		{
			bool results = macro.GetRegex().IsMatch(input);
			return results;
		}

		public CalendarPoint Parse(
			ICalendarSystem calendar,
			ITranslationProvider translations,
			LanguageTagSelector selector,
			string input)
		{
			var context = new CalendarFormatContext(
				calendar,
				translations,
				selector);

			return Parse(context, input);
		}

		public CalendarPoint Parse(
			CalendarFormatContext context,
			string input)
		{
			// First get the values for the input.
			CalendarElementValueCollection values = ParseValues(
				context,
				input);

			// Create a new calendar point from a given value.
			CalendarPoint results = context.Calendar.Create(values);

			return results;
		}

		public CalendarElementValueCollection ParseValues(
			ICalendarSystem calendar,
			ITranslationProvider translations,
			LanguageTagSelector selector,
			string input)
		{
			var context = new CalendarFormatContext(
				calendar,
				translations,
				selector);

			return ParseValues(context, input);
		}

		public CalendarElementValueCollection ParseValues(
			CalendarFormatContext context,
			string input)
		{
			CalendarFormatMacroContext macroContext = context.CreateMacroContext();

			macro.Parse(macroContext, input);

			return macroContext.ElementValues;
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
