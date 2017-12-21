using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HP32SII.Logic.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    class FunctionalUnitTest
    {
        private FunctionalUnit functionalUnit;
        private const double x = 3.0;

        [SetUp]
        public void SetUp()
        {
            functionalUnit = new FunctionalUnit();
        }

        [Test]
        public void TestChangeSign()
        {
            var expected = -x;
            var result = functionalUnit.ChangeSign(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestInvert()
        {
            var expected = 1.0 / x;
            var result = functionalUnit.Invert(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestSquare()
        {
            var expected = x * x;
            var result = functionalUnit.Square(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestSquareRoot()
        {
            var expected = Math.Sqrt(x);
            var result = functionalUnit.SquareRoot(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestPowerOfTen()
        {
            var expected = Math.Pow(10, x);
            var result = functionalUnit.PowerOfTen(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestExponential()
        {
            var expected = Math.Exp(x);
            var result = functionalUnit.Exponential(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestNaturalLogarithm()
        {
            var expected = Math.Log(x);
            var result = functionalUnit.NaturalLogarithm(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestLogBase10()
        {
            var expected = Math.Log10(x);
            var result = functionalUnit.LogBase10(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestFactorialPositive()
        {
            var expected = 1 * 2 * 3 * 4;
            var result = functionalUnit.Factorial(4.5);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestFactorialNegative()
        {
            var expected = double.NaN;
            var result = functionalUnit.Factorial(-4.5);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestFactorialZero()
        {
            var expected = 1;
            var result = functionalUnit.Factorial(0);

            Assert.That(expected, Is.EqualTo(result));
        }
    }
}
