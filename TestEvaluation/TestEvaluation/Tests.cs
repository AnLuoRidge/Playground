using System;
using NUnit.Framework;

namespace TestEvaluation
{
    [TestFixture]
    public class Tests
    {
        private EquationSolver _equationSolver = new EquationSolver();
        public QuadraticEquation _quadraticEquation = new QuadraticEquation();

        [Test]
        public void Test1()
        {
            UnknownShouldBe((1, null), "X + 2 = 3");
        }
        
        [Test]
        public void Test2()
        {
            UnknownShouldBe((1, -11), "(X + 5)(X + 5) = 36");
        }
        
        [Test]
        public void Test3()
        {
            UnknownShouldBe((1, -4), "(X + 1)(X + 2) = 6");
        }
        
        [Test]
        public void Test4()
        {
            UnknownShouldBe((3, null), "X - 1 = 2");
        }
        
        [Test]
        public void Test5()
        {
            UnknownShouldBe((2, null), "X * 2 = 4");
        }
        
        [Test]
        public void Test6()
        {
            UnknownShouldBe((8, null), "X / 2 = 4");
        }
        
        [Test]
        public void Ass1()
        {
            UnknownShouldBe((4, null), "X + 2 = 6");
        }
        
        [Test]
        public void Ass2()
        {
            UnknownShouldBe((5, null), "3X - 6 = 9");
        }
        
        [Test]
        public void Ass3()
        {
            UnknownShouldBe(((float)3.75, null), "X = 5*X – 5 * 3");
        }
        
        [Test]
        public void Ass4()
        {
            UnknownShouldBe((1, null), "5(2) + 5x = 15");
        }
        
        [Test]
        public void Ass5()
        {
            UnknownShouldBe((30, null), "X/5=6");
        }
        
        [Test]
        public void Ass6()
        {
            UnknownShouldBe((-3, -4), "x^2 + 7 * x + 6 * 2 = 0");
        }
        
        [Test]
        public void Ass7()
        {
            UnknownShouldBe((3, 2), "(X-2) (X-3) = 0");
        }
        
        [Test]
        public void Ass8()
        {
            UnknownShouldBe((4, null), "4(4X) + 2 (X) = 72");
        }
        
        [Test]
        public void Ass9()
        {
            UnknownShouldBe((13, null), "2(X-1)+8=4*X-20");
        }
        
        [Test]
        public void Ass10()
        {
            UnknownShouldBe((3, -3), "4x^2 – 11 * 2 = x^2 + 5");
        }
        
        [Test]
        public void Ass11()
        {
            UnknownShouldBe((71, null), "X = 5 + 22 * 3");
        }
        
        [Test]
        public void Ass12()
        {
            UnknownShouldBe((71, null), "X = 5 + 22 * 3");
        }
        
        [Test]
        public void Ass13()
        {
            UnknownShouldBe(((float)5/3, null), "3X = 5");
        }
        
        [Test]
        public void Ass14()
        {
            UnknownShouldBe(((float)5/3, null), "5 = 3X");
        }
        
        [Test]
        public void Ass15()
        {
            UnknownShouldBe((8, null), "5 + 3 = X");
        }
        
        [Test]
        public void Ass16()
        {
            UnknownShouldBe((-3, null), "X = -6 + 3");
        }

        [Test]
        public void Ass17()
        {
            UnknownShouldBe((-3, null), "X = 3 + -6");
        }
        
        [Test]
        public void Ass18()
        {
            UnknownShouldBe((32, null), "24 = 6X / 8");
        }
        
        [Test]
        public void Ass19()
        {
            UnknownShouldBe((-1, null), "6 / 3 = 5 + 3X");
        }

        [Test]
        public void Ass20()
        {
            UnknownShouldBe((11, null), "6 + 10 / 2 = X");
        }
        
        [Test]
        public void Ass21()
        {
            UnknownShouldBe((-1, null), "X = 2 + 3X");
        }

        [Test]
        public void Ass22()
        {
            UnknownShouldBe((6, null), "2X = 4X - 12");
        }
        
        [Test]
        public void Ass23()
        {
            UnknownShouldBe((null, null), "24 = 6X / 3X");
        }
        
        [Test]
        public void Ass24()
        {
            UnknownShouldBe((4, null), "X = (5 + 3) / 2");
        }
        
        [Test]
        public void Ass25()
        {
            UnknownShouldBe(((float)-1.75, null), "5 = 3(X+2)");
        }
        
        [Test]
        public void Ass26()
        {
            UnknownShouldBe((13, null), "6 = X + -7");
        }
                             
        [Test]
        public void Ass27()
        {
            UnknownShouldBe((-40, null), "X = 5 * -8");
        }
        [Test]
        public void Ass28()
        {
            UnknownShouldBe((11, null), "4 = 2 ( X – 3 )");
        }
        [Test]
        public void Ass29()
        {
            UnknownShouldBe((5, null), "X = 3 + 12 / 6 +");
        }
        [Test]
        public void Ass30()
        {
            UnknownShouldBe((2, null), "X = 14 - - - 8X + 4");
        }
        [Test]
        public void Self1()
        {
            UnknownShouldBe((2, null), "X = 14 - - - 8X + 4");
        }
//        
//        [Test]
//        public void Ass31()
//        {
//            UnknownShouldBe((11, null), "6 + 10 / 2 = X");
//        }
//        [Test]
//        public void Ass32()
//        {
//            UnknownShouldBe((11, null), "6 + 10 / 2 = X");
//        }
//        [Test]
//        public void Ass33()
//        {
//            UnknownShouldBe((11, null), "6 + 10 / 2 = X");
//        }
//        [Test]
//        public void Ass34()
//        {
//            UnknownShouldBe((11, null), "6 + 10 / 2 = X");
//        }
//        [Test]
//        public void Ass35()
//        {
//            UnknownShouldBe((11, null), "6 + 10 / 2 = X");
//        }
        
        
        

//        UnknownShouldBe((3, null), "X-1=2");// X 后面什么都不匹配

//        UnknownShouldBe((1, null), "2(X + 2) = 6");

        //            UnknownShouldBe(1, "(X + 2)(X - 3) + 4 / 2 = 3*X^2");

        private void UnknownShouldBe((float?, float?) expected, string expression)
        {
            Assert.AreEqual(expected, _equationSolver.Calc(expression));
        }
    }
}