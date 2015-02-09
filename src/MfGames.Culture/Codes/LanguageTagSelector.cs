// <copyright file="LanguageTagSelector.cs" company="Moonfire Games">
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
	/// Contains a list of accepted languages and their preferred weighting
	/// for translations.
	/// </summary>
	/// <remarks>
	/// This is modeled after the HTTP Accept-Language header.
	/// </remarks>
	public class LanguageTagSelector
	{
		#region Fields

		private readonly List<LanguageTagQuality> languages;

		#endregion

		#region Constructors and Destructors

		public LanguageTagSelector()
		{
			languages = new List<LanguageTagQuality>();
		}

		public LanguageTagSelector(string selector)
			: this(LanguageCodeManager.Instance, selector)
		{
		}

		public LanguageTagSelector(
			LanguageCodeManager languageManager,
			string selector)
			: this()
		{
			// Establish our contracts.
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}

			// Split the string on the commas.
			string[] parts = selector.Split(',');

			foreach (string part in parts)
			{
				var quality = new LanguageTagQuality(languageManager, part);

				languages.Add(quality);
			}
		}

		#endregion
	}
}
