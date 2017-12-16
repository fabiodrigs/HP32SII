using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Calculator.Test")]

namespace HP32SII.Calculator
{
    internal sealed class StackUnit
    {
        Stack<double> stack = new Stack<double>();

        public double Add(double x)
        {
            return Pop() + x;
        }

        public double Subtract(double x)
        {
            return Pop() - x;
        }

        public double Multiply(double x)
        {
            return Pop() * x;
        }

        public double Divide(double x)
        {
            return x == 0.0 ? double.NaN : Pop() / x;
        }

        public double Power(double x)
        {
            return Math.Pow(Pop(), x);
        }

        public double Swap(double x)
        {
            var y = stack.Pop();
            stack.Push(x);
            return y;
        }

        #region Stack operations

        public void Push(double x)
        {
            stack.Push(x);
        }

        private double Pop()
        {
            return stack.Count == 0 ? 0.0 : stack.Pop();
        }

        public double Peek()
        {
            return stack.Peek();
        }

        public void Clear()
        {
            stack.Clear();
        }
        #endregion
    }
}
