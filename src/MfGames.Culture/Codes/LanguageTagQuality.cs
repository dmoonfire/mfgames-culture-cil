// <copyright file="LanguageTagQuality.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Text.RegularExpressions;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// An immutable pairing of a specific language tag along with a quality
	/// level to indicate preference.
	/// </summary>
	/// <remarks>
	/// This is based on the HTTP Accept-Language header.
	/// </remarks>
	public class LanguageTagQuality : IComparable<LanguageTagQuality>
	{
		#region Static Fields

		private static readonly Regex pattern;

		#endregion

		#region Constructors and Destructors

		static LanguageTagQuality()
		{
			pattern = new Regex(@"\s*(\*|[\w-]+)(?:;q=(\d*\.\d*))?$");
		}

		public LanguageTagQuality(LanguageTag languageTag, float quality = 1.0f)
		{
			// Assert our contracts.
			if (languageTag == null)
			{
				throw new ArgumentNullException(
					"languageTag",
					"LanguageTag cannot be null.");
			}

			if (quality < 0)
			{
				throw new ArgumentOutOfRangeException(
					"quality",
					"Quality cannot be less than zero.");
			}

			// Save the member variables.
			LanguageTag = languageTag;
			Quality = quality;
		}

		public LanguageTagQuality(string selector)
			: this(CodeManager.Instance, selector)
		{
		}

		public LanguageTagQuality(CodeManager codes, string selector)
		{
			// Establish our contracts.
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}

			// See if it matches our pattern.
			Match match = pattern.Match(selector);

			if (!match.Success)
			{
				throw new ArgumentException(
					"Cannot parse language selector: " + selector + ".",
					"selector");
			}

			// Figure out the language.
			LanguageTag = new LanguageTag(codes, match.Groups[1].Value);

			// Parse the quality, defaulting to one if we don't have it.
			float quality;

			if (!Single.TryParse(match.Groups[2].Value, out quality))
			{
				quality = 1.0f;
			}

			Quality = quality;
		}

		#endregion

		#region Public Properties

		public LanguageTag LanguageTag { get; private set; }
		public float Quality { get; private set; }

		#endregion

		#region Public Methods and Operators

		public int CompareTo(LanguageTagQuality other)
		{
			// Sort quality from high to low.
			int results = other.Quality.CompareTo(Quality);

			if (results != 0)
			{
				return results;
			}

			// Otherwise, just use another qualifier.
			return LanguageTag.CompareTo(other.LanguageTag);
		}

		public override string ToString()
		{
			return string.Format("{0};q={1:G1}", LanguageTag, Quality);
		}

		#endregion
	}
}
