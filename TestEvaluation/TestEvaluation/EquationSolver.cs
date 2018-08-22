using System;
using System.Linq;
using static System.Math;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public enum ElementType
{
    Num = 1,
    Unknown1 = 2,
    Unknown2 = 4,
    Plus,
    Minus,
    Multiple,
    Division,
    LeftBracket,
    RightBracket,
    Equals
};

// TODO 25, 28 

public class EquationSolver // Refactor | Move
{
    public (float?, float?) Calc(string args)
    {
        var expression = args;
        var parsedExpression = new List<(string print, ElementType type, float? value, int index)>();

        
        // search for X^2
        string pattern = @"[Xx]\^2";
        MatchCollection matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            var element = (print: match.Value, type: ElementType.Unknown2, value: 1, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }

        expression = expression.Replace("X^2", "");
        expression = expression.Replace("x^2", "");
        
        // search for X
        pattern = @"[Xx](?!\^)"; // exclue ^2
//        pattern = @"[X,x]\b?[^\^]"; // exclue ^2
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            var element = (print: "X", type: ElementType.Unknown1, value: 1, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine("Unknown '{0}' found at position {1}.",
                match.Value, match.Index);
        }
        
        // search for digits
        pattern = @"(?<!\^)[0-9]+";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            var element = (print: match.Value, type: ElementType.Num, value: float.Parse(match.Value),
                index: match.Index);
            parsedExpression.Add(element);
            Console.WriteLine("Digit: '{0}' found at position {1}.",
                match.Value, match.Index);
        }

        // search for *
        pattern = @"\*";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            (string print, ElementType type, float? value, int index) element
                = (print: match.Value, type: ElementType.Multiple, value: null, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }

        // search for /
        pattern = @"\/";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            (string print, ElementType type, float? value, int index) element
                = (print: match.Value, type: ElementType.Division, value: null, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }

        // search for +
        pattern = @"\+";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            (string print, ElementType type, float? value, int index) element
                = (print: match.Value, type: ElementType.Plus, value: null, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }

        // search for -
        pattern = @"[\-,–]";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            (string print, ElementType type, float? value, int index) element
                = (print: match.Value, type: ElementType.Minus, value: null, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }

        // search for =
        pattern = @"\=";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            (string print, ElementType type, float? value, int index) element
                = (print: match.Value, type: ElementType.Equals, value: null, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }

        // search for (
        pattern = @"\(";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            (string print, ElementType type, float? value, int index) element
                = (print: match.Value, type: ElementType.LeftBracket, value: null, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }

        // search for )
        pattern = @"\)";
        matches = Regex.Matches(expression, pattern);
        foreach (Match match in matches)
        {
            (string print, ElementType type, float? value, int index) element
                = (print: match.Value, type: ElementType.RightBracket, value: null, index: match.Index);
            parsedExpression.Add(element);

            Console.WriteLine($"'{match.Value}' found at position {match.Index}.");
        }


        parsedExpression = parsedExpression.OrderBy(o => o.index).ToList();


        // -(
        // - 2
        // - X
        // - =???, -)
        // 预处理
        // 3X
        
        
        // invert minus，去括号前做一遍，去完再做一遍
        for (int i = parsedExpression.Count - 1; i >=0 ; i--)
        {
            var element = parsedExpression[i];
            if (element.type == ElementType.Minus && i < parsedExpression.Count - 1)
            {
                var nextElement = parsedExpression[i + 1];
                // TODO: enum byte
                if (nextElement.type == ElementType.Num ||
                    nextElement.type == ElementType.Unknown1 ||
                    nextElement.type == ElementType.Unknown2)
                {
                    // TODO: update print
                    parsedExpression[i + 1] = ("-" + nextElement.print, nextElement.type, -nextElement.value,
                        nextElement.index);
                    if (i > 0)
                    {
                        // if * - OR / -
                        var lastElement = parsedExpression[i - 1];
                        if (lastElement.type == ElementType.Multiple ||
                            lastElement.type == ElementType.Division)
                        {
                        parsedExpression.RemoveAt(i);
                        }
                        else
                        {
                            parsedExpression[i] = ("+", ElementType.Plus, null, element.index);
                        }
                            
                    }

                }

//                else if (nextElement.type == ElementType.LeftBracket)
//                {
//                    
//                }
            }
            
            // deal with a*X like 2X 
            if (i>0 && (element.type == ElementType.Unknown1 || element.type == ElementType.Unknown2))
            {
                var lastElement = parsedExpression[i - 1];
                if (parsedExpression[i - 1].type == ElementType.Num)
                {
                    parsedExpression[i] = (lastElement.print + element.print, element.type, lastElement.value.Value,
                        lastElement.index);
                    parsedExpression.RemoveAt(i-1);
                }
            }
        }

        Console.WriteLine(string.Join("\n", parsedExpression));


        Console.WriteLine("start:");
        foreach (var item in parsedExpression)
        {
            Console.Write(item.print + " ");
        }

        Console.WriteLine("\nbrackets removed:");

        // TODO
//        parsedExpression.FindAll()
        // or put into RE part

        // record index of brackets
        var bracketLocations = new List<int>();
        for (int i = 0; i < parsedExpression.Count; i++)
        {
            if (parsedExpression[i].type == ElementType.LeftBracket ||
                parsedExpression[i].type == ElementType.RightBracket)
            {
                bracketLocations.Add(i);
            }
        }

        var expandedExpression = new List<(string print, ElementType type, float? value, int index)>();

        // Remove brackets
        for (int i = 0; i + 1 < bracketLocations.Count; i = i + 2)
        {
            var leftBracketIndex = bracketLocations[i];
            var rightBracketIndex = bracketLocations[i + 1];

            // 2(, not start with (
            if (leftBracketIndex > 0)
            {
                var lastElement = parsedExpression[leftBracketIndex - 1];
                // 3(
                if (lastElement.type == ElementType.Num)
                {
                    // update the factors by multipled by the outside factor
                    for (int j = leftBracketIndex + 1; j < rightBracketIndex; j=j+2)
                    {
                        // TODO
//                        parsedExpression[j].value = lastElement.value * parsedExpression[j].value;
                        var updatedValue = lastElement.value * parsedExpression[j].value;

                        var updatedPrintValue = (parsedExpression[j].type == ElementType.Num)
                            ? updatedValue.ToString()
                            : parsedExpression[j].print;

                        ElementType updatedType;
                         
                        switch ((int) lastElement.type * (int) parsedExpression[j].type)
                        {
                            case 1:
                                updatedType = ElementType.Num;
                                break;
                            case 2:
                                updatedType = ElementType.Unknown1;
                                break;
                            case 4:
                                updatedType = ElementType.Unknown2;
                                break;
                            default:
                                updatedType =  parsedExpression[j].type;
                                break;
                        }
                        
                        
                        parsedExpression[j] = (updatedPrintValue, updatedType, updatedValue,
                            parsedExpression[j].index);
                    }

                    // 不查后面还有没有括号，直接去括号。
                    parsedExpression[rightBracketIndex] = ("", ElementType.RightBracket, null,
                        parsedExpression[rightBracketIndex].index);
                    parsedExpression[rightBracketIndex] = ("", ElementType.LeftBracket, null,
                        parsedExpression[leftBracketIndex].index);
                    // TODO remove?
//                    parsedExpression.RemoveAt(rightBracketIndex);
//                    parsedExpression.RemoveAt(leftBracketIndex);
                    // remove the factor before the bracket
                    if ((leftBracketIndex - 1) >=0)
                    {
                        parsedExpression[leftBracketIndex-1] = ("", ElementType.Num, 0,
                            parsedExpression[leftBracketIndex-1].index);
//                        parsedExpression.RemoveAt(leftBracketIndex-1);
                    }
                   
                }

                // )(
            }

            // start with ( 一定后面有且仅有一组
//            if (leftBracketIndex == 0)
//            {
            // TODO: +1 out of range
            // if has next pair of ()
            if (parsedExpression[rightBracketIndex + 1].type == ElementType.LeftBracket)
            {
                for (int j = leftBracketIndex + 1; j < rightBracketIndex; j++)
                {
                    var op1 = parsedExpression[j];
                    for (int k = bracketLocations[i + 2] + 1; k < bracketLocations[i + 3]; k++)
                    {
                        var op2 = parsedExpression[k];
                        var updatedValue = op1.value * op2.value;
                        ElementType updatedType;
                        switch ((int) op1.type * (int) op2.type)
                        {
                            case 1:
                                updatedType = ElementType.Num;
                                break;
                            case 2:
                                updatedType = ElementType.Unknown1;
                                break;
                            case 4:
                                updatedType = ElementType.Unknown2;
                                break;
                            default:
                                updatedType = op2.type;
                                break;
                        }

                        // TODO: index
                        var product = (print: "unknown", type: updatedType, value: updatedValue, leftBracketIndex: 0);
                        if (product.value != null)
                        {
                            expandedExpression.Add(product);
                        }
                    }
                }

//                }
                RemoveRange(parsedExpression, leftBracketIndex, bracketLocations[i + 3]);

                void RemoveRange(List<(string print, ElementType type, float? value, int index)> list, int from, int to)
                {
                    for (int m = from; m <= to; m++)
                    {
                        list.RemoveAt(from);
                    }
                }

                parsedExpression.InsertRange(leftBracketIndex, expandedExpression);
                i = i + 2;
            }
        }

        foreach (var item in parsedExpression)
        {
            Console.Write(item.value + " ");
        }


        // invert minus，去括号前做一遍，去完再做一遍
        for (int i = 0; i < parsedExpression.Count; i++)
        {
            var element = parsedExpression[i];
            if (element.type == ElementType.Minus && i < parsedExpression.Count - 1)
            {
                var nextElement = parsedExpression[i + 1];
                // TODO: enum byte
                if (nextElement.type == ElementType.Num ||
                    nextElement.type == ElementType.Unknown1 ||
                    nextElement.type == ElementType.Unknown2)
                {
                    // TODO: update print
                    parsedExpression[i + 1] = ("-" + nextElement.print, nextElement.type, -nextElement.value,
                        nextElement.index);
                    parsedExpression[i] = ("+", ElementType.Plus, null, element.index);
                }
            }
        }

        // Invert Division
        for (int i = 0; i < parsedExpression.Count; i++)
        {
            var element = parsedExpression[i];
            if (element.type == ElementType.Division && i < parsedExpression.Count - 1)
            {
                var nextElement = parsedExpression[i + 1];
                // TODO: enum byte
                if (nextElement.type == ElementType.Num ||
                    nextElement.type == ElementType.Unknown1 ||
                    nextElement.type == ElementType.Unknown2)
                {
                    // TODO: update print
                    parsedExpression[i + 1] = ("1/" + nextElement.print, nextElement.type, 1 / nextElement.value,
                        nextElement.index);
                    parsedExpression[i] = ("/", ElementType.Multiple, null, element.index);
                }
            }
        }

        // Multiple
        for (int i = parsedExpression.Count - 1; i >= 0; i--)
        {
            var element = parsedExpression[i];
            if (element.type == ElementType.Multiple)
            {
                var nextElement = parsedExpression[i + 1];
                var lastElement = parsedExpression[i - 1];
                ElementType newType;
                switch ((int) lastElement.type * (int) nextElement.type)
                {
                    case 1:
                        newType = ElementType.Num;
                        break;
                    case 2:
                        newType = ElementType.Unknown1;
                        break;
                    case 4:
                        newType = ElementType.Unknown2;
                        break;
                    default:
                        newType = nextElement.type;
                        break;
                }

                var product = lastElement.value * nextElement.value;
                parsedExpression[i - 1] = (product.ToString(), newType, product, lastElement.index);
                parsedExpression.RemoveAt(i + 1);
                parsedExpression.RemoveAt(i);
            }
        }


// Solve the equation
        var factorsA = new List<float>();
        var factorsB = new List<float>();
        var factorsC = new List<float>();

        // move all the items to left of the equals
        var toLeft = false;

        foreach (var val in parsedExpression)
        {
            if (val.type == ElementType.Equals)
            {
                toLeft = true;
            }

            if (val.type == ElementType.Num)
            {
                factorsC.Add(toLeft ? -val.value.Value : val.value.Value);
            }

            if (val.type == ElementType.Unknown1)
            {
                factorsB.Add(toLeft ? -val.value.Value : val.value.Value);
            }

            if (val.type == ElementType.Unknown2)
            {
                factorsA.Add(toLeft ? -val.value.Value : val.value.Value);
            }
        }

        var A = factorsA.Sum(); // item => item
        var B = factorsB.Sum();
        var C = factorsC.Sum();

        float? result1 = null;
        float? result2 = null;
        if (A == 0)
        {
            result1 = -(float) (C / B);
        }
        else if ((B * B - 4 * A * C) >= 0)
        {
            result1 = (float) ((-B + Sqrt(B * B - 4 * A * C)) / 2 / A);
            result2 = (float) ((-B - Sqrt(B * B - 4 * A * C)) / 2 / A);
        }

        return (result1, result2);
    }
}