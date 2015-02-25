// <copyright file="GregorianCalendarSystem.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using Fractions;

using MfGames.Culture.Calendars.Calculations;
using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Calendars.Lengths;
using MfGames.Culture.Codes;
using MfGames.Culture.Translations;
using MfGames.HierarchicalPaths;

namespace MfGames.Culture.Calendars
{
	public class GregorianCalendarSystem : CalendarSystem
	{
		#region Constructors and Destructors

		public GregorianCalendarSystem()
			: this(CodeManager.Instance)
		{
		}

		public GregorianCalendarSystem(CodeManager codes)
			: base(codes)
		{
			// Set up the cannonical name which is used for various lookups.

			// Add in the names and set up translations.
			SetupTranslations();

			// Everything is hung off the year. We use a number of lengths for
			// this. The first is to "fast forward" every 400 years until we
			// get closer to the point. The second is to figure out the precise
			// length of the year. Since we don't use another element as a
			// reference, it defaults to 1.0m Julian Days.
			var year = new LengthCycle("Year")
			{
				JulianDateOffset = new Fraction(-0.5m - 1721059m)
			};
			var yearLength400 = new LogicCycleLength(
				400,
				365m * 400 + 100m - 4m + 1m);
			var yearLength1 = new LogicCycleLength(
				new IfModLengthLogic("Year", 400, 366),
				new IfModLengthLogic("Year", 100, 365),
				new IfModLengthLogic("Year", 4, 366),
				new ConstantLengthLogic(365));

			year.Lengths.Add(yearLength400);
			year.Lengths.Add(yearLength1);

			// These are elements that are calculated based on the year index.
			var decade = new CalculatedCycle(
				"Decade",
				new DivCycleCalculation("Year", 10));
			var century = new CalculatedCycle(
				"Century",
				new DivCycleCalculation("Year", 100));
			var millennium = new CalculatedCycle(
				"Millennium",
				new DivCycleCalculation("Year", 1000));
			var decadeYear = new CalculatedCycle(
				"Decade Year",
				new ModCycleCalcualtion("Year", 10));
			var centuryDecade = new CalculatedCycle(
				"Century Decade",
				new ModCycleCalcualtion("Decade", 10));
			var millenniumCentury = new CalculatedCycle(
				"Millennium Century",
				new ModCycleCalcualtion("Century", 10));

			decade.Cycles.Add(decadeYear);
			century.Cycles.Add(centuryDecade);
			millennium.Cycles.Add(millenniumCentury);

			year.Cycles.Add(decade);
			year.Cycles.Add(century);
			year.Cycles.Add(millennium);

			// Day of year is simply a zero-based number of days in the year.
			var yearDay = new LengthCycle("Year Day");
			var yearDayLength = new LogicCycleLength(1, 1.0m);

			yearDay.Lengths.Add(yearDayLength);
			year.Cycles.Add(yearDay);

			// Month is the most complicated aspect of the calendar.
			var yearMonth = new LengthCycle("Year Month");
			ILengthLogic[] monthDay31 = { new ConstantLengthLogic(31m) };
			ILengthLogic[] monthDay30 = { new ConstantLengthLogic(30m) };
			ILengthLogic[] february =
			{
				new IfModLengthLogic("Year", 400, 29m),
				new IfModLengthLogic("Year", 100, 28m),
				new IfModLengthLogic("Year", 4, 29m),
				new ConstantLengthLogic(28)
			};

			var yearMonthLength = new ArrayCycleLength(
				monthDay31,
				february,
				monthDay31,
				monthDay30,
				monthDay31,
				monthDay30,
				monthDay31,
				monthDay31,
				monthDay30,
				monthDay31,
				monthDay30,
				monthDay31);
			var monthDay = new LengthCycle("Month Day");
			var monthDayLength = new LogicCycleLength(1, 1.0m);

			monthDay.Lengths.Add(monthDayLength);
			yearMonth.Cycles.Add(monthDay);
			yearMonth.Lengths.Add(yearMonthLength);
			year.Cycles.Add(yearMonth);

			// Create the calendar and add the open cycle which will add
			// everything else.
			Add(year);
		}

		#endregion

		#region Methods

		private void SetupTranslations()
		{
			// This is the root translation path for everything.
			new HierarchicalPath(
				"/MfGames/Culture/Calendar/Gregorian/");
			var translations = new MemoryTranslationManager();

			Codes = translations;

			// Set up the names.
			translations.Add(
				new HierarchicalPath("Name", TranslationPath),
				LanguageTag.Canonical,
				"Gregorian");

			// Add in the translations for the cycles.
			var shortPath = new HierarchicalPath("Short", TranslationPath);
			translations.AddRange(
				shortPath,
				LanguageTag.Canonical,
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec");
			translations.Add(
				new HierarchicalPath("jan", shortPath),
				LanguageTag.Canonical,
				"0");
			translations.Add(
				new HierarchicalPath("feb", shortPath),
				LanguageTag.Canonical,
				"1");
			translations.Add(
				new HierarchicalPath("mar", shortPath),
				LanguageTag.Canonical,
				"2");
			translations.Add(
				new HierarchicalPath("apr", shortPath),
				LanguageTag.Canonical,
				"3");
			translations.Add(
				new HierarchicalPath("may", shortPath),
				LanguageTag.Canonical,
				"4");
			translations.Add(
				new HierarchicalPath("jun", shortPath),
				LanguageTag.Canonical,
				"5");
			translations.Add(
				new HierarchicalPath("jul", shortPath),
				LanguageTag.Canonical,
				"6");
			translations.Add(
				new HierarchicalPath("aug", shortPath),
				LanguageTag.Canonical,
				"7");
			translations.Add(
				new HierarchicalPath("sep", shortPath),
				LanguageTag.Canonical,
				"8");
			translations.Add(
				new HierarchicalPath("oct", shortPath),
				LanguageTag.Canonical,
				"9");
			translations.Add(
				new HierarchicalPath("nov", shortPath),
				LanguageTag.Canonical,
				"10");
			translations.Add(
				new HierarchicalPath("dec", shortPath),
				LanguageTag.Canonical,
				"11");
		}

		#endregion
	}
}
