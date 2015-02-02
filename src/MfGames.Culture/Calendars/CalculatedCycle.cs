// <copyright file="CalculatedCycle.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;
    using System.Text.RegularExpressions;

    using MfGames.Text;

    public class CalculatedCycle : CalendarElement
    {
        public override bool IsValueElement { get { return true; } }

        public override void CalculateIndex(CalculateIndexArguments args)
        {
            // Convert the expression to an integer.
            string expanded = macros.Expand(Element, args.Elements);
            int element = Convert.ToInt32(expanded);

            // Perform the operation based on the two values.
            int results;

            switch (Operation)
            {
                case "mod":
                    results = element % Value;
                    break;

                case "div":
                    results = element / Value;
                    break;

                default:
                    throw new Exception(
                        "Cannot perform calculated index: " + Operation + ".");
            }

            // Set the result in the elements.
            args.Elements[Id] = results;

            // Calculate the additional cycles.
            base.CalculateIndex(args);
        }

        public CalculatedCycle(string id, string expression)
            : base(id)
        {
            ParseExpression(expression);
        }

        public string Operation { get; set; }
        public int Value { get; set; }

        private void ParseExpression(string expression)
        {
            // Make sure we have valid expressions.
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            // The expressions are simple "test operation value" with a limitation number of
            // operations.
            Match match = OperationRegex.Match(expression);

            if (!match.Success)
            {
                throw new ArgumentException(
                    string.Format("Cannot parse expression: {0}.", expression),
                    "expression");
            }

            // Pull out the elements.
            Element = match.Groups[1].Value;
            Operation = match.Groups[2].Value.ToLower();
            Value = Convert.ToInt32(match.Groups[3].Value);
        }

        public string Element { get; set; }

        private static readonly Regex OperationRegex;

        private static readonly MacroExpansion macros;

        static CalculatedCycle()
        {
            OperationRegex = new Regex(
                @"^\s*(.*?)\s+(mod|div)\s+(.*?)\s*$",
                RegexOptions.IgnoreCase);
            macros = new MacroExpansion("$(", ")");
        }
    }
}
