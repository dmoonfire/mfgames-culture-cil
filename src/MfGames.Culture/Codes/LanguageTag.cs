// <copyright file="LanguageTag.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// An immutable identifier for an IEFT language tag including the ISO 639 language
	/// code and optional components for country, dialect, and sub-tags.
	/// </summary>
	public class LanguageTag : IEquatable<LanguageTag>, IComparable<LanguageTag>
	{
		#region Fields

		private List<string> privateUseTags;

		#endregion

		#region Constructors and Destructors

		static LanguageTag()
		{
			Canonical = new LanguageTag(LanguageCode.Canonical);
		}

		public LanguageTag(LanguageCode language)
		{
			// Establishg our contracts.
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}

			// Save the member variables.
			Language = language;
		}

		public LanguageTag(string language)
			: this(CodeManager.Instance, language)
		{
		}

		public LanguageTag(CodeManager codes, string tag)
		{
			// Establish our contracts.
			if (codes == null)
			{
				throw new ArgumentNullException("codes");
			}

			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}

			if (string.IsNullOrWhiteSpace(tag))
			{
				throw new ArgumentException("Language tag cannot be a blank string.", "tag");
			}

			// Try to parse the language tag out into its components. This can
			// be a relatively complex process, which depends on a left to
			// right order for operation.
			string[] parts = tag.Split('-');

			// From the beginning, we have either a language code or an private
			// use extension. The private use will consume everything when it
			// parses, leaving nothing else. According to the specification,
			// all codes are case insensitive.
			var index = 0;

			// We check the extension first, then go through the tag ending with
			// another extension.
			ParsePrivateUse(parts, ref index);
			ParseLanguage(codes, parts, ref index);
			ParseCountry(codes, parts, ref index);
			ParsePrivateUse(parts, ref index);
		}

		#endregion

		#region Public Properties

		public static LanguageTag Canonical { get; private set; }
		public CountryCode Country { get; private set; }

		/// <summary>
		/// Gets the ISO 639 description of the language.
		/// </summary>
		/// <value>
		/// The language object.
		/// </value>
		public LanguageCode Language { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static bool operator ==(LanguageTag left, LanguageTag right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(LanguageTag left, LanguageTag right)
		{
			return !Equals(left, right);
		}

		public int CompareTo(LanguageTag other)
		{
			return ToString().CompareTo(other.ToString());
		}

		public bool Equals(LanguageTag other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Language.Equals(other.Language);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != GetType())
			{
				return false;
			}

			return Equals((LanguageTag)obj);
		}

		public override int GetHashCode()
		{
			return Language.GetHashCode();
		}

		public override string ToString()
		{
			return Language.IsoAlpha3;
		}

		#endregion

		#region Methods

		private void ParseCountry(CodeManager codes, string[] parts, ref int index)
		{
			// Make sure we aren't at the end of the string.
			if (index >= parts.Length)
			{
				return;
			}

			// We only allow two and three-character countries and regions.
			string country = parts[index];

			if (country.Length != 2 && country.Length != 3)
			{
				return;
			}

			// Try to look up the country.
			Country = codes.Countries.Get(country);

			if (Country == null)
			{
				throw new InvalidOperationException(
					"Found an invalid country code: " + country + ".");
			}

			// Increment the index to move to the next part.
			index++;
		}

		private void ParseLanguage(CodeManager codes, string[] parts, ref int index)
		{
			// Make sure we aren't at the end of the string.
			if (index >= parts.Length)
			{
				return;
			}

			// Grab the first element as a language.
			string language = parts[index];

			Language = codes.Languages.Get(language);

			if (Language == null)
			{
				throw new InvalidOperationException(
					"Cannot create a language tag without a language code.");
			}

			// Increment the index to move to the next part.
			index++;
		}

		private void ParsePrivateUse(string[] parts, ref int index)
		{
			// Make sure we aren't at the end of the string.
			if (index >= parts.Length)
			{
				return;
			}

			// If the current code is an "x", then the rest of the tag
			// is private use.
			if (!String.Equals(parts[0], "X", StringComparison.CurrentCultureIgnoreCase))
			{
				return;
			}

			// Increment the index and make sure we have at least one item.
			index++;

			if (index >= parts.Length)
			{
				throw new InvalidOperationException(
					"Cannot have a private use subtag without at least one tag.");
			}

			// Pull in the rest of the elements.
			privateUseTags = new List<string>();

			for (; index < parts.Length; index++)
			{
				privateUseTags.Add(parts[index]);
			}
		}

		#endregion
	}
}
