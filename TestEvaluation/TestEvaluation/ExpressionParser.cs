using System;
//using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace TestEvaluation
{
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

                // Unown
                if (char.IsLetter(ch))
                {
                    parsedExpression.Add(new Element(ElementType.Unknown, 1, ch.ToString()));
                }

                // =
                if (ch == '=')
                {
                    parsedExpression.Add(new Element(ElementType.Equals, null, ch.ToString()));
                }

                // +
                if (ch == '+')
                {
                    parsedExpression.Add(new Element(ElementType.Plus, null, ch.ToString()));
                }

                // -
                if (ch == '-' || ch == '–')
                {
                    parsedExpression.Add(new Element(ElementType.Minus, null, ch.ToString()));
                }

                // *
                if (ch == '*')
                {
                    parsedExpression.Add(new Element(ElementType.Multiple, null, ch.ToString()));
                }

                // /
                if (ch == '/')
                {
                    parsedExpression.Add(new Element(ElementType.Division, null, ch.ToString()));
                }

                // (
                if (ch == '(')
                {
                    parsedExpression.Add(new Element(ElementType.LeftBracket, null, ch.ToString()));
                }

                // )
                if (ch == ')')
                {
                    parsedExpression.Add(new Element(ElementType.RightBracket, null, ch.ToString()));
                }
            }

            return parsedExpression;
        }

        // deal with a*X, such as 2X 
        public static List<Element> CombineCoefficientToUnknown(List<Element> exp)
        {
            var parsedExpression = exp;
            for (int i = parsedExpression.Count - 1; i >= 0; i--)
            {
                var element = parsedExpression[i];
                if (i > 0 && (element.Type == ElementType.Unknown))
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

        public static List<Element> CombineDigits(List<Element> exp)
        {
            var combinedExp = new List<Element>();
            var combinedDigits = string.Empty;

            for (var i = 0; i < exp.Count; i++)
            {
                if (exp[i].Type == ElementType.Num)
                {
                    combinedDigits += exp[i].Print;
                    if (i == exp.Count - 1)
                    {
                        int num;
                        try
                        {
num = int.Parse(combinedDigits);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        combinedExp.Add(new Element(ElementType.Num, num, combinedDigits));
                    }
                }
                else
                {
                    if (combinedDigits.Length > 0)
                    {
                        int num;
                        try
                        {
                            num = int.Parse(combinedDigits);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        combinedExp.Add(new Element(ElementType.Num, num, combinedDigits));
                    }

                    combinedExp.Add(exp[i]);
                    combinedDigits = "";
                }
            }

            return combinedExp;
        }

        public static List<Element> RevertMinus(List<Element> exp)
        {
            var parsedExpression = exp;

            for (int i = parsedExpression.Count - 1; i >= 0; i--)
            {
                var element = parsedExpression[i];
                if (element.Type == ElementType.Minus) //  exception: && i < parsedExpression.Count - 1
                {
                    var nextNum = parsedExpression[i + 1];
                    if (nextNum.Type == ElementType.Num ||
                        nextNum.Type == ElementType.Unknown)
                    {
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

            for (var i = bracketLocations.Count - 2; i >= 0; i = i - 2)
            {
                var leftBracketIndex = bracketLocations[i];
                var rightBracketIndex = bracketLocations[i + 1];

                // Handle 2(, but index 0 is not (
                if (leftBracketIndex > 0)
                {
                    var coeffcientBeforeBracket = parsedExpression[leftBracketIndex - 1];
                    // if there is a coefficient before (
                    if (coeffcientBeforeBracket.Type == ElementType.Num)
                    {
                        // update the factors by multipled by the outside coefficient
                        for (var j = leftBracketIndex + 1; j < rightBracketIndex; j = j + 2)
                        {
                            var updatedValue = coeffcientBeforeBracket.Value * parsedExpression[j].Value;

                            // TODO: DELETE - update printValue
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

                            // TODO: X (), ()/X
                            ElementType updatedType = parsedExpression[j].Type;
                            // if X (...) OR if 0 (...)
//                        switch ((int) lastElement.Type * (int) parsedExpression[j].Type)
//                        {
//                            case 1:
//                                updatedType = ElementType.Num;
//                                break;
//                            case 2:
//                                updatedType = ElementType.Unknown;
//                                break;
//                            default:
//                                updatedType =  parsedExpression[j].Type;
//                                break;
//                        }
//                        
                            parsedExpression[j] = new Element(updatedType, updatedValue, updatedPrintValue);
                        }

                        // if (...) / X OR X / X

                        // 不查后面还有没有括号，直接去括号。
//                    parsedExpression[leftBracketIndex] = new Element(ElementType.RightBracket, null, string.Empty);
//                    parsedExpression[rightBracketIndex] = new Element(ElementType.LeftBracket, null, string.Empty);
                        // TODO remove?
                        parsedExpression.RemoveAt(rightBracketIndex);
                        parsedExpression.RemoveAt(leftBracketIndex);

                        // remove the coefficient before the bracket
//                        parsedExpression[leftBracketIndex - 1] =
//                            new Element(ElementType.Num, 0, ""); // diff? string.empty
                        parsedExpression.RemoveAt(leftBracketIndex-1);
                    }
                }
                
                // (...) / ?
                if (rightBracketIndex < parsedExpression.Count - 2)
                {
                    if (exp[rightBracketIndex + 1].Type == ElementType.Division)
                    {
                        var coefficentAfterBracket = exp[rightBracketIndex + 2].Value;
                        // update by divided by the coefficient
                        for (var j = leftBracketIndex + 1; j < rightBracketIndex; j = j + 2)
                        {
                            var updatedValue = 1 / coefficentAfterBracket.Value * parsedExpression[j].Value;

                            // TODO: DELETE - update printValue
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

                            // TODO: X (), ()/X
                            ElementType updatedType = parsedExpression[j].Type;
                            // if X (...) OR if 0 (...)
//                        switch ((int) lastElement.Type * (int) parsedExpression[j].Type)
//                        {
//                            case 1:
//                                updatedType = ElementType.Num;
//                                break;
//                            case 2:
//                                updatedType = ElementType.Unknown;
//                                break;
//                            default:
//                                updatedType =  parsedExpression[j].Type;
//                                break;
//                        }
//                        
                            parsedExpression[j] = new Element(updatedType, updatedValue, updatedPrintValue);
                        }

                        // if (...) / X OR X / X

                        parsedExpression.RemoveAt(rightBracketIndex + 2);
                        parsedExpression.RemoveAt(rightBracketIndex + 1);
                        // TODO remove?
                        parsedExpression.RemoveAt(rightBracketIndex);
                        parsedExpression.RemoveAt(leftBracketIndex);
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
                    // TODO: enum byte
                    if (nextElement.Type == ElementType.Num ||
                        nextElement.Type == ElementType.Unknown)
                    {
                        float? updatedValue;
                        try
                        {
                            updatedValue = 1 / (int) nextElement.Value;
                        }
                        catch (DivideByZeroException e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        if (nextElement.Type == ElementType.Unknown)
                        {
                            parsedExpression[i + 1] = new Element(ElementType.ReciprocalUnknown, 1 / nextElement.Value,
                                "1/" + nextElement.Print);
                        }
                        else
                        {
                            parsedExpression[i + 1] = new Element(nextElement.Type, 1 / nextElement.Value,
                                "1/" + nextElement.Print);
                        }

                        parsedExpression[i] = new Element(ElementType.Multiple, null, "*");
                    }
                }
            }

            return parsedExpression;
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
                    ElementType updatedType;

                    switch ((int) lastElement.Type * (int) nextElement.Type)
                    {
                        case 1:
                        case 6:
                            updatedType = ElementType.Num;
                            parsedExpression[i - 1] = new Element(updatedType, product, product.ToString());
                            break;
                        case 2:
                            updatedType = ElementType.Unknown;
                            parsedExpression[i - 1] = new Element(updatedType, product, product + "X");
                            break;
                        case 3:
                            updatedType = ElementType.ReciprocalUnknown;
                            parsedExpression[i - 1] = new Element(updatedType, product, product + "1/X");
                            break;
                        default:
                            updatedType = nextElement.Type;
                            parsedExpression[i - 1] = new Element(updatedType, product, product.ToString());
                            break;
                    }

                    parsedExpression.RemoveAt(i + 1);
                    parsedExpression.RemoveAt(i);
                }
            }

            return parsedExpression;
        }

        public static List<Element> RemoveRedundantOperators(List<Element> exp)
        {
            // if the same operator appear more than twice
            return exp;
        }

        public static void PrintExpressionWhen(string leading, List<Element> exp)
        {
            Console.WriteLine(leading);
            foreach (var item in exp)
            {
                Console.Write(item.Print + " ");
            }
            Console.WriteLine("");
        }
    }
}