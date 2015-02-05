// <copyright file="CalendarElementCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MfGames.Culture.Calendars
{
	/// <summary>
	/// A specialized collection of calendar elements that enforces ID constraint
	/// while focusing on a small number of total elements.
	/// </summary>
	/// <typeparam name="TElement"></typeparam>
	public class CalendarElementCollection<TElement> : ICollection<TElement>
		where TElement : CalendarElement
	{
		#region Fields

		private readonly List<TElement> elements;

		#endregion

		#region Constructors and Destructors

		public CalendarElementCollection()
		{
			elements = new List<TElement>();
		}

		#endregion

		#region Public Properties

		public int Count { get { return elements.Count; } }
		public bool IsReadOnly { get { return false; } }

		#endregion

		#region Public Indexers

		public TElement this[string elementRef]

		{
			get
			{
				TElement element =
					elements.FirstOrDefault(e => e.Id == elementRef);

				if (element != null)
				{
					return element;
				}

				throw new IndexOutOfRangeException(
					"Cannot find calendar element: " + elementRef + ".");
			}

			set
			{
				elements.RemoveAll(p => p.Id == elementRef);
				elements.Add(value);
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Add(TElement item)
		{
			this[item.Id] = item;
		}

		public void Clear()
		{
			elements.Clear();
		}

		public bool Contains(TElement item)
		{
			return elements.Contains(item);
		}

		public void CopyTo(TElement[] array, int arrayIndex)
		{
			elements.CopyTo(array, arrayIndex);
		}

		public IEnumerator<TElement> GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		public bool Remove(TElement item)
		{
			return elements.Remove(item);
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
