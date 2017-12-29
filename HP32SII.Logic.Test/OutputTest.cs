using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace HP32SII.Logic.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    class OutputTest
    {
        private Output output = new Output();

        [SetUp]
        public void SetUp()
        {
            output.Clear();
        }

        #region Clear()

        [Test]
        public void TestClear()
        {
            output.Clear();

            Assert.That(output.ToString(), Is.EqualTo(" 0"));
            Assert.That(output.ToDouble(), Is.EqualTo(0.0));
        }
        #endregion
        #region AppendDigit()

        [Test]
        public void TestAppendDigitEditable()
        {
            output.FromString(" 123_");
            output.AppendDigit("4");

            Assert.That(output.ToString(), Is.EqualTo(" 1234_"));
            Assert.That(output.ToDouble(), Is.EqualTo(1234.0));
        }

        [Test]
        public void TestAppendDigitEditableMax()
        {
            output.FromString(" 123456789012_");
            output.AppendDigit("3");

            Assert.That(output.ToString(), Is.EqualTo(" 123456789012_"));
            Assert.That(output.ToDouble(), Is.EqualTo(123456789012.0));
        }

        [Test]
        public void TestAppendDigitEditableWithdot()
        {
            output.FromString(" 12345678901._");
            output.AppendDigit("3");

            Assert.That(output.ToString(), Is.EqualTo(" 12345678901.3_"));
            Assert.That(output.ToDouble(), Is.EqualTo(12345678901.3));
        }

        [Test]
        public void TestAppendDigitFrozen()
        {
            output.FromString(" 123");
            output.AppendDigit("4");

            Assert.That(output.ToString(), Is.EqualTo(" 4_"));
            Assert.That(output.ToDouble(), Is.EqualTo(4.0));
        }

        #endregion
        #region AppendDot()

        [Test]
        public void TestAppendDotEditable()
        {
            output.FromString(" 123_");
            output.AppendDot();

            Assert.That(output.ToString(), Is.EqualTo(" 123._"));
            Assert.That(output.ToDouble(), Is.EqualTo(123.0));
        }

        [Test]
        public void TestAppendDotFrozen()
        {
            output.FromString(" 123");
            output.AppendDot();

            Assert.That(output.ToString(), Is.EqualTo(" 0._"));
            Assert.That(output.ToDouble(), Is.EqualTo(0.0));
        }


        [Test]
        public void TestAppendDotAlreadyDot()
        {
            output.FromString(" 12.3_");
            output.AppendDot();

            Assert.That(output.ToString(), Is.EqualTo(" 12.3_"));
            Assert.That(output.ToDouble(), Is.EqualTo(12.3));
        }

        #endregion
        #region Backspace()

        [Test]
        public void TestBackspaceFrozen()
        {
            output.FromString(" 123");
            output.Backspace();

            Assert.That(output.ToString(), Is.EqualTo(" 0"));
            Assert.That(output.ToDouble(), Is.EqualTo(0.0));
        }

        [Test]
        public void TestBackspaceEditable()
        {
            output.FromString(" 123_");
            output.Backspace();

            Assert.That(output.ToString(), Is.EqualTo(" 12_"));
            Assert.That(output.ToDouble(), Is.EqualTo(12.0));
        }

        [Test]
        public void TestBackspaceMinSize()
        {
            output.FromString(" 1_");
            output.Backspace();

            Assert.That(output.ToString(), Is.EqualTo(" 0"));
            Assert.That(output.ToDouble(), Is.EqualTo(0.0));
        }
        #endregion
        #region ChangeSign()

        [Test]
        public void TestChangeSignZeroFrozen()
        {
            output.FromString(" 0");
            output.ChangeSign();

            Assert.That(output.ToString(), Is.EqualTo(" 0"));
            Assert.That(output.ToDouble(), Is.EqualTo(0.0));
        }

        [Test]
        public void TestChangeSignZeroEditable()
        {
            output.FromString(" 0_");
            output.ChangeSign();

            Assert.That(output.ToString(), Is.EqualTo("-0_"));
            Assert.That(output.ToDouble(), Is.EqualTo(0.0));
        }

        [Test]
        public void TestChangeSignPositiveFrozen()
        {
            output.FromString(" 123");
            output.ChangeSign();

            Assert.That(output.ToString(), Is.EqualTo("-123"));
            Assert.That(output.ToDouble(), Is.EqualTo(-123.0));
        }

        [Test]
        public void TestChangeSignPositiveEditable()
        {
            output.FromString(" 123_");
            output.ChangeSign();

            Assert.That(output.ToString(), Is.EqualTo("-123_"));
            Assert.That(output.ToDouble(), Is.EqualTo(-123.0));
        }

        [Test]
        public void TestChangeSignNegativeFrozen()
        {
            output.FromString("-123");
            output.ChangeSign();

            Assert.That(output.ToString(), Is.EqualTo(" 123"));
            Assert.That(output.ToDouble(), Is.EqualTo(123.0));
        }

        [Test]
        public void TestChangeSignNegativeEditable()
        {
            output.FromString("-123_");
            output.ChangeSign();

            Assert.That(output.ToString(), Is.EqualTo(" 123_"));
            Assert.That(output.ToDouble(), Is.EqualTo(123.0));
        }

        #endregion
        #region Freeze()

        [Test]
        public void Freeze()
        {
            output.FromString(" 123_");
            output.Freeze();

            Assert.That(output.ToString(), Is.EqualTo(" 123"));
            Assert.That(output.ToDouble(), Is.EqualTo(123.0));
        }
        #endregion
    }
}
