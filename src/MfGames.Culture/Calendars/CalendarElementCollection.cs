// <copyright file="CalendarElementCollection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Encapsulates the common logic for managing cycles within a calendar.
    /// </summary>
    public class CalendarElementCollection<TElement> : ICollection<TElement>
        where TElement : CalendarElement
    {
        private readonly List<TElement> list;

        public CalendarElementCollection()
        {
            this.list = new List<TElement>();
        }

        public TElement this[string id]
        {
            get { return this.First(e => e.Id == id); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public void Add(TElement item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(TElement item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(TElement[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public bool Remove(TElement item)
        {
            return this.list.Remove(item);
        }

        public int Count { get { return this.list.Count; } }
        public bool IsReadOnly { get; set; }
    }
}
