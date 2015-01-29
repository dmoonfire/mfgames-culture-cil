// <copyright file="Basis.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    public abstract class Basis : CalendarElement
    {
        protected Basis(string id)
            : base(id)
        {
        }

        public abstract decimal GetLength(CalendarElementValueDictionary values);
    }
}
