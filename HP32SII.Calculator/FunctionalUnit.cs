using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Logic.Test")]

namespace HP32SII.Logic
{ 
    internal sealed class FunctionalUnit
    {
        public double ChangeSign(double x)
        {
            return -x;
        }

        public double Invert(double x)
        {
            return 1.0 / x;
        }

        public double Square(double x)
        {
            return x * x;
        }

        public double SquareRoot(double x)
        {
            return Math.Sqrt(x);
        }

        public double PowerOfTen(double x)
        {
            return Math.Pow(10, x);
        }

        public double Exponential(double x)
        {
            return Math.Exp(x);
        }

        public double NaturalLogarithm(double x)
        {
            return Math.Log(x);
        }

        public double LogBase10(double x)
        {
            return Math.Log10(x);
        }

        public double Factorial(double x)
        {
            if (x < 0)
                return double.NaN;
            if (x == 0)
                return 1.0;

            int n = (int)Math.Floor(x);
            var range = Enumerable.Range(1, n);
            return (double) range.Aggregate((a, b) => a * b);
        }
    }
}
