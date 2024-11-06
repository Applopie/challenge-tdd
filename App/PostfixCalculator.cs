using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public static class PostfixCalculator
    {
        private static string[] operations = new[] { "+", "-", "*" };

        public static string Calculate(string postfixExpression)
        {
            if (postfixExpression == null)
                throw new FormatException();
            if (postfixExpression == string.Empty)
                return LongComplex.Zero.ToString();

            var tokens = postfixExpression.Split(' ');
            var stack = new Stack<string>();
            foreach (var token in tokens)
            {
                if (operations.Contains(token))
                {
                    var operation = token;
                    if (stack.Count < 2)
                        throw new FormatException();
                    var second = stack.Pop();
                    var first = stack.Pop();
                    var calculated = (Calculate(token, int.Parse(second), int.Parse(first))).ToString(); 
                    stack.Push(calculated);
                }
                else
                {
                    stack.Push(token);
                }
            }
            if (stack.Count != 1)
                throw new FormatException();
            var result = stack.Pop();
            if (int.TryParse(result, out _))
                return result.ToString();
            else
                throw new FormatException();
        }

        private static int Calculate(string operation, int operand1, int operand2)
        {
            if (operation == "+")
                return operand1 + operand2;
            if (operation == "-")
                return operand2 - operand1;
            if (operation == "*")
                return operand1*operand2;
            throw new FormatException();
        }
    }
}