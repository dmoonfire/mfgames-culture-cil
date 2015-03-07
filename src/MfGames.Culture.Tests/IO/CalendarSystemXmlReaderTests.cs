// <copyright file="CalendarSystemXmlReaderTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.IO;
using System.Text;

using MarkdownLog;

using MfGames.Culture.Calendars;
using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.IO;

using Newtonsoft.Json;

using NUnit.Framework;

namespace MfGames.Culture.Tests.IO
{
	[TestFixture]
	public class CalendarSystemXmlReaderTests
	{
		#region Public Methods and Operators

		[Test]
		public void CompareCycles()
		{
			// Create the code version.
			var code = new GregorianCalendarSystem();

			// Read the calendar from XML.
			const string Path = "..\\..\\data\\calendars\\gregorian.xml";
			var reader = new CalendarSystemXmlReader();
			ICalendarSystem xml;

			using (FileStream stream = File.OpenRead(Path))
				xml = reader.Read(stream);

			// Compare the two using JSON, jsut an easy way to handle recursion.
			string codeFormat = FormatCycles(code);
			string xmlFormat = FormatCycles((CalendarSystem)xml);

			Console.WriteLine("Code Version".ToMarkdownHeader());
			Console.WriteLine(codeFormat);
			Console.WriteLine();
			Console.WriteLine("XML Version".ToMarkdownHeader());
			Console.WriteLine(xmlFormat);

			Assert.AreEqual(codeFormat, xmlFormat);
		}

		[Test]
		public void CompareJson()
		{
			// Create the code version.
			var code = new GregorianCalendarSystem();

			// Read the calendar from XML.
			const string Path = "..\\..\\data\\calendars\\gregorian.xml";
			var reader = new CalendarSystemXmlReader();
			ICalendarSystem xml;

			using (FileStream stream = File.OpenRead(Path))
				xml = reader.Read(stream);

			// Compare the two using JSON, jsut an easy way to handle recursion.
			string codeJson = JsonConvert.SerializeObject(code, Formatting.Indented);
			string xmlJson = JsonConvert.SerializeObject(xml, Formatting.Indented);

			Console.WriteLine("Code Version".ToMarkdownHeader());
			Console.WriteLine(codeJson);
			Console.WriteLine();
			Console.WriteLine("XML Version".ToMarkdownHeader());
			Console.WriteLine(xmlJson);

			File.WriteAllText(@"C:\temp\b.json", xmlJson);

			Assert.AreEqual(codeJson, xmlJson);
		}

		[Test]
		public void ReadGregorian()
		{
			// Read the calendar into memory.
			const string Path = "..\\..\\data\\calendars\\gregorian.xml";
			var reader = new CalendarSystemXmlReader();
			ICalendarSystem calendar;

			using (FileStream stream = File.OpenRead(Path))
				calendar = reader.Read(stream);

			Console.WriteLine(calendar);
		}

		#endregion

		#region Methods

		private string FormatCycles(CalendarSystem calendar)
		{
			var builder = new StringBuilder();

			FormatCycles(calendar.Cycles, builder, "");

			return builder.ToString();
		}

		private void FormatCycles(
			CalendarElementCollection<Cycle> cycles,
			StringBuilder builder,
			string prefix)
		{
			if (cycles == null)
			{
				return;
			}

			foreach (Cycle cycle in cycles)
			{
				builder.AppendFormat("{0}{1}", prefix, cycle.Id);
				builder.AppendLine();
				FormatCycles(cycle.Cycles, builder, prefix + "  ");
			}
		}

		#endregion
	}
}
