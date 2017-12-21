using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Logic.Test")]

namespace HP32SII.Logic
{
    internal class Output
    {
        private const string PositivePrefix = " ";
        private const string NegativePrefix = "-";
        private const string EditableSuffix = "_";
        private const string FrozenSuffix = "";
        private const string DefaultNumber = "0";

        private const int MaxNumberLength = 12;

        private bool isPositive = true;
        private string number;
        private bool isEditable = true;

        public void FromString(string output)
        {
            if (output[0] == PositivePrefix[0])
            {
                isPositive = true;
            }
            else if (output[0] == NegativePrefix[0])
            {
                isPositive = false;
            }
            else
            {
                throw new InvalidOperationException("Sign must be ' ' or '-'");
            }

            if (output[output.Length - 1] == EditableSuffix[0])
            {
                isEditable = true;
                number = output.Substring(1, output.Length - 2);
            }
            else
            {
                isEditable = false;
                number = output.Substring(1, output.Length - 1);
            }
        }

        public void Clear()
        {
            isPositive = true;
            isEditable = false;
            number = DefaultNumber;
        }

        public void Append(string digit)
        {
            if (isEditable)
            {
                if (number.Length < MaxNumberLength)
                {
                    number += digit;
                }
            }
            else
            {
                isPositive = true;
                isEditable = true;
                number = digit;
            }
        }

        public void Backspace()
        {
            if (!isEditable || number.Length == 1)
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
            if (number != DefaultNumber || isEditable)
            {
                isPositive = !isPositive;
            }
        }

        public void Freeze()
        {
            isEditable = false;
        }

        public override string ToString()
        {
            return (isPositive ? PositivePrefix : NegativePrefix) + number + (isEditable ? EditableSuffix : FrozenSuffix);
        }

        public double ToDouble()
        {
            return Convert.ToDouble(number) * (isPositive ? 1 : -1);
        }
    }
}
