using System;
using System.Collections.Generic;
using System.Linq;

namespace TestEvaluation
{
    public enum ElementType
    {
        Num = 1,
        Unknown,
        ReciprocalUnknown,
        Plus,
        Minus,
        Multiple,
        Division,
        LeftBracket,
        RightBracket,
        Equals
    };

    public struct Element
    {
        public readonly ElementType Type;
        public readonly float? Value;
        public readonly string Print;

        public Element(ElementType type, float? value, string print)
        {
            Type = type;
            Value = value;
            Print = print;
        }
    }

    public class ExpressionParser
    {
        public static List<Element> ParseCharacters(string exp)
        {
            var parsedExpression = new List<Element>();
            // parse single char
            foreach (var ch in exp.ToCharArray())
            {
                // Num
                int num;
                if (int.TryParse(ch.ToString(), out num))
                {
                    parsedExpression.Add(new Element(ElementType.Num, num, ch.ToString()));
                }

                // Unkown Number
                if (char.IsLetter(ch))
                {
                    parsedExpression.Add(new Element(ElementType.Unknown, 1, ch.ToString()));
                }

                switch (ch)
                {
                    case '=':
                        parsedExpression.Add(new Element(ElementType.Equals, null, ch.ToString()));
                        break;
                    case '+':
                        parsedExpression.Add(new Element(ElementType.Plus, null, ch.ToString()));
                        break;
                    case '-':
                    case '–':
                        parsedExpression.Add(new Element(ElementType.Minus, null, ch.ToString()));
                        break;
                    case '*':
                        parsedExpression.Add(new Element(ElementType.Multiple, null, ch.ToString()));
                        break;
                    case '/':
                        parsedExpression.Add(new Element(ElementType.Division, null, ch.ToString()));
                        break;
                    case '(':
                        parsedExpression.Add(new Element(ElementType.LeftBracket, null, ch.ToString()));
                        break;
                    case ')':
                        parsedExpression.Add(new Element(ElementType.RightBracket, null, ch.ToString()));
                        break;
                }
            }

            return parsedExpression;
        }

        public static void CheckIntegrity(List<Element> exp)
        {
            var hasUnknown = false;
            var hasEquals = false;
            var hasNumbers = false;
            foreach (var item in exp)
            {
                switch (item.Type)
                {
                    case ElementType.Unknown:
                        hasUnknown = true;
                        break;
                    case ElementType.Equals:
                        hasEquals = true;
                        break;
                    case ElementType.Num:
                        hasNumbers = true;
                        break;
                }
            }

            if (!hasUnknown)
            {
                throw (new UnknownNotFouldException("Unknown number not found!"));
            }

            if (!hasEquals)
            {
                throw (new EqualsNotFoundException("Equals not found!"));
            }

            if (!hasNumbers)
            {
                throw (new NumbersNotFoundException("Numbers not found!"));
            }
        }

        // deal with a*X, such as 2X 
        public static List<Element> CombineCoefficientToUnknown(List<Element> exp)
        {
            var parsedExpression = exp;
            for (var i = parsedExpression.Count - 1; i >= 0; i--)
            {
                var element = parsedExpression[i];
                var hasPrevious = i > 0;
                var isUnknownNum = element.Type == ElementType.Unknown;
                if (hasPrevious && isUnknownNum)
                {
                    var lastElement = parsedExpression[i - 1];
                    if (lastElement.Type == ElementType.Num)
                    {
                        parsedExpression[i] = new Element(element.Type, lastElement.Value,
                            lastElement.Print + element.Print);
                        parsedExpression.RemoveAt(i - 1);
                    }
                }
            }

            return parsedExpression;
        }

        private static int? TryParseInt(string digits)
        {
            try
            {
                return int.Parse(digits);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<Element> CombineDigits(List<Element> exp)
        {
            var combinedExp = new List<Element>();
            var combinedDigits = string.Empty;

            for (var i = 0; i < exp.Count; i++)
            {
                // add up digits
                if (exp[i].Type == ElementType.Num)
                {
                    combinedDigits += exp[i].Print;
                    var isEnd = (i == exp.Count - 1);
                    // if last one is a digit, add combinedDigits to exp
                    if (isEnd)
                    {
                        var num = TryParseInt(combinedDigits);
                        combinedExp.Add(new Element(ElementType.Num, num, combinedDigits));
                    }
                }
                else
                {
                    // if it's not number, add previous combined digits
                    if (combinedDigits.Length > 0)
                    {
                        var num = TryParseInt(combinedDigits);
                        combinedExp.Add(new Element(ElementType.Num, num, combinedDigits));
                    }

                    combinedExp.Add(exp[i]);
                    combinedDigits = "";
                }
            }

            return combinedExp;
        }

        // if the same operator appears more than twice
        public static List<Element> RemoveRedundantOperators(List<Element> expression)
        {
            var exp = expression;
            for (var i = exp.Count - 1; i >= 0; i--)
            {
                var type = exp[i].Type;
                var hasPrevious = i > 0;
                if (hasPrevious)
                {
                    var lastType = exp[i - 1].Type;
                    if (type == lastType)
                    {
                        exp.RemoveAt(i);
                    }
                }
            }

            return exp;
        }

        public static List<Element> RevertMinus(List<Element> exp)
        {
            var parsedExpression = exp;

            for (var i = parsedExpression.Count - 1; i >= 0; i--)
            {
                var element = parsedExpression[i];
                var hasNext = i < parsedExpression.Count - 1;
                if (element.Type == ElementType.Minus && hasNext)
                {
                    var nextNum = parsedExpression[i + 1];
                    if (nextNum.Type == ElementType.Num ||
                        nextNum.Type == ElementType.Unknown)
                    {
                        // reverse value
                        parsedExpression[i + 1] = new Element(nextNum.Type, -nextNum.Value, "-" + nextNum.Print);
                        // deal with minus symbol
                        if (i > 0)
                        {
                            // if * - OR / -, remove '-'
                            var lastElement = parsedExpression[i - 1];
                            if (lastElement.Type == ElementType.Multiple ||
                                lastElement.Type == ElementType.Division)
                            {
                                parsedExpression.RemoveAt(i);
                            }
                            // replace '-' with '+'
                            else
                            {
                                parsedExpression[i] = new Element(ElementType.Plus, null, "+");
                            }
                        }
                    }
                }
            }

            return parsedExpression;
        }

        private static float SafeDivide(float a, float b)
        {
            if (b == 0f)
            {
                throw (new DivideByZeroException());
            }
            else
            {
                return a / b;
            }
        }

        // update the terms via multipled by the outside coefficient
        private static List<Element> MultipleCoefficientIntoBrackets(float coefficent, List<Element> exp,
            int leftBracketIndex, int rightBracketIndex)
        {
            var parsedExpression = exp;

            for (var j = leftBracketIndex + 1; j < rightBracketIndex; j = j + 2)
            {
                var updatedValue = coefficent * parsedExpression[j].Value;

                string updatedPrintValue;
                if (parsedExpression[j].Type == ElementType.Num)
                {
                    updatedPrintValue = updatedValue.ToString();
                }
                else if (parsedExpression[j].Type == ElementType.Unknown)
                {
                    updatedPrintValue = updatedValue + "X";
                }
                else
                {
                    updatedPrintValue = parsedExpression[j].Print;
                }

                ElementType updatedType = parsedExpression[j].Type;
                parsedExpression[j] = new Element(updatedType, updatedValue, updatedPrintValue);
            }

            parsedExpression.RemoveAt(rightBracketIndex);
            parsedExpression.RemoveAt(leftBracketIndex);
            // remove the coefficient before the bracket
            parsedExpression.RemoveAt(leftBracketIndex - 1);
            return parsedExpression;
        }

        private static List<Element> BlockDividedByUnknown(float divisor, List<Element> exp,
            int leftBracketIndex, int rightBracketIndex)
        {
            var parsedExpression = exp;
            // update by divided by the coefficient
            for (var j = leftBracketIndex + 1; j < rightBracketIndex; j = j + 2)
            {
                var preciproal = SafeDivide(1, divisor);
                var updatedValue = preciproal * parsedExpression[j].Value;

                string updatedPrintValue;
                if (parsedExpression[j].Type == ElementType.Num)
                {
                    updatedPrintValue = updatedValue.ToString();
                }
                else if (parsedExpression[j].Type == ElementType.Unknown)
                {
                    updatedPrintValue = updatedValue + "X";
                }
                else
                {
                    updatedPrintValue = parsedExpression[j].Print;
                }

                parsedExpression[j] =
                    new Element(parsedExpression[j].Type, updatedValue, updatedPrintValue);
            }

            parsedExpression.RemoveAt(rightBracketIndex + 2);
            parsedExpression.RemoveAt(rightBracketIndex + 1);
            parsedExpression.RemoveAt(rightBracketIndex);
            parsedExpression.RemoveAt(leftBracketIndex);
            return parsedExpression;
        }

        public static List<Element> RemoveBrackets(List<Element> exp)
        {
            var parsedExpression = exp;

            // record index of brackets
            var bracketLocations = new List<int>();
            for (var i = 0; i < parsedExpression.Count; i++)
            {
                if (parsedExpression[i].Type == ElementType.LeftBracket ||
                    parsedExpression[i].Type == ElementType.RightBracket)
                {
                    bracketLocations.Add(i);
                }
            }

            // remove the brackets
            for (var i = bracketLocations.Count - 2; i >= 0; i = i - 2)
            {
                var leftBracketIndex = bracketLocations[i];
                var rightBracketIndex = bracketLocations[i + 1];

                if (leftBracketIndex > 0)
                {
                    var termBeforeBracket = parsedExpression[leftBracketIndex - 1];

                    if (termBeforeBracket.Type == ElementType.Num)
                    {
                        // 1. Handle a( which index 0 is not (
                        parsedExpression = MultipleCoefficientIntoBrackets(
                            termBeforeBracket.Value.Value,
                            parsedExpression,
                            leftBracketIndex,
                            rightBracketIndex);
                    }
                    else if (termBeforeBracket.Type == ElementType.Division)
                    {
                        throw (new Exception("Can't handle X/(...) for now."));
                    }
                }

                // 2. handle (...) / X
                if (rightBracketIndex < parsedExpression.Count - 2)
                {
                    if (exp[rightBracketIndex + 1].Type == ElementType.Division)
                    {
                        var divisor = exp[rightBracketIndex + 2];
                        if (divisor.Type == ElementType.Num)
                        {
                            BlockDividedByUnknown(
                                divisor.Value.Value,
                                parsedExpression,
                                leftBracketIndex,
                                rightBracketIndex);
                        }
                    }
                }
            }

            return parsedExpression;
        }

        public static List<Element> Divide(List<Element> exp)
        {
            var parsedExpression = exp;
            for (int i = 0; i < parsedExpression.Count; i++)
            {
                var element = parsedExpression[i];
                if (element.Type == ElementType.Division && i < parsedExpression.Count - 1)
                {
                    var nextElement = parsedExpression[i + 1];
                    if (nextElement.Type == ElementType.Num ||
                        nextElement.Type == ElementType.Unknown)
                    {
                        var updatedValue = SafeDivide(1, nextElement.Value.Value);
                        if (nextElement.Type == ElementType.Unknown)
                        {
                            parsedExpression[i + 1] = new Element(ElementType.ReciprocalUnknown, updatedValue,
                                "1/" + nextElement.Print);
                        }
                        else
                        {
                            parsedExpression[i + 1] = new Element(nextElement.Type, updatedValue,
                                "1/" + nextElement.Print);
                        }

                        parsedExpression[i] = new Element(ElementType.Multiple, null, "*");
                    }
                }
            }

            return parsedExpression;
        }

        private static ElementType IdentifyTermsTypeAfterProduction(ElementType type1, ElementType type2)
        {
            switch ((int) type1 * (int) type2)
            {
                case 1:
                case 6:
                    return ElementType.Num;
                    break;
                case 2:
                    return ElementType.Unknown;
                    break;
                case 3:
                    return ElementType.ReciprocalUnknown;
                    break;
                default:
                    throw (new Exception("Fail to recognise the type of the production."));
                    break;
            }
        }

        public static List<Element> Multiple(List<Element> exp)
        {
            var parsedExpression = exp;
            for (int i = parsedExpression.Count - 1; i >= 0; i--)
            {
                var element = parsedExpression[i];
                if (element.Type == ElementType.Multiple)
                {
                    var nextElement = parsedExpression[i + 1];
                    var lastElement = parsedExpression[i - 1];
                    var product = lastElement.Value * nextElement.Value;
                    ElementType newType = IdentifyTermsTypeAfterProduction(lastElement.Type, nextElement.Type);
                    parsedExpression[i - 1] = new Element(newType, product, product.ToString());

                    parsedExpression.RemoveAt(i + 1);
                    parsedExpression.RemoveAt(i);
                }
            }

            return parsedExpression;
        }

        public static float? SolveEquation(List<Element> exp)
        {
            var coefficientX = new List<float>();
            var coefficient = new List<float>();

            var toLeft = false;

            foreach (var term in exp)
            {
                if (term.Type == ElementType.Equals)
                {
                    toLeft = true;
                }

                if (term.Type == ElementType.Num)
                {
                    coefficient.Add(toLeft ? -term.Value.Value : term.Value.Value);
                }

                if (term.Type == ElementType.Unknown)
                {
                    coefficientX.Add(toLeft ? -term.Value.Value : term.Value.Value);
                }
            }

            // Check the existance of X and number
            if (coefficient.Count > 0 && coefficientX.Count > 0)
            {
                var b = coefficientX.Sum();
                var c = coefficient.Sum();

                var solution = -SafeDivide(c, b);
                return solution;
            }
            else
            {
                return null;
            }
        }

        public static void PrintExpressionWhen(string leading, List<Element> exp)
        {
            Console.WriteLine("\n" + leading);
            foreach (var item in exp)
            {
                Console.Write(item.Print + " ");
            }

            Console.WriteLine("");
        }
    }
}

public class UnknownNotFouldException : Exception
{
    public UnknownNotFouldException(string message) : base(message)
    {
    }
}

public class EqualsNotFoundException : Exception
{
    public EqualsNotFoundException(string message) : base(message)
    {
    }
}

public class NumbersNotFoundException : Exception
{
    public NumbersNotFoundException(string message) : base(message)
    {
    }
}
