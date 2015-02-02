// <copyright file="IfBasisLengthLogic.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)

namespace MfGames.Culture.Calendars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using MfGames.Text;

    public class IfBasisLengthLogic : IBasisLengthLogic
    {
        static IfBasisLengthLogic()
        {
            OperationRegex = new Regex(
                @"^\s*(.*?)\s+(mod|in)\s+(.*?)\s*$",
                RegexOptions.IgnoreCase);
            macros = new MacroExpansion("$(", ")");
        }

        public static MacroExpansion macros;

        public string Operation { get; set; }
        public string Value { get; set; }

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
            Test = match.Groups[1].Value;
            Operation = match.Groups[2].Value.ToLower();
            Value = match.Groups[3].Value;
        }

        private static readonly Regex OperationRegex;

        public bool GetCount(
            Dictionary<string, object> variables,
            CalendarElementValueDictionary values,
            out int count)
        {
            // Test our expression to see if we will run any of these elements.
            if (!IsTrue(variables, values))
            {
                count = 0;
                return false;
            }

            // Loop through all the logic until we find one that resolve, which also
            // sets the "count" value.
            foreach (IBasisLengthLogic logic in BasisLengthLogics)
            {
                if (logic.GetCount(variables, values, out count))
                {
                    return true;
                }
            }

            // If we loop through all the logic fields and still haven't found it
            // then return false to indicate that we can't process it.
            count = 0;
            return false;
        }

        private bool IsTrue(
            Dictionary<string, object> variables,
            CalendarElementValueDictionary values)
        {
            // Pull out the variables.
            string text = macros.Expand(Test, values).Trim();
            string value = macros.Expand(Value, variables);

            // Resolve the text value.
            int textValue = Convert.ToInt32(text);

            // The operation determines how we process the values.
            bool result;

            switch (Operation)
            {
                case "mod":
                    int valueValue = Convert.ToInt32(value);
                    result = textValue == valueValue
                        || textValue % valueValue == 0;
                    break;

                case "in":
                    IEnumerable<int> valueValues =
                        value.Split(',').Select(t => Convert.ToInt32(t));
                    result = valueValues.Contains(textValue);
                    break;

                default:
                    throw new Exception(
                        "Cannot identify test operation: " + Operation + ".");
            }

            // Return the result.
            return result;
        }

        public IfBasisLengthLogic()
        {
            BasisLengthLogics = new BasisLengthLogicCollection();
        }

        public BasisLengthLogicCollection BasisLengthLogics { get; set; }

        public IfBasisLengthLogic(string expression, int count)
            : this()
        {
            // Parse the expressions.
            ParseExpression(expression);

            // Add in the count results for the block.
            var logic = new CountBasisLengthLogic(count);
            BasisLengthLogics.Add(logic);
        }

        public IfBasisLengthLogic(
            string expression,
            params IBasisLengthLogic[] logics)
            : this()
        {
            // Parse the expressions.
            ParseExpression(expression);

            // Add the various logic elements for the block.
            BasisLengthLogics.AddRange(logics);
        }

        public string Test { get; set; }
    }
}
