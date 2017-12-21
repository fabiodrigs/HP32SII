using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Logic.Test")]

namespace HP32SII.Logic
{

    public sealed class Calculator
	{
        public double LastX { get; private set; }

        private StackUnit stackUnit = new StackUnit();
        private FunctionalUnit functionalUnit = new FunctionalUnit();
        private StorageUnit storageUnit = new StorageUnit();
        private ConversionUnit conversionUnit = new ConversionUnit();

        #region Stack features

        public double Add(double x)
        {
            return stackUnit.Add(x);
        }

        public double Subtract(double x)
        {
            return stackUnit.Subtract(x);
        }

        public double Multiply(double x)
        {
            return stackUnit.Multiply(x);
        }

        public double Divide(double x)
        {
            return stackUnit.Divide(x);
        }

        public double Power(double x)
        {
            return stackUnit.Power(x);
        }

        public double Swap(double x)
        {
            return stackUnit.Swap(x);
        }

        public void Push(double x)
        {
            stackUnit.Push(x);
        }

        public void Clear()
        {
            stackUnit.Clear();
        }

        #endregion

        #region Functional features

        public double ChangeSign(double x)
        {
            return functionalUnit.ChangeSign(x);
        }

        public double Invert(double x)
        {
            return functionalUnit.Invert(x);
        }

        public double Square(double x)
        {
            return functionalUnit.Square(x);
        }

        public double SquareRoot(double x)
        {
            return functionalUnit.SquareRoot(x);
        }

        public double PowerOfTen(double x)
        {
            return functionalUnit.PowerOfTen(x);
        }

        public double Exponential(double x)
        {
            return functionalUnit.Exponential(x);
        }

        public double NaturalLogarithm(double x)
        {
            return functionalUnit.NaturalLogarithm(x);
        }

        public double LogBase10(double x)
        {
            return functionalUnit.LogBase10(x);
        }

        public double Factorial(double x)
        {
            return functionalUnit.Factorial(x);
        }
        #endregion

        #region Conversion features
        public double ToInch(double centimeter)
        {
            return conversionUnit.ToInch(centimeter);
        }

        public double ToCentimeter(double inch)
        {
            return conversionUnit.ToCentimeter(inch);
        }

        public double ToGallon(double liter)
        {
            return conversionUnit.ToGallon(liter);
        }

        public double ToLiter(double gallon)
        {
            return conversionUnit.ToLiter(gallon);
        }

        public double ToFahrenheit(double celsius)
        {
            return conversionUnit.ToFahrenheit(celsius);
        }

        public double ToCelsius(double fahrenheit)
        {
            return conversionUnit.ToCelsius(fahrenheit);
        }

        public double ToPound(double kilo)
        {
            return conversionUnit.ToPound(kilo);
        }

        public double ToKilo(double pound)
        {
            return conversionUnit.ToKilo(pound);
        }

        public double ToDegree(double radian)
        {
            return conversionUnit.ToDegree(radian);
        }

        public double ToRadian(double degree)
        {
            return conversionUnit.ToRadian(degree);
        }
        #endregion

        #region Storage features
        public void Store(char key, double value)
        {
            storageUnit.Store(key, value);
        }

        public double Recall(char key)
        {
            return storageUnit.Recall(key);
        }

        public void ClearAll()
        {
            storageUnit.ClearAll();
        }
        #endregion
    }
}
