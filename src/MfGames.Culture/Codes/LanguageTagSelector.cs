// <copyright file="LanguageTagSelector.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// Contains a list of accepted languages and their preferred weighting
	/// for translations.
	/// </summary>
	/// <remarks>
	/// This is modeled after the HTTP Accept-Language header.
	/// </remarks>
	public class LanguageTagSelector : IEnumerable<LanguageTagQuality>
	{
		#region Fields

		private readonly List<LanguageTagQuality> languages;

		#endregion

		#region Constructors and Destructors

		static LanguageTagSelector()
		{
			LanguageTag allLanguage = LanguageTag.Canonical;

			All = new LanguageTagSelector(allLanguage);
		}

		public LanguageTagSelector()
		{
			languages = new List<LanguageTagQuality>();
		}

		public LanguageTagSelector(string selector)
			: this(LanguageCodeManager.Instance, selector)
		{
		}

		public LanguageTagSelector(
			ILanguageCodeManager languageManager,
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

			// Make sure the qualities are sorted from high to low.
			languages.Sort();
		}

		public LanguageTagSelector(LanguageTag tag)
			: this()
		{
			var quality = new LanguageTagQuality(tag, 1f);

			languages.Add(quality);
		}

		#endregion

		#region Public Properties

		public static LanguageTagSelector All { get; private set; }

		#endregion

		#region Public Methods and Operators

		public IEnumerator<LanguageTagQuality> GetEnumerator()
		{
			return languages.GetEnumerator();
		}

		public override string ToString()
		{
			string results = string.Join(
				", ",
				languages.Select(l => l.ToString()));

			return results;
		}

		#endregion

		#region Explicit Interface Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
