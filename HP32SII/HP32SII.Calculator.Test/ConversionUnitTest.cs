using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HP32SII.Calculator.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    class ConversionUnitTest
    {
        private ConversionUnit conversionUnit = new ConversionUnit();

        [Test]
        public void TestToInch()
        {
            Assert.AreEqual(2.0, conversionUnit.ToInch(5.08));
        }

        [Test]
        public void TestToCentimeter()
        {
            Assert.AreEqual(5.08, conversionUnit.ToCentimeter(2.0));
        }

        [Test]
        public void TestToGallon()
        {
            Assert.AreEqual(2.0, conversionUnit.ToGallon(7.570823568));
        }

        [Test]
        public void TestToLiter()
        {
            Assert.AreEqual(7.570823568, conversionUnit.ToLiter(2.0));
        }

        [Test]
        public void TestToFahrenheit()
        {
            Assert.AreEqual(212.0, conversionUnit.ToFahrenheit(100.0));
        }

        [Test]
        public void TestToCelsius()
        {
            Assert.AreEqual(100.0, conversionUnit.ToCelsius(212.0));
        }

        [Test]
        public void TestToPound()
        {
            Assert.AreEqual(2.0, conversionUnit.ToPound(0.90718474));
        }

        [Test]
        public void TestToKilo()
        {
            Assert.AreEqual(0.90718474, conversionUnit.ToKilo(2.0));
        }

        [Test]
        public void TestToDegree()
        {
            Assert.AreEqual(180, conversionUnit.ToDegree(Math.PI));
        }

        [Test]
        public void TestToRadian()
        {
            Assert.AreEqual(Math.PI, conversionUnit.ToRadian(180));
        }
    }
}
