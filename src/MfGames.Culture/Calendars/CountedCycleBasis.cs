// <copyright file="CountedCycleBasis.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;
    using System.Collections.Generic;

    public class CountedCycleBasis : Basis
    {
        public CountedCycleBasis(string id, ClosedCycle cycle)
            : base(id)
        {
            Cycle = cycle;
            BasisLengthLogics = new BasisLengthLogicCollection();
        }

        public CountedCycleBasis(string id, ClosedCycle cycle, int count)
            : this(id, cycle)
        {
            var logic = new CountBasisLengthLogic(count);
            BasisLengthLogics.Add(logic);
        }

        public ClosedCycle Cycle { get; set; }
        public BasisLengthLogicCollection BasisLengthLogics { get; set; }

        public override void CalculateIndex(CalculateIndexArguments args)
        {
            // Loop through the number of elements inside this basis.
            int count = GetCount(Calendar.Variables, args.Elements);
            decimal beginningOfCycle = args.JulianDate;

            args.ExceededCycle = true;
            args.Elements[Id] = 0;

            for (var i = 0; i < count; i++)
            {
                // Keep track of the beginning of the cycle.
                beginningOfCycle = args.JulianDate;

                // If we have a zero or less JD, then we are done.
                if (args.JulianDate <= 0m)
                {
                    args.ExceededCycle = false;
                    break;
                }

                // Calculate the index.
                args.Elements[Id] = i;
                Cycle.CalculateIndex(args);

                // If we have exactly 0 and we exceeded our cycle, we have to bump up the ID.
                if (args.JulianDate <= 0m && args.ExceededCycle)
                {
                    args.Elements[Id] = i + 1;
                }
            }

            // If we break out the loop, then reset the value for the parent cycles.
            if (args.ExceededCycle)
            {
                args.Elements[Id] = 0;
            }

            // Calculate the additional cycles.
            var args2 = new CalculateIndexArguments(
                args.Elements,
                beginningOfCycle);
            base.CalculateIndex(args2);
        }

        public override decimal GetLength(CalendarElementValueDictionary values)
        {
            // We have to figure out the length based on the values.
            int count = GetCount(Calendar.Variables, values);

            // Build up the length of the total amount.
            var length = 0.0m;

            for (var index = 0; index < count; index++)
            {
                values[Cycle.Id] = index;
                length += Cycle.GetLength(values);
            }

            // Return the resulting length.
            return length;
        }

        private int GetCount(
            Dictionary<string, object> variables,
            CalendarElementValueDictionary values)
        {
            // Loop through all the logic until we find one that resolve, which also
            // sets the "count" value.
            foreach (IBasisLengthLogic logic in BasisLengthLogics)
            {
                int count;

                if (logic.GetCount(variables, values, out count))
                {
                    return count;
                }
            }

            // If we loop through all the logic fields, then we have a problem
            // and can't calculate it.
            throw new Exception(
                "Cannot calculate counted cycles for " + Id + ".");
        }
    }
}
