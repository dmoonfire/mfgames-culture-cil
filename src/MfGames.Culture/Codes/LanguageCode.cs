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
			All = new LanguageCode("*", "*", "*", false);
		}

		public LanguageCode(string alpha3T)
			: this(null, alpha3T)
		{
		}

		public LanguageCode(
			string alpha2,
			string alpha3T)
			: this(alpha2, null, alpha3T)
		{
		}

		public LanguageCode(
			string alpha2,
			string alpha3B,
			string alpha3T)
			: this(alpha2, alpha3B, alpha3T, IsLanguageCodePrivateUse(alpha3T))
		{
		}

		public LanguageCode(
			string alpha2,
			string alpha3B,
			string alpha3T,
			bool isPrivateUse)
		{
			// Verify our contracts.
			if (alpha3T == null)
			{
				throw new ArgumentNullException("alpha3T");
			}

			// Save the member variables.
			Alpha2 = alpha2 == null ? null : string.Intern(alpha2.ToLowerInvariant());
			Alpha3B = alpha3B == null ? null : string.Intern(alpha3B.ToLowerInvariant());
			Alpha3T = string.Intern(alpha3T.ToLower());
			IsPrivateUse = isPrivateUse;
		}

		#endregion

		#region Public Properties

		public static LanguageCode All { get; private set; }

		/// <summary>
		/// Gets the two character ISO 639-1 code for the language. If there
		/// is no such code, this will be null.
		/// </summary>
		public string Alpha2 { get; private set; }

		/// <summary>
		/// Gets the preferred three character ISO 639-2 code where the
		/// terminological (ISO 639-2/T) takes precendences over the
		/// bibiographic (ISO 639-2/B).
		/// </summary>
		public string Alpha3 { get { return Alpha3T ?? Alpha3B; } }

		/// <summary>
		/// Gets the three character ISO 639-2/B (bibiographic) code for the
		/// language. In cases where the terminological (T) code is identical
		/// to this one, then this may null to indicate no difference.
		/// </summary>
		public string Alpha3B { get; private set; }

		/// <summary>
		/// Gets the three character ISO 639-2/T (terminological) code for the
		/// language.
		/// </summary>
		/// <remarks>
		/// This field will never be null.
		/// </remarks>
		public string Alpha3T { get; private set; }

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

			return string.Equals(Alpha3T, other.Alpha3T);
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
			return Alpha3T.GetHashCode();
		}

		public override string ToString()
		{
			return Alpha3;
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
