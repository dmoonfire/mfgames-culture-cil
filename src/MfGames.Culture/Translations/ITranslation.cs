// <copyright file="ITranslation.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Culture.Translations
{
	public interface ITranslation
	{
		#region Public Properties

		/// <summary>
		/// Retrieves the number of total translations.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Returns the first translation, which is typically the fallback
		/// translation.
		/// </summary>
		string First { get; }

		/// <summary>
		/// Gets a flag whether the translation can be changed.
		/// </summary>
		bool IsImmutable { get; }

		#endregion
	}
}
