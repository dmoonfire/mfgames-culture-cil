// <copyright file="LanguageTag.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

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
			: this(LanguageCodeManager.Instance, language)
		{
		}

		public LanguageTag(ILanguageCodeManager languages, string tag)
		{
			// Establish our contracts.
			if (languages == null)
			{
				throw new ArgumentNullException("languages");
			}

			if (tag == null)
			{
				throw new ArgumentNullException("language");
			}

			// Try to parse the language.
			string language = tag.Split('-')[0];

			Language = languages.Get(language);
		}

		private LanguageTag()
		{
		}

		#endregion

		#region Public Properties

		public static LanguageTag Canonical { get; private set; }

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
	}
}
