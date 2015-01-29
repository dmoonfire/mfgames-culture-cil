namespace MfGames.Culture.Calendars
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Encapsulates the common logic for managing cycles within a calendar.
    /// </summary>
    public class CycleCollection : ICollection<Cycle>
    {
        /// <summary>
        /// </summary>
        private readonly List<Cycle> cycles;

        /// <summary>
        /// </summary>
        public CycleCollection()
        {
            this.cycles = new List<Cycle>();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public IEnumerator<Cycle> GetEnumerator()
        {
            return this.cycles.GetEnumerator();
        }

        public void Add(Cycle item)
        {
            this.cycles.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// </summary>
        public void Clear()
        {
            this.cycles.Clear();
        }

        /// <summary>
        /// </summary>
        /// <param name="item">
        /// </param>
        /// <returns>
        /// </returns>
        public bool Contains(Cycle item)
        {
            return this.cycles.Contains(item);
        }

        /// <summary>
        /// </summary>
        /// <param name="array">
        /// </param>
        /// <param name="arrayIndex">
        /// </param>
        public void CopyTo(Cycle[] array, int arrayIndex)
        {
            this.cycles.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// </summary>
        /// <param name="item">
        /// </param>
        /// <returns>
        /// </returns>
        public bool Remove(Cycle item)
        {
            return this.cycles.Remove(item);
        }

        /// <summary>
        /// </summary>
        public int Count
        {
            get
            {
                return this.cycles.Count;
            }
        }

        /// <summary>
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}