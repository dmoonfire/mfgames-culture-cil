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

            for (var i = 0; i < count; i++)
            {
                // Set the index and pass the logic into the basis to calculate
                // the next one. We do this to also reset values.
                args.Elements[Id] = i;
                args.ExceededCycle = false;

                // If we have a zero or less JD, then we are done.
                if (args.JulianDate <= 0m)
                {
                    return;
                }

                // Calculate the index.
                Cycle.CalculateIndex(args);
            }

            // If we break out the loop, then reset the value for the parent cycles.
            args.Elements[Id] = 0;
            args.ExceededCycle = true;

            // Calculate the additional cycles.
            base.CalculateIndex(args);
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
