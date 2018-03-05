using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HP32SII.Logic.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    class StackUnitTest
    {
        private StackUnit stackUnit;
        double y = 5.0;
        double x = 3.0;

        [SetUp]
        public void SetUp()
        {
            stackUnit = new StackUnit();
            stackUnit.Push(y);
        }

        [TearDown]
        public void TearDown()
        {
            stackUnit.Clear();
        }

        [Test]
        public void TestAddOneOperand()
        {
            stackUnit.Clear();
            Assert.That(x, Is.EqualTo(stackUnit.Add(x)));
        }

        [Test]
        public void TestAddTwoOperands()
        {
            var expected = y + x;
            var result = stackUnit.Add(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestSubtract()
        {
            var expected = y - x;
            var result = stackUnit.Subtract(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestMultiply()
        {
            var expected = y * x;
            var result = stackUnit.Multiply(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestDivide()
        {
            var expected = y / x;
            var result = stackUnit.Divide(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestDivideByZero()
        {
            var result = stackUnit.Divide(0.0);
            Assert.That(double.NaN, Is.EqualTo(result));
        }

        [Test]
        public void TestPower()
        {
            var expected = Math.Pow(y, x);
            var result = stackUnit.Power(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestRoot()
        {
            var expected = Math.Pow(y, x);
            var result = stackUnit.Power(x);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TestRootZero()
        {
            var result = stackUnit.Root(0.0);
            Assert.That(double.NaN, Is.EqualTo(result));
        }

        [Test]
        public void TestSwap()
        {
            var expected = y;
            var result = stackUnit.Swap(x);

            Assert.That(expected, Is.EqualTo(result));
            Assert.That(x, Is.EqualTo(stackUnit.Peek()));
        }
    }
}
