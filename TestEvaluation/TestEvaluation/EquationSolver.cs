using System;
using static TestEvaluation.ExpressionParser;


namespace TestEvaluation
{
    public class EquationSolver
    {
        public static float? Main(string args)
        {
            Console.WriteLine("Input: " + args);

            var noSpaceEexpression = args.Replace(" ", "");
            var exp = ParseCharacters(noSpaceEexpression);
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
            return SolveEquation(exp);
        }
    }
}
