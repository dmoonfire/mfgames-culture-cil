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
	public class LanguageTag
	{
		#region Constructors and Destructors

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

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the ISO 639 description of the language.
		/// </summary>
		/// <value>
		/// The language object.
		/// </value>
		public LanguageCode Language { get; private set; }

		#endregion

		#region Public Methods and Operators

		public override string ToString()
		{
			return Language.Alpha3;
		}

		#endregion
	}
}
