// <copyright file="CalendarSystemValidator.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-culture-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Linq;

using MfGames.Culture.Calendars.Calculations;
using MfGames.Culture.Calendars.Cycles;
using MfGames.Culture.Calendars.Lengths;

namespace MfGames.Culture.Calendars
{
	public class CalendarSystemValidator
	{
		#region Fields

		private readonly List<string> messages;

		#endregion

		#region Constructors and Destructors

		public CalendarSystemValidator()
		{
			messages = new List<string>();
		}

		#endregion

		#region Public Methods and Operators

		public bool IsValid(CalendarSystem calendar)
		{
			// Make sure the calendar has an ID.
			if (string.IsNullOrWhiteSpace(calendar.Id))
			{
				messages.Add("Calendar must have an Id defined.");
			}

			// Loop through the cycles and validate each one.
			foreach (Cycle cycle in calendar.Cycles)
			{
				var seen = new HashSet<string>();

				ValidateCycle(cycle, seen);
			}

			// It is valid if we have no messages.
			return messages.Count == 0;
		}

		public override string ToString()
		{
			return string.Join(Environment.NewLine, messages);
		}

		#endregion

		#region Methods

		private void AddMissingReference(
			string id,
			string reference,
			HashSet<string> seen)
		{
			messages.Add(
				id + ": The reference (" + reference
					+ ") is not defined in a parent cycle ["
					+ string.Join(", ", seen.ToList().OrderBy(s => s).ToArray())
					+ "].");
		}

		private void ValidateCalculatedCycle(
			CalculatedCycle cycle,
			HashSet<string> seen)
		{
			if (cycle == null)
			{
				return;
			}

			var div = cycle.Calculation as DivCycleCalculation;
			var mod = cycle.Calculation as ModCycleCalculation;

			if (div != null && !seen.Contains(div.ElementRef))
			{
				AddMissingReference(cycle.Id, div.ElementRef, seen);
			}

			if (mod != null && !seen.Contains(mod.ElementRef))
			{
				AddMissingReference(cycle.Id, mod.ElementRef, seen);
			}
		}

		private void ValidateCycle(Cycle cycle, HashSet<string> seen)
		{
			// Make sure the cycle has an ID.
			if (string.IsNullOrWhiteSpace(cycle.Id))
			{
				messages.Add("All cycles must have an Id defined.");
			}

			// Create a hash with this ID.
			var cycleSeen = new HashSet<string>(seen);

			cycleSeen.Add(cycle.Id);

			// Validate specific cycle types.
			ValidateLengthCycle(cycle as LengthCycle, cycleSeen);
			ValidateCalculatedCycle(cycle as CalculatedCycle, cycleSeen);

			// Loop through the child cycles.
			foreach (Cycle childCycle in cycle.Cycles)
			{
				ValidateCycle(childCycle, cycleSeen);
			}
		}

		private void ValidateIfModLengthLogic(
			IfModLengthLogic logic,
			string id,
			HashSet<string> seen)
		{
			if (logic == null)
			{
				return;
			}

			if (!seen.Contains(logic.DividendRef))
			{
				AddMissingReference(id, logic.DividendRef, seen);
			}
		}

		private void ValidateLengthCycle(LengthCycle cycle, HashSet<string> seen)
		{
			if (cycle == null)
			{
				return;
			}

			foreach (CycleLength length in cycle.Lengths)
			{
				ValidLogicCycleLength(
					length as LogicCycleLength,
					cycle.Id,
					seen);
			}
		}

		private void ValidLogicCycleLength(
			LogicCycleLength length,
			string id,
			HashSet<string> seen)
		{
			if (length == null)
			{
				return;
			}

			foreach (ILengthLogic lengthLogic in length.LengthLogics)
			{
				ValidateIfModLengthLogic(
					lengthLogic as IfModLengthLogic,
					id,
					seen);
			}
		}

		#endregion
	}
}
