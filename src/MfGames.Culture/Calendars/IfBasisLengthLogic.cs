// <copyright file="IfBasisLengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System.Collections.Generic;

    public class IfBasisLengthLogic : IBasisLengthLogic
    {
        public IfBasisLengthLogic()
        {
        }

        public IfBasisLengthLogic(string expression, int count)
        {
        }

        public IfBasisLengthLogic(
            string expression,
            params IBasisLengthLogic[] logics)
        {
        }

        public string Test { get; set; }
        public IList<IBasisLengthLogic> IfTrueLengthLogic { get; set; }
    }
}
