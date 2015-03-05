// <copyright file="CalendarSystemXmlReader.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.IO;
using System.Xml;

using MfGames.Culture.Calendars;

namespace MfGames.Culture.IO
{
	public class CalendarSystemXmlReader
	{
		#region Public Methods and Operators

		/// <summary>
		/// Reads a given stream and create a calendar system out of it.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public ICalendarSystem Read(Stream stream)
		{
			using (XmlReader xml = XmlReader.Create(stream))
				return Read(xml);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Reads a given XML reader and creates a calendar out of it.
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		private ICalendarSystem Read(XmlReader xml)
		{
			// Create the calendar we'll be populating.
			var calendar = new CalendarSystem();

			// Loop through the XML reader until we get to the proper element.
			while (xml.Read())
			{
				// If we aren't at the calendar begin, just move on.
				if (xml.NodeType != XmlNodeType.Element ||
					xml.NamespaceURI != CultureXml.Namespace0 ||
					xml.LocalName != "calendar")
				{
					continue;
				}

				// Pull out the attributes.
				int version = Convert.ToInt32(xml.GetAttribute("version"));
				string id = xml.GetAttribute("xml:id");

				Console.WriteLine("versio {0} id {1}", version, id);
			}

			// Return the resulting calendar.
			return calendar;
		}

		#endregion
	}
}
