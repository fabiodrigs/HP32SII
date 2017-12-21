﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HP32SII.Logic.Test")]

namespace HP32SII.Logic
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

        public void Clear()
        {
            stack.Clear();
        }

        public double Peek()
        {
            return stack.Peek();
        }

        private double Pop()
        {
            return stack.Count == 0 ? 0.0 : stack.Pop();
        }

        #endregion
    }
}