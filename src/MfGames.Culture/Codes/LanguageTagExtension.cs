// <copyright file="LanguageTagExtension.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Immutable;

namespace MfGames.Culture.Codes
{
	/// <summary>
	/// An immutable class that represents a single extension.
	/// </summary>
	public class LanguageTagExtension : IEquatable<LanguageTagExtension>,
		IComparable<LanguageTagExtension>
	{
		#region Constructors and Destructors

		public LanguageTagExtension(
			string extensionType,
			string[] parts,
			ref int index)
		{
			// The current part is always the extension, which is never an X.
			Type = string.Intern(extensionType);
			index++;

			// Loop through until we hit the end of the parts or we encounter
			// another extension.
			ImmutableList<string> list = ImmutableList<string>.Empty;

			while (index + 1 < parts.Length)
			{
				// Check for another extension. If we found one, then stop.
				string value = parts[index].ToLowerInvariant();

				if (value.Length == 1)
				{
					return;
				}

				// Otherwise, add it to the list.
				list = list.Add(string.Intern(value));
				index++;
			}

			// Verify we have at lesat one.
			if (list.Count == 0)
			{
				throw new InvalidOperationException(
					"Language tag extensions must have at least one value.");
			}

			// Save the tags.
			Tags = list;
		}

		#endregion

		#region Public Properties

		public ImmutableList<string> Tags { get; private set; }
		public string Type { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static bool operator ==(
			LanguageTagExtension left,
			LanguageTagExtension right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(
			LanguageTagExtension left,
			LanguageTagExtension right)
		{
			return !Equals(left, right);
		}

		public int CompareTo(LanguageTagExtension other)
		{
			return String.Compare(Type, other.Type, StringComparison.Ordinal);
		}

		public bool Equals(LanguageTagExtension other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return string.Equals(Type, other.Type);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != GetType())
			{
				return false;
			}

			return Equals((LanguageTagExtension)obj);
		}

		public override int GetHashCode()
		{
			return Type.GetHashCode();
		}

		#endregion
	}
}
