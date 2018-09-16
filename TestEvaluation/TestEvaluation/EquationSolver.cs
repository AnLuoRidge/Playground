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
        var exp = ParseCharacters(expression);
        exp = CombineDigits(exp);
        exp = RemoveRedundantOperators(exp);
        exp = RevertMinus(exp);
        exp = CombineCoefficientToUnknown(exp);
        CheckIntegrity(exp);
        PrintExpressionWhen("Parsed: ", exp);
        exp = RemoveBrackets(exp);
        PrintExpressionWhen("All brackets removed:", exp);
        exp = Divide(exp);
        PrintExpressionWhen("After division:", exp);
        exp = Multiple(exp);
        PrintExpressionWhen("After multiplication:", exp);

// Solve the equation
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
}
