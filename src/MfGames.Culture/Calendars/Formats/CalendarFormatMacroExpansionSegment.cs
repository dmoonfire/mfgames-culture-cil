// <copyright file="CalendarFormatMacroExpansionSegment.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Text.RegularExpressions;

using MfGames.Culture.Translations;
using MfGames.HierarchicalPaths;
using MfGames.Text;

namespace MfGames.Culture.Calendars.Formats
{
	public class CalendarFormatMacroExpansionSegment : IMacroExpansionSegment
	{
		#region Constructors and Destructors

		public CalendarFormatMacroExpansionSegment(
			VariableMacroExpansionSegment segment)
		{
			// Verify our contracts.
			if (segment == null)
			{
				throw new ArgumentNullException("segment");
			}

			// Save the components.
			Field = segment.Field;
			Format = segment.Format;
			MacroIndex = segment.MacroIndex;

			// If we have a "/" in the format, then we are going to be doing
			// a translation lookup.
			if (Format.Contains("/"))
			{
				// Pull out the path, but strip the "/" because we are going
				// to use relative translations.
				int index = Format.IndexOf("/", StringComparison.InvariantCulture);
				string path = Format.Substring(index + 1);
				TranslationLookup = path;

				// Update the format so it only has the "S" code.
				Format = Format.Substring(0, index);
			}

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
		public string TranslationLookup { get; set; }

		#endregion

		#region Public Methods and Operators

		public string Expand(IMacroExpansionContext context)
		{
			// Get the value from inside the macro.
			var formatContext = (CalendarFormatMacroContext)context;
			int value = formatContext.ElementValues[Field] + Offset;

			// If the format is a string, then use that for the lookup key.
			if (Format.StartsWith("S"))
			{
				string key = value.ToString();
				TranslationResult result = formatContext
					.Context
					.Translations
					.GetTranslationResult(key, formatContext.Context.Selector);

				return result.Result;
			}

			// Format it.
			return value.ToString(Format);
		}

		public string GetRegex()
		{
			return Pattern;
		}

		public void Match(IMacroExpansionContext context, Match match)
		{
			// Cast our context to the custom one.
			var formatContext = (CalendarFormatMacroContext)context;

			// Try to parse the value as an integer. If we can, then apply the
			// offset to normalize the values.
			int index;
			string value = match.Groups[MacroIndex].Value;

			// Check to see if we have a character lookup. If we do, we need
			// to translate it into a numeric value.
			if (TranslationLookup != null)
			{
				// Look up the translation.
				string key = value.ToLowerInvariant();
				TranslationResult translation = formatContext
					.Context
					.Translations
					.GetTranslationResult(key, formatContext.Context.Selector);

				if (translation == null)
				{
					throw new IndexOutOfRangeException(
						"Cannot find translation for: " + value + ".");
				}

				// Make sure we can parse it.
				if (!Int32.TryParse(translation.Result, out index))
				{
					throw new InvalidOperationException(
						"Cannot parse value as a numeric: " + translation.Result);
				}

				formatContext.ElementValues[Field] = index;
				formatContext.Values[Field] = value;
				return;
			}

			// If this is a number, we parse that.
			if (Int32.TryParse(value, out index))
			{
				int result = index - Offset;
				formatContext.ElementValues[Field] = result;
				value = result.ToString();
			}

			// Save the value in the results hash.
			context.Values[Field] = value;
		}

		#endregion
	}
}
