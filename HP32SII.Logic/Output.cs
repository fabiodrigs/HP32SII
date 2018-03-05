using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Logic.Test")]

namespace HP32SII.Logic
{
    public class Output
    {
        private const string PositivePrefix = " ";
        private const string NegativePrefix = "-";
        private const string EditableSuffix = "_";
        private const string FrozenSuffix = "";
        private const string DefaultNumber = "0";
        private const string Dot = ".";

        private const int MaxNumberLength = 12;

        public bool IsPositive { get; set; } = true;
        private string number;
        public bool IsEditable { get; private set; } = true;

        public Output()
        {
            Clear();
        }

        public void Clear()
        {
            IsPositive = true;
            IsEditable = false;
            number = DefaultNumber;
        }

        public void AppendDigit(string digit)
        {
            if (IsEditable)
            {
                if (number.Replace(".", "").Length < MaxNumberLength)
                {
                    number += digit;
                }
            }
            else
            {
                IsPositive = true;
                IsEditable = true;
                number = digit;
            }
        }

        public void AppendDot()
        {
            if (IsEditable)
            {
                if (!number.Contains("."))
                {
                    number += Dot;
                }
            }
            else
            {
                IsPositive = true;
                IsEditable = true;
                number = DefaultNumber + Dot;
            }
        }

        public void Backspace()
        {
            if (!IsEditable || number.Length == 1)
            {
                Clear();
            }
            else
            {
                number = number.Remove(number.Length - 1);
            }
        }

        public void ChangeSign()
        {
            if (number != DefaultNumber || IsEditable)
            {
                IsPositive = !IsPositive;
            }
        }

        public void Freeze()
        {
            IsEditable = false;
            TrimLeadingZeros();
        }

        private void TrimLeadingZeros()
        {
            number = int.Parse(number).ToString();
        }

        public void FromString(string output)
        {
            if (output[0] == PositivePrefix[0])
            {
                IsPositive = true;
            }
            else if (output[0] == NegativePrefix[0])
            {
                IsPositive = false;
            }
            else
            {
                throw new InvalidOperationException("Sign must be ' ' or '-'");
            }

            if (output[output.Length - 1] == EditableSuffix[0])
            {
                IsEditable = true;
                number = output.Substring(1, output.Length - 2);
            }
            else
            {
                IsEditable = false;
                number = output.Substring(1, output.Length - 1);
            }
        }

        public void FromDouble(double x)
        {
            IsPositive = x >= 0;
            number = Math.Abs(x).ToString();
            IsEditable = false;
        }

        public override string ToString()
        {
            return (IsPositive ? PositivePrefix : NegativePrefix) + number + (IsEditable ? EditableSuffix : FrozenSuffix);
        }

        public double ToDouble()
        {
            return Convert.ToDouble(number) * (IsPositive ? 1 : -1);
        }
    }
}
