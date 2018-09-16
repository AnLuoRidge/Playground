using System;
using System.Collections;
using System.Linq;
using static System.Math;
using System.Collections.Generic;
using TestEvaluation;
using static TestEvaluation.ExpressionParser;

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

public class EquationSolver // Refactor | Move
{
    public static float? Calc(string args)
    {
        Console.WriteLine("Input: " + args);

        var expression = args.Replace(" ", ""); // remove space(s)
        var parsedExpression = ParseCharacters(expression);
        parsedExpression = CombineDigits(parsedExpression);
        parsedExpression = RemoveRedundantOperators(parsedExpression);
//        ArrayList HandleExceptedOperators(ArrayList exp)
//        {
//            
//        }

        // -(
        // - 2
        // - X
        // - =???, -)
        // 预处理
        // 3X

        // TODO: multipleOperator

        // Handle minus -> -value，去括号前做一遍，去完再做一遍
        parsedExpression = RevertMinus(parsedExpression);
        parsedExpression = CombineCoefficientToUnknown(parsedExpression);

//        Console.WriteLine(string.Join("\n", parsedExpression));

        PrintExpressionWhen("Parsed: ", parsedExpression);

        parsedExpression = RemoveBrackets(parsedExpression);
        PrintExpressionWhen("\nAll brackets removed:", parsedExpression);

        parsedExpression = Divide(parsedExpression);
        PrintExpressionWhen("\nAfter division:", parsedExpression);
        parsedExpression = Multiple(parsedExpression);
        PrintExpressionWhen("\nAfter multiplication:", parsedExpression);

// Solve the equation
        var coefficientX = new List<float>();
        var coefficient = new List<float>();

        var toLeft = false;

        foreach (var term in parsedExpression)
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

            var solution = SafeDivide(c, b);
            return solution;
        }
        else
        {
            return null;
        }
    }
}
