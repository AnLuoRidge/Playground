using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestEvaluation
{
    [TestFixture]
    public class Tests
    {
//        private EquationSolver _equationSolver = new EquationSolver();

        [Test]
        public void CombineDigits1()
        {
            var exp = ExpressionParser.CombineDigits(new List<Element>
            {
                new Element(ElementType.Num, 7, "7"), 
                new Element(ElementType.Num, 2, "2"),
                new Element(ElementType.Multiple, 0, "x"),
                new Element(ElementType.RightBracket, 0, "("),
                new Element(ElementType.RightBracket, 0, "5"),
            });
            var expection = new List<Element>()
            {
                new Element(ElementType.Num, 72, "72"), 
                new Element(ElementType.Multiple, 0, "x"),
                new Element(ElementType.RightBracket, 0, "("),
                new Element(ElementType.RightBracket, 0, "5"),
            };
            Assert.AreEqual(exp, expection);
        }
        
        [Test]
        public void CombineDigitsShouldUnchangeSingleDigits()
        {
            var exp = ExpressionParser.CombineDigits(new List<Element>
            {
                new Element(ElementType.Unknown, 1, "X"), 
                new Element(ElementType.Plus, 0, "+"),
                new Element(ElementType.Num, 2, "2"),
                new Element(ElementType.Equals, 0, "="),
                new Element(ElementType.Num, 6, "6"),
            });
            var expection = new List<Element>()
            {
                new Element(ElementType.Unknown, 1, "X"), 
                new Element(ElementType.Plus, 0, "+"),
                new Element(ElementType.Num, 2, "2"),
                new Element(ElementType.Equals, 0, "="),
                new Element(ElementType.Num, 6, "6"),
            };
            Assert.AreEqual(exp, expection);
        }
                [Test]
        public void A1()
        {
            UnknownShouldBe(4, "X+2=6");
        }
                [Test]
        public void A2()
        {
            UnknownShouldBe(5, "3X-6=9");
        }
                [Test]
        public void A3()
        {
            UnknownShouldBe(3.75f, "X = 5*X – 5 * 3");
        }
                [Test]
        public void A4()
        {
            UnknownShouldBe(1, "5(2) + 5x = 15");
        }
                [Test]
        public void A5()
        {
            UnknownShouldBe(30, "X/5=6");
        }
                [Test]
        public void A6()
        {
            UnknownShouldBe(2, "(X-2) = 0");
        }

                [Test]
        public void A7()
        {
            UnknownShouldBe(4, "4(4X) + 2(X) = 72");
        }

                        [Test]
        public void A8()
        {
            UnknownShouldBe(14, "2X + 8 = 4X - 20");
        }

                                [Test]
        public void A9()
        {
            UnknownShouldBe(2, "X = 14 - - - 8X + 4");
        }
                                [Test]
        public void A10()
        {
            UnknownShouldBe(5, "4 = 2 ( X – 3 )");
        }
                                [Test]
        public void A11()
        {
            UnknownShouldBe(-1, "X = 2 + 3X");
        }
                                [Test]
        public void A12()
        {
            UnknownShouldBe(6, "2X = 4X - 12");
        }
                                [Test]
        public void A13()
        {
            UnknownShouldBe(null, "24 = 6X / 3X");
        }
                                [Test]
        public void A14()
        {
            UnknownShouldBe(4, "X = (5 + 3) / 2");
        }

        [Test]
             public void A15()
        {
            UnknownShouldBe(-1/3f, "5 = 3(X+2)");
        }
        [Test]
                public void A16()
        {
            UnknownShouldBe(32, "24 = 6X / 8");
        }
        [Test]
                public void A17()
        {
            UnknownShouldBe(-1, "6 / 3 = 5 + 3X");
        }
        [Test]
                public void A18()
        {
            UnknownShouldBe(11, "6 + 10 / 2 = X");
        }
        [Test]
                public void A19()
        {
            UnknownShouldBe(71, "X = 5 + 22 * 3");
        }
        [Test]
                public void A20()
        {
            UnknownShouldBe(-3, "X = -6 + 3");
        }
        [Test]
                public void A21()
        {
            UnknownShouldBe(5f/3, "5 = 3X");
        }
        [Test]
                public void A22()
        {
            UnknownShouldBe(-3, "X = 3 + -6");
        }

        private void UnknownShouldBe(float? expected, string expression)
        {
            Assert.AreEqual(expected, EquationSolver.Calc(expression));
        }
    }
}