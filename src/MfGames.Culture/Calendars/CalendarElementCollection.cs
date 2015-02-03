// <copyright file="CalendarElementCollection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A specialized collection of calendar elements that enforces ID constraint
    /// while focusing on a small number of total elements.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public class CalendarElementCollection<TElement> : ICollection<TElement>
        where TElement : CalendarElement
    {
        public CalendarElementCollection()
        {
            elements = new List<TElement>();
        }

        private readonly List<TElement> elements;

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

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

        public bool Remove(TElement item)
        {
            return elements.Remove(item);
        }

        public int Count { get { return elements.Count; } }

        public bool IsReadOnly { get { return false; } }
    }
}
