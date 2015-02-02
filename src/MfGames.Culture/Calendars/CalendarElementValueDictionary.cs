// <copyright file="CalendarElementValueDictionary.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CalendarElementValueDictionary : Dictionary<string, int>,
        IDictionary<string, object>
    {
        IEnumerator<KeyValuePair<string, object>>
            IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<string, object>>.Add(
            KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(
            KeyValuePair<string, object> item)
        {
            return ContainsKey(item.Key);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(
            KeyValuePair<string, object>[] array,
            int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(
            KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        void IDictionary<string, object>.Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        bool IDictionary<string, object>.TryGetValue(
            string key,
            out object value)
        {
            int intValue;

            if (TryGetValue(key, out intValue))
            {
                value = intValue;
                return true;
            }

            value = null;
            return false;
        }

        object IDictionary<string, object>.this[string key]
        {
            get { return this[key]; }
            set
            {
                int intValue = Convert.ToInt32(value);
                this[key] = intValue;
            }
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get { throw new NotImplementedException(); }
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get { throw new NotImplementedException(); }
        }
    }
}
