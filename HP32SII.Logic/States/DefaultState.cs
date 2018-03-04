using HP32SII.Logic.EscapeModes;
using System;

namespace HP32SII.Logic.States
{
    public sealed class DefaultState : State
    {
        public DefaultState() : base()
        {
            BottomStatus = "";
        }

        public static void AssignButtonOperations()
        {
            Func<Func<double, double>, State> monadic = MonadicOperation;
            Func<Func<double, double>, State> dyadic = DyadicOperation;
            Func<string, State> numeric = NumericKey;

            Buttons.Sqrt.DefaultOperation = monadic.Compose(calculator.SquareRoot);
            Buttons.Sqrt.LeftOperation = monadic.Compose(calculator.Square);
            Buttons.Sqrt.RightOperation = DoNothing;

            Buttons.Exp.DefaultOperation = monadic.Compose(calculator.Exponential);
            Buttons.Exp.LeftOperation = monadic.Compose(calculator.PowerOfTen);
            Buttons.Exp.RightOperation = monadic.Compose(calculator.PowerOfTen);

            Buttons.Ln.DefaultOperation = monadic.Compose(calculator.NaturalLogarithm);
            Buttons.Ln.LeftOperation = monadic.Compose(calculator.LogBase10);
            Buttons.Ln.RightOperation = monadic.Compose(calculator.LogBase10);

            Buttons.Pow.DefaultOperation = dyadic.Compose(calculator.Power);
            Buttons.Pow.LeftOperation = DoNothing;
            Buttons.Pow.RightOperation = DoNothing;

            Buttons.Invert.DefaultOperation = monadic.Compose(calculator.Invert);
            Buttons.Invert.LeftOperation = DoNothing;
            Buttons.Invert.RightOperation = monadic.Compose(calculator.Factorial);

            Buttons.Sum.DefaultOperation = DoNothing;
            Buttons.Sum.LeftOperation = DoNothing;
            Buttons.Sum.RightOperation = DoNothing;

            Buttons.Store.DefaultOperation = Store;
            Buttons.Store.LeftOperation = DoNothing;
            Buttons.Store.RightOperation = DoNothing;

            Buttons.Recall.DefaultOperation = Recall;
            Buttons.Recall.LeftOperation = DoNothing;
            Buttons.Recall.RightOperation = DoNothing;

            Buttons.RollDown.DefaultOperation = DoNothing;
            Buttons.RollDown.LeftOperation = DoNothing;
            Buttons.RollDown.RightOperation = DoNothing;

            Buttons.Sin.LeftOperation = DoNothing;
            Buttons.Sin.RightOperation = DoNothing;
            Buttons.Sin.DefaultOperation = DoNothing;

            Buttons.Cos.LeftOperation = DoNothing;
            Buttons.Cos.RightOperation = DoNothing;
            Buttons.Cos.DefaultOperation = DoNothing;

            Buttons.Tan.DefaultOperation = DoNothing;
            Buttons.Tan.LeftOperation = DoNothing;
            Buttons.Tan.RightOperation = DoNothing;

            Buttons.Enter.DefaultOperation = Enter;
            Buttons.Enter.LeftOperation = DoNothing;
            Buttons.Enter.RightOperation = DoNothing;

            Buttons.Swap.DefaultOperation = dyadic.Compose(calculator.Swap);
            Buttons.Swap.LeftOperation = DoNothing;
            Buttons.Swap.RightOperation = DoNothing;

            Buttons.ChangeSign.DefaultOperation = ChangeSign;
            Buttons.ChangeSign.LeftOperation = DoNothing;
            Buttons.ChangeSign.RightOperation = DoNothing;

            Buttons.E.LeftOperation = DoNothing;
            Buttons.E.DefaultOperation = DoNothing;
            Buttons.E.RightOperation = DoNothing;

            Buttons.Back.DefaultOperation = Backspace;
            Buttons.Back.LeftOperation = DoNothing;
            Buttons.Back.RightOperation = DoNothing;

            Buttons.Xeq.DefaultOperation = DoNothing;
            Buttons.Xeq.LeftOperation = DoNothing;
            Buttons.Xeq.RightOperation = DoNothing;

            Buttons.Seven.DefaultOperation = numeric.Compose(Buttons.Seven.Name);
            Buttons.Seven.LeftOperation = DoNothing;
            Buttons.Seven.RightOperation = DoNothing;

            Buttons.Eight.DefaultOperation = numeric.Compose(Buttons.Eight.Name);
            Buttons.Eight.LeftOperation = DoNothing;
            Buttons.Eight.RightOperation = DoNothing;

            Buttons.Nine.DefaultOperation = numeric.Compose(Buttons.Nine.Name);
            Buttons.Nine.LeftOperation = DoNothing;
            Buttons.Nine.RightOperation = DoNothing;

            Buttons.Divide.DefaultOperation = dyadic.Compose(calculator.Divide);
            Buttons.Divide.LeftOperation = DoNothing;
            Buttons.Divide.RightOperation = DoNothing;

            Buttons.Four.DefaultOperation = numeric.Compose(Buttons.Four.Name);
            Buttons.Four.LeftOperation = DoNothing;
            Buttons.Four.RightOperation = DoNothing;

            Buttons.Five.DefaultOperation = numeric.Compose(Buttons.Five.Name);
            Buttons.Five.LeftOperation = DoNothing;
            Buttons.Five.RightOperation = DoNothing;

            Buttons.Six.DefaultOperation = numeric.Compose(Buttons.Six.Name);
            Buttons.Six.LeftOperation = monadic.Compose(calculator.ToRadian);
            Buttons.Six.RightOperation = monadic.Compose(calculator.ToRadian);

            Buttons.Multiply.DefaultOperation = dyadic.Compose(calculator.Multiply);
            Buttons.Multiply.LeftOperation = DoNothing;
            Buttons.Multiply.RightOperation = DoNothing;

            Buttons.One.DefaultOperation = numeric.Compose(Buttons.One.Name);
            Buttons.One.LeftOperation = monadic.Compose(calculator.ToPound);
            Buttons.One.RightOperation = monadic.Compose(calculator.ToPound);

            Buttons.Two.DefaultOperation = numeric.Compose(Buttons.Two.Name);
            Buttons.Two.LeftOperation = monadic.Compose(calculator.ToFahrenheit);
            Buttons.Two.RightOperation = monadic.Compose(calculator.ToFahrenheit);

            Buttons.Three.DefaultOperation = numeric.Compose(Buttons.Three.Name);
            Buttons.Three.LeftOperation = monadic.Compose(calculator.ToInch);
            Buttons.Three.RightOperation = monadic.Compose(calculator.ToInch);

            Buttons.Subtract.DefaultOperation = dyadic.Compose(calculator.Subtract);
            Buttons.Subtract.LeftOperation = DoNothing;
            Buttons.Subtract.RightOperation = DoNothing;

            Buttons.Clear.DefaultOperation = Clear;
            Buttons.Clear.LeftOperation = ThrowException;   
            Buttons.Clear.RightOperation = ThrowException;

            Buttons.Zero.DefaultOperation = numeric.Compose(Buttons.Zero.Name);
            Buttons.Zero.LeftOperation = DoNothing;
            Buttons.Zero.RightOperation = DoNothing;

            Buttons.Dot.DefaultOperation = HandleDot;
            Buttons.Dot.LeftOperation = DoNothing;
            Buttons.Dot.RightOperation = DoNothing;

            Buttons.Solve.DefaultOperation = DoNothing;
            Buttons.Solve.LeftOperation = DoNothing;
            Buttons.Solve.RightOperation = DoNothing;

            Buttons.Add.DefaultOperation = dyadic.Compose(calculator.Add);
            Buttons.Add.LeftOperation = DoNothing;
            Buttons.Add.RightOperation = DoNothing;
        }

        private static State ThrowException()
        {
            throw new InvalidOperationException("Unexpected key");
        }

        private static State DoNothing()
        {
            return new DefaultState();
        }

        private static State Enter()
        {
            pushAtNextAppend = false;
            output.Freeze();
            calculator.Push(output.ToDouble());
            Display = output.ToString();
            return new DefaultState();
        }

        private static State ChangeSign()
        {
            output.ChangeSign();
            Display = output.ToString();
            if (!output.IsEditable)
            {
                pushAtNextAppend = true;
            }
            return new DefaultState();
        }

        private static State NumericKey(string button)
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.AppendDigit(button);
            Display = output.ToString();
            return new DefaultState();
        }

        private static State HandleDot()
        {
            if (pushAtNextAppend)
            {
                calculator.Push(output.ToDouble());
                pushAtNextAppend = false;
            }
            output.AppendDot();
            Display = output.ToString();
            return new DefaultState();
        }

        private static State Clear()
        {
            pushAtNextAppend = false;
            output.Clear();
            Display = output.ToString();
            return new DefaultState();
        }

        private static State Backspace()
        {
            pushAtNextAppend = false;
            output.Backspace();
            Display = output.ToString();
            return new DefaultState();
        }

        private static State MonadicOperation(Func<double, double> operation)
        {
            pushAtNextAppend = true;
            var result = operation(output.ToDouble());
            output.FromDouble(result);
            Display = output.ToString();
            return new DefaultState();
        }

        private static State DyadicOperation(Func<double, double> operation)
        {
            pushAtNextAppend = false;
            var result = operation(output.ToDouble());
            output.FromDouble(result);
            Display = output.ToString();
            return new DefaultState();
        }

        private static State Store()
        {
            if (output.IsEditable)
            {
                output.Freeze();
                pushAtNextAppend = true;
            }

            return new StoreState();
        }

        private static State Recall()
        {
            if (output.IsEditable)
            {
                output.Freeze();
            }
            calculator.Push(output.ToDouble());

            return new RecallState();
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            switch (escapeMode)
            {
                case NoEscapeMode mode:
                    return button.DefaultOperation();
                case LeftEscapeMode mode:
                    return button.LeftOperation();
                case RightEscapeMode mode:
                    return button.RightOperation();
                default:
                    throw new InvalidOperationException("Unexpected escape mode");
            }
        }
    }
}
