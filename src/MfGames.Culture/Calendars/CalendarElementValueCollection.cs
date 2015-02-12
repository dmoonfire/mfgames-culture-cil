// <copyright file="CalendarElementValueCollection.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

namespace MfGames.Culture.Calendars
{
	public class CalendarElementValueCollection : Dictionary<string, int>,
		IDictionary<string, object>
	{
		#region Constructors and Destructors

		public CalendarElementValueCollection(CalendarElementValueCollection values)
		{
			foreach (KeyValuePair<string, int> pair in values)
			{
				this[pair.Key] = pair.Value;
			}
		}

		public CalendarElementValueCollection()
		{
		}

		#endregion

		#region Explicit Interface Properties

		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		ICollection<string> IDictionary<string, object>.Keys
		{
			get { throw new NotImplementedException(); }
		}

		ICollection<object> IDictionary<string, object>.Values
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region Explicit Interface Indexers

		object IDictionary<string, object>.this[string key]
		{
			get { return this[key]; }
			set { this[key] = Convert.ToInt32(value); }
		}

		#endregion

		#region Explicit Interface Methods

		void ICollection<KeyValuePair<string, object>>.Add(
			KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		void IDictionary<string, object>.Add(string key, object value)
		{
			throw new NotImplementedException();
		}

		bool ICollection<KeyValuePair<string, object>>.Contains(
			KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		void ICollection<KeyValuePair<string, object>>.CopyTo(
			KeyValuePair<string, object>[] array,
			int arrayIndex)
		{
			throw new NotImplementedException();
		}

		IEnumerator<KeyValuePair<string, object>>
			IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		bool ICollection<KeyValuePair<string, object>>.Remove(
			KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
