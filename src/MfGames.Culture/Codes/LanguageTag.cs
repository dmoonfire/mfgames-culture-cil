// <copyright file="LanguageTag.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// An immutable identifier for an IEFT language tag including the ISO 639 language
	/// code and optional components for country, dialect, and sub-tags.
	/// </summary>
	public class LanguageTag : IEquatable<LanguageTag>, IComparable<LanguageTag>
	{
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
			ParseExtendedLanguageTags(parts, ref index);
			ParseScript(codes, parts, ref index);
			ParseCountry(codes, parts, ref index);
			ParseVariants(parts, ref index);
			ParseExtensions(parts, ref index);
			ParsePrivateUse(parts, ref index);

			// If we still have tags left, its invalid.
			if (index < parts.Length)
			{
				throw new InvalidOperationException("Could not parse language tag.");
			}
		}

		#endregion

		#region Public Properties

		public static LanguageTag Canonical { get; private set; }
		public CountryCode Country { get; private set; }
		public ImmutableList<string> ExtendedLanguageTags { get; private set; }
		public ImmutableList<LanguageTagExtension> Extensions { get; private set; }

		/// <summary>
		/// Gets the ISO 639 description of the language.
		/// </summary>
		/// <value>
		/// The language object.
		/// </value>
		public LanguageCode Language { get; private set; }

		public ImmutableList<string> PrivateUse { get; private set; }
		public ScriptCode Script { get; private set; }
		public ImmutableList<string> Variants { get; private set; }

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
			// Build up a list of tags.
			var tags = new List<string>();

			// Go through the codes.
			if (Language != null)
			{
				tags.Add(Language.IsoAlpha2);
			}

			tags.AddRange(ExtendedLanguageTags);

			if (Script != null)
			{
				tags.Add(Script.Alpha4);
			}

			if (Country != null)
			{
				tags.Add(Country.Alpha2);
			}

			tags.AddRange(Variants);

			foreach (LanguageTagExtension extension in Extensions)
			{
				tags.Add(extension.Type);
				tags.AddRange(extension.Tags);
			}

			if (PrivateUse.Count > 0)
			{
				tags.Add("x");
				tags.AddRange(PrivateUse);
			}

			// Return the resulting tags, combined together with dashes.
			string results = string.Join("-", tags.ToArray());
			return results;
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

			if (country.Length != 2)
			{
				return;
			}

			// Try to look up the country.
			Country = codes.Countries.GetIsoAlpha2(country);

			if (Country == null)
			{
				throw new InvalidOperationException(
					"Found an invalid country code: " + country + ".");
			}

			// Increment the index to move to the next part.
			index++;
		}

		private void ParseExtendedLanguageTags(
			string[] parts,
			ref int index)
		{
			// Make sure we aren't at the end of the string.
			ImmutableList<string> list = ImmutableList<string>.Empty;

			if (index < parts.Length)
			{
				// See if we have at least one valid code. Extended languages are
				// always three characters each, in lower case.
				string value = parts[index].ToLower();
				while (value.Length == 3)
				{
					// Add them to the list.
					list = list.Add(string.Intern(value));
					index++;
				}

				// If we have no items, then we're done.
				if (list.Count > 3)
				{
					throw new InvalidOperationException(
						"The BCP 47 spcification only allows three extended language tags.");
				}
			}

			// Save it for later.
			ExtendedLanguageTags = list;
		}

		private void ParseExtensions(string[] parts, ref int index)
		{
			// Make sure we aren't at the end of the string.
			var list = new List<LanguageTagExtension>();

			while (index < parts.Length)
			{
				// Variant codes are 5-8 characters or 4 characters with an
				// initial digit.
				string value = parts[index].ToLowerInvariant();
				int length = value.Length;

				if (length != 1 && value != "x")
				{
					break;
				}

				// Create a language extension and validate its contents.
				var extension = new LanguageTagExtension(value, parts, ref index);

				// Make sure we don't have the list.
				if (list.Contains(extension))
				{
					throw new Exception("A single extension tag cannot show more than once.");
				}

				// Add it to the list.
				list.Add(extension);
			}

			// Save it for later.
			list.Sort();
			Extensions = list.ToImmutableList();
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
			ImmutableList<string> list = ImmutableList<string>.Empty;

			if (index < parts.Length)
			{
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
				for (; index < parts.Length; index++)
				{
					list.Add(string.Intern(parts[index].ToLower()));
				}
			}

			// Set the private use list.
			PrivateUse = list;
		}

		private void ParseScript(CodeManager codes, string[] parts, ref int index)
		{
			// Make sure we aren't at the end of the string.
			if (index >= parts.Length)
			{
				return;
			}

			// We only allow four-character script codes.
			string script = parts[index];

			if (script.Length != 4)
			{
				return;
			}

			// Try to look up the country.
			Script = codes.Scripts.Get(script);

			if (Script == null)
			{
				throw new InvalidOperationException(
					"Found an invalid script code: " + script + ".");
			}

			// Increment the index to move to the next part.
			index++;
		}

		private void ParseVariants(string[] parts, ref int index)
		{
			// Make sure we aren't at the end of the string.
			ImmutableList<string> list = ImmutableList<string>.Empty;

			while (index < parts.Length)
			{
				// Variant codes are 5-8 characters or 4 characters with an
				// initial digit.
				string value = parts[index].ToLowerInvariant();
				int length = value.Length;

				if (length < 5 || length > 8)
				{
					break;
				}

				if (length == 4 && char.IsDigit(value[0]))
				{
					break;
				}

				// Add them to the list.
				list = list.Add(string.Intern(value));
				index++;
			}

			// Save it for later.
			Variants = list;
		}

		#endregion
	}
}
