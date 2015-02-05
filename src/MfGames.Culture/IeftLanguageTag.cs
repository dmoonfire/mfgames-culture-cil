// <copyright file="IeftLanguageTag.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Culture
{
	/// <summary>
	/// An immutable identifier for an IEFT language tag including the ISO 639 language
	/// code and optional components for country, dialect, and sub-tags.
	/// </summary>
	public class IeftLanguageTag
	{
		#region Public Properties

		/// <summary>
		/// Gets the ISO 639 description of the language.
		/// </summary>
		/// <value>
		/// The language object.
		/// </value>
		public Iso639Code Language { get; private set; }

		#endregion
	}
}
