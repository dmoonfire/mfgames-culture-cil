// <copyright file="CalendarSystemXmlReaderTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.IO;

using MfGames.Culture.Calendars;
using MfGames.Culture.IO;

using NUnit.Framework;

namespace MfGames.Culture.Tests.IO
{
	[TestFixture]
	public class CalendarSystemXmlReaderTests
	{
		#region Public Methods and Operators

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
	}
}
