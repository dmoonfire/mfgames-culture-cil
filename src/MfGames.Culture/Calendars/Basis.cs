// <copyright file="Basis.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System.Collections.Generic;

    public class Basis
    {
        public Basis(string id, Cycle cycle, int count)
        {
            this.Id = id;
            this.Cycle = cycle;
            this.LengthLogic = new[] { new BasisLength(count) };
        }

        public static Basis JulianDay { get; private set; }

        /// <summary>
        /// Contains the unique identifier of the cycle.
        /// </summary>
        /// <remarks>
        /// This should be a space-separated, title cased name.
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// Contains the cycle that this basis is based on.
        /// </summary>
        public Cycle Cycle { get; set; }

        /// <summary>
        /// Contains an ordered list of IBasisLengthLogic elements used to calculate
        /// the length of the basis.
        /// </summary>
        public IList<IBasisLengthLogic> LengthLogic { get; set; }
    }
}
