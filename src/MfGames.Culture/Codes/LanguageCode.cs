// <copyright file="LanguageCode.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// An immutable identifier for an ISO language code including support unknown
	/// languages to the <c>System.Globalization.CultureInfo</c> implementation. This
	/// provides values for all components of ISO 639 code, but primarily focuses on
	/// the ISO 639-3 implementation.
	/// </summary>
	public class LanguageCode : IEquatable<LanguageCode>
	{
		#region Constructors and Destructors

		static LanguageCode()
		{
			Canonical = new LanguageCode("*", "*", "*", false);
		}

		public LanguageCode(string isoAlpha3T)
			: this(isoAlpha3T, null)
		{
		}

		public LanguageCode(string isoAlpha3T, string isoAlpha2)
			: this(isoAlpha3T, isoAlpha2, null)
		{
		}

		public LanguageCode(string isoAlpha3T, string isoAlpha2, string isoAlpha3B)
			: this(isoAlpha3T,
				isoAlpha2,
				isoAlpha3B,
				IsLanguageCodePrivateUse(isoAlpha3T))
		{
		}

		public LanguageCode(
			string isoAlpha3T,
			string isoAlpha2,
			string isoAlpha3B,
			bool isPrivateUse)
		{
			// Verify our contracts.
			if (isoAlpha3T == null)
			{
				throw new ArgumentNullException("isoAlpha3T");
			}

			// Verify lengths.
			if (isoAlpha3T != "*" && isoAlpha3T.Length != 3)
			{
				throw new ArgumentOutOfRangeException(
					"isoAlpha3T",
					"The ISO Alpha3 T code must be exactly three characters.");
			}

			if (isoAlpha3B != "*" && isoAlpha3B != null && isoAlpha3B.Length != 3)
			{
				throw new ArgumentOutOfRangeException(
					"isoAlpha3B",
					"The ISO Alpha3 B code must be exactly three characters.");
			}

			if (isoAlpha2 != "*" && isoAlpha2 != null && isoAlpha2.Length != 2)
			{
				throw new ArgumentOutOfRangeException(
					"isoAlpha2",
					"The ISO Alpha2 code must be exactly two characters.");
			}

			// Save the member variables.
			IsoAlpha2 = isoAlpha2 == null
				? null
				: string.Intern(isoAlpha2.ToLowerInvariant());
			IsoAlpha3B = isoAlpha3B == null
				? null
				: string.Intern(isoAlpha3B.ToLowerInvariant());
			IsoAlpha3T = string.Intern(isoAlpha3T.ToLower());
			IsPrivateUse = isPrivateUse;
		}

		#endregion

		#region Public Properties

		public static LanguageCode Canonical { get; private set; }

		/// <summary>
		/// Gets the two character ISO 639-1 code for the language. If there
		/// is no such code, this will be null.
		/// </summary>
		public string IsoAlpha2 { get; private set; }

		/// <summary>
		/// Gets the preferred three character ISO 639-2 code where the
		/// terminological (ISO 639-2/T) takes precendences over the
		/// bibiographic (ISO 639-2/B).
		/// </summary>
		public string IsoAlpha3 { get { return IsoAlpha3T ?? IsoAlpha3B; } }

		/// <summary>
		/// Gets the three character ISO 639-2/B (bibiographic) code for the
		/// language. In cases where the terminological (T) code is identical
		/// to this one, then this may null to indicate no difference.
		/// </summary>
		public string IsoAlpha3B { get; private set; }

		/// <summary>
		/// Gets the three character ISO 639-2/T (terminological) code for the
		/// language.
		/// </summary>
		/// <remarks>
		/// This field will never be null.
		/// </remarks>
		public string IsoAlpha3T { get; private set; }

		public bool IsPrivateUse { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static bool operator ==(LanguageCode left, LanguageCode right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(LanguageCode left, LanguageCode right)
		{
			return !Equals(left, right);
		}

		public bool Equals(LanguageCode other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return string.Equals(IsoAlpha3T, other.IsoAlpha3T);
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

			return Equals((LanguageCode)obj);
		}

		public override int GetHashCode()
		{
			return IsoAlpha3T.GetHashCode();
		}

		public override string ToString()
		{
			return IsoAlpha2 ?? IsoAlpha3;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a value indicating whether this language is private use.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is private use; otherwise, <c>false</c>.
		/// </value>
		private static bool IsLanguageCodePrivateUse(string alpha3)
		{
			// Normalize the case.
			alpha3 = alpha3.ToLowerInvariant();

			// If it doesn't start with Q, then it isn't private.
			if (alpha3[0] != 'q')
			{
				return false;
			}

			switch (alpha3[1])
			{
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
					return true;

				default:
					return false;
			}
		}

		#endregion
	}
}
