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
using System.Threading;

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
			: this(CodeManager.Instance, selector)
		{
		}

		public LanguageTagSelector(
			CodeManager codes,
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
				var quality = new LanguageTagQuality(codes, part);

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

		private LanguageTagSelector(params LanguageTagQuality[] qualities)
		{
			languages = qualities.ToList();
			languages.Sort();
		}

		#endregion

		#region Public Properties

		public static LanguageTagSelector All { get; private set; }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Creates a language tag selector from the UI culture at a quality of
		/// 1.0 with a canonical tag at 0.1f.
		/// </summary>
		/// <returns></returns>
		public static LanguageTagSelector FromThreadUICulture()
		{
			Thread thread = Thread.CurrentThread;
			var tag = new LanguageTag(thread.CurrentUICulture.IetfLanguageTag);
			var selector = new LanguageTagSelector(
				new LanguageTagQuality(tag),
				new LanguageTagQuality(LanguageTag.Canonical, 0.1f));

			return selector;
		}

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
