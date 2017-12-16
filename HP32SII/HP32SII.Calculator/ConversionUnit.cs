using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Calculator.Test")]

namespace HP32SII.Calculator
{
    internal sealed class ConversionUnit
    {
        private const double InchToCentimeter = 2.54;
        private const double GallonToLiter = 3.785411784;
        private const double TemperatureIntercept = 32.0;
        private const double TemperatureSlope = 9.0 / 5.0;
        private const double PoundToKilo = 0.45359237;

        public double ToInch(double centimeter)
        {
            return centimeter / InchToCentimeter;
        }

        public double ToCentimeter(double inch)
        {
            return inch * InchToCentimeter;
        }

        public double ToGallon(double liter)
        {
            return liter / GallonToLiter;
        }

        public double ToLiter(double gallon)
        {
            return gallon * GallonToLiter;
        }

        public double ToFahrenheit(double celsius)
        {
            return celsius * TemperatureSlope + TemperatureIntercept;
        }

        public double ToCelsius(double fahrenheit)
        {
            return (fahrenheit - TemperatureIntercept) / TemperatureSlope;
        }

        public double ToPound(double kilo)
        {
            return kilo / PoundToKilo;
        }

        public double ToKilo(double pound)
        {
            return pound * PoundToKilo;
        }

        public double ToDegree(double radian)
        {
            return radian / Math.PI * 180;
        }

        public double ToRadian(double degree)
        {
            return degree / 180 * Math.PI;
        }
    }
}
