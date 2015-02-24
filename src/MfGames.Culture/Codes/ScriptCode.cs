// <copyright file="ScriptCode.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using MfGames.Culture.Translations;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// An immutable identifier for an ISO 15924 script code including support
	/// for unknown scripts.
	/// </summary>
	public class ScriptCode
	{
		#region Constructors and Destructors

		public ScriptCode(
			ITranslationCollection names,
			string alpha4,
			short? numeric)
		{
			Names = names;
			Alpha4 = alpha4;
			Numeric = numeric;
		}

		#endregion

		#region Public Properties

		public string Alpha4 { get; private set; }
		public ITranslationCollection Names { get; private set; }
		public short? Numeric { get; private set; }

		#endregion

		#region Public Methods and Operators

		public bool Equals(string code)
		{
			return code == Alpha4;
		}

		public override string ToString()
		{
			return Alpha4;
		}

		#endregion
	}
}
