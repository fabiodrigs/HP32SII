using System;

namespace HP32SII.Logic
{
    static class FuncExtensions
    {
        public static Func<TResult1> Compose<TParam2, TResult2, TResult1>(this Func<Func<TParam2, TResult2>, TResult1> func1, Func<TParam2, TResult2> func2)
        {
            return () => func1(func2);
        }

        public static Func<TResult> Compose<TParam, TResult>(this Func<TParam, TResult> func, TParam param)
        {
            return () => func(param);
        }
    }
}
