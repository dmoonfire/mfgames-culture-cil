// <copyright file="LanguageTagSelector.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections;
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
	public class LanguageTagSelector : IEnumerable<LanguageTagQuality>
	{
		#region Static Fields

		private static readonly Lazy<LanguageTagSelector> all;

		#endregion

		#region Fields

		private readonly List<LanguageTagQuality> languages;

		#endregion

		#region Constructors and Destructors

		static LanguageTagSelector()
		{
			all = new Lazy<LanguageTagSelector>(
				CreateAll);
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

			// Make sure the qualities are sorted from high to low.
			languages.Sort();
		}

		public LanguageTagSelector(LanguageTag tag)
		{
			languages.Add(new LanguageTagQuality(tag, 1f));
		}

		#endregion

		#region Public Properties

		public static LanguageTagSelector All { get { return all.Value; } }

		#endregion

		#region Public Methods and Operators

		public IEnumerator<LanguageTagQuality> GetEnumerator()
		{
			return languages.GetEnumerator();
		}

		#endregion

		#region Explicit Interface Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Methods

		private static LanguageTagSelector CreateAll()
		{
			var results = new LanguageTagSelector(LanguageTag.All);
			return results;
		}

		#endregion
	}
}
