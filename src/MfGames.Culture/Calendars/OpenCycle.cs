// <copyright file="OpenCycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;

    /// <summary>
    /// Defines an open-ended cycle that doesn't have a containing cycle and continually
    /// increases without resetting or cycling.
    /// </summary>
    public class OpenCycle : Cycle
    {
        public OpenCycle(string id)
            : base(id)
        {
        }

        public OpenCycle(string id, Basis basis)
            : this(id)
        {
            if (basis == null)
            {
                throw new ArgumentNullException("basis");
            }

            Basis = basis;
        }
    }
}
