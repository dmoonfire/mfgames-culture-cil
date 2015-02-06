// <copyright file="ImmutableTranslation.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Culture.Codes
{
	public class ImmutableTranslation : ITranslation
	{
		#region Fields

		private readonly ITranslation translation;

		#endregion

		#region Constructors and Destructors

		public ImmutableTranslation(ITranslation translation)
		{
			this.translation = translation;
		}

		#endregion

		#region Public Properties

		public int Count { get { return translation.Count; } }
		public string First { get { return translation.First; } }
		public bool IsImmutable { get { return true; } }

		#endregion
	}
}
