// <copyright file="CompositeTranslationProvider.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

using MfGames.Culture.Codes;

namespace MfGames.Culture.Translations
{
	public class CompositeTranslationProvider : ITranslationProvider
	{
		#region Constructors and Destructors

		public CompositeTranslationProvider()
		{
			Providers = new List<ITranslationProvider>();
		}

		#endregion

		#region Public Properties

		public IList<ITranslationProvider> Providers { get; private set; }

		#endregion

		#region Public Methods and Operators

		public void Add(ITranslationProvider translationProvider)
		{
			Providers.Add(translationProvider);
		}

		public TranslationResult GetTranslationResult(
			string key,
			LanguageTagSelector selector)
		{
			// Loop through the translations and try to find a better one.
			TranslationResult result = null;

			foreach (ITranslationProvider provider in Providers)
			{
				// Get the result from this item.
				TranslationResult providerResult = provider.GetTranslationResult(
					key,
					selector);

				if (providerResult == null)
				{
					continue;
				}

				// If the quality of the provider is 1.0f, then automatically
				// accept it as the best.
				if (Math.Abs(providerResult.Quality.Quality - 1.0f) < 0.01f)
				{
					return providerResult;
				}

				// Otherwise, keep it to see if we can find a better one.
				if (result == null || result < providerResult)
				{
					result = providerResult;
				}
			}

			// Whatever we have at the end is it, even if it is null.
			return result;
		}

		#endregion
	}
}
