// <copyright file="CalendarSystemXmlReader.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Xml;

using Fractions;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Calculations;
using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Calendars.Lengths;

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

		private static Cycle ParseCalculatedCycle(XmlReader xml, string id)
		{
			// Create the calculated reference and pull out the common ref.
			var calculatedCycle = new CalculatedCycle(id);
			string cycleRef = xml.GetAttribute("ref");

			// See if we have a mod operation.
			string modAttribute = xml.GetAttribute("mod");

			if (modAttribute != null)
			{
				calculatedCycle.Calculation = new ModCycleCalculation(
					cycleRef,
					Int32.Parse(modAttribute));
			}

			// See if we have a div operation.
			string divAttribute = xml.GetAttribute("div");

			if (divAttribute != null)
			{
				calculatedCycle.Calculation = new DivCycleCalculation(
					cycleRef,
					Int32.Parse(divAttribute));
			}

			// Return the resulting cycle.
			return calculatedCycle;
		}

		private LogicCycleLength ParseLength(XmlReader xml)
		{
			// Create a new cycle.
			var count = 1;
			var lengths = new List<ILengthLogic>();

			// Loop through the XML reader until we get to the proper element.
			while (xml.Read())
			{
				// If we aren't the right namespace, just move on.
				if (xml.NamespaceURI != CultureXml.Namespace0)
				{
					continue;
				}

				// If we got the end element, break out.
				if (xml.NodeType == XmlNodeType.EndElement && xml.LocalName == "length")
				{
					break;
				}

				// If we aren't a start element.
				if (xml.NodeType != XmlNodeType.Element)
				{
					continue;
				}

				// Figure out what to do with the element.
				switch (xml.LocalName)
				{
					case "count":
						count = xml.ReadElementContentAsInt();
						break;

					case "julian":
						string cycleRef = xml.GetAttribute("ref");
						int mod = Convert.ToInt32(xml.GetAttribute("mod"));
						Fraction value = ReadFraction(xml);

						if (cycleRef == null)
						{
							lengths.Add(new ConstantLengthLogic(value));
						}
						else
						{
							lengths.Add(new IfModLengthLogic(cycleRef, mod, value));
						}
						break;
				}
			}

			// Return the resulting length.
			return new LogicCycleLength(count, lengths.ToArray());
		}

		private List<CycleLength> ParseLengths(XmlReader xml)
		{
			// Keep a list of all the lengths.
			var lengths = new List<CycleLength>();

			// Loop through the XML reader until we get to the proper element.
			while (xml.Read())
			{
				// If we aren't the right namespace, just move on.
				if (xml.NamespaceURI != CultureXml.Namespace0)
				{
					continue;
				}

				// If we got the end element, break out.
				if (xml.NodeType == XmlNodeType.EndElement && xml.LocalName == "lengths")
				{
					break;
				}

				// If we aren't a start element.
				if (xml.NodeType != XmlNodeType.Element)
				{
					continue;
				}

				// Figure out what to do with the element.
				switch (xml.LocalName)
				{
					case "length":
						CycleLength length = ParseLength(xml);
						lengths.Add(length);
						break;
				}
			}

			// Return the resulting lengths.
			return lengths;
		}

		private Cycle ParseListCycle(XmlReader xml, string id)
		{
			// Keep a list of all the lengths.
			var list = new List<ILengthLogic[]>();
			var logics = new List<ILengthLogic>();

			// Loop through the XML reader until we get to the proper element.
			while (xml.Read())
			{
				// If we aren't the right namespace, just move on.
				if (xml.NamespaceURI != CultureXml.Namespace0)
				{
					continue;
				}

				// If we got the end element, break out.
				if (xml.NodeType == XmlNodeType.EndElement)
				{
					switch (xml.LocalName)
					{
						case "list":
							var cycle = new LengthCycle(id);
							cycle.Lengths.Add(new ArrayCycleLength(list.ToArray()));
							return cycle;

						case "lengths":
							list.Add(logics.ToArray());
							logics.Clear();
							break;
					}
				}

				// If we aren't a start element.
				if (xml.NodeType != XmlNodeType.Element)
				{
					continue;
				}

				// Figure out what to do with the element.
				switch (xml.LocalName)
				{
					case "length":
						LogicCycleLength length = ParseLength(xml);
						logics.AddRange(length.LengthLogics);
						break;
				}
			}

			// If we get this far, we have a problem.
			throw new InvalidOperationException("Cannot parse list cycle (" + id + ").");
		}

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
				// If we aren't the right namespace, just move on.
				if (xml.NamespaceURI != CultureXml.Namespace0)
				{
					continue;
				}

				// If we got the end element, break out.
				if (xml.NodeType == XmlNodeType.EndElement && xml.LocalName == "calendar")
				{
					break;
				}

				// If we aren't a start element.
				if (xml.NodeType != XmlNodeType.Element)
				{
					continue;
				}

				// Figure out what to do with the element.
				switch (xml.LocalName)
				{
					case "calendar":
						//int version = Convert.ToInt32(xml.GetAttribute("version"));
						calendar.Id = xml.GetAttribute("xml:id");
						break;

					case "cycle":
						Cycle cycle = ReadCycle(xml);
						calendar.Cycles.Add(cycle);
						break;
				}
			}

			// Return the resulting calendar.
			return calendar;
		}

		private Cycle ReadCycle(XmlReader xml)
		{
			// Create the calendar we'll be populating.
			Cycle cycle = null;
			string id = xml.GetAttribute("xml:id");

			// Loop through the XML reader until we get to the proper element.
			while (xml.Read())
			{
				// If we aren't the right namespace, just move on.
				if (xml.NamespaceURI != CultureXml.Namespace0)
				{
					continue;
				}

				// If we got the end element, break out.
				if (xml.NodeType == XmlNodeType.EndElement && xml.LocalName == "cycle")
				{
					break;
				}

				// If we aren't a start element.
				if (xml.NodeType != XmlNodeType.Element)
				{
					continue;
				}

				// Figure out what to do with the element.
				switch (xml.LocalName)
				{
					case "calculate":
						cycle = ParseCalculatedCycle(xml, id);
						break;

					case "lengths":
						List<CycleLength> lengths = ParseLengths(xml);
						var lengthCycle = new LengthCycle(id);

						foreach (CycleLength length in lengths)
						{
							lengthCycle.Lengths.Add(length);
						}

						cycle = lengthCycle;
						break;

					case "list":
						cycle = ParseListCycle(xml, id);
						break;

					case "julian-offset":
						if (cycle == null)
						{
							throw new InvalidOperationException(
								"Cannot parse cycle (" + id
									+ ") without a calculated or lengths element.");
						}

						cycle.JulianDateOffset = ReadFraction(xml);
						break;

					case "cycle":
						if (cycle == null)
						{
							throw new InvalidOperationException(
								"Cannot parse cycle (" + id
									+ ") without a calculated or lengths element.");
						}

						Cycle childCycle = ReadCycle(xml);
						cycle.Cycles.Add(childCycle);
						break;
				}
			}

			// Return the resulting calendar.
			return cycle;
		}

		private Fraction ReadFraction(XmlReader xml)
		{
			// Read in the string.
			string value = xml.ReadElementString();

			// Look for the slash.
			decimal[] parts = value
				.Split('/')
				.Select(t => decimal.Parse(t.Trim()))
				.ToArray();

			// If we have one part, then it just a simple number.
			if (parts.Length == 1)
			{
				return new Fraction(parts[0]);
			}

			// If there isn't parts == 2, then blow up.
			if (parts.Length != 2)
			{
				throw new InvalidOperationException("Cannot parse fraction: " + value + ".");
			}

			// If we have two parts.
			var b1 = new BigInteger(parts[0]);
			var b2 = new BigInteger(parts[1]);

			return new Fraction(b1, b2);
		}

		#endregion
	}
}
