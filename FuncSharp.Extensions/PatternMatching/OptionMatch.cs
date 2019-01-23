using FuncSharp.Monads;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuncSharp.Extensions.PatternMatching
{
    /// <summary>
    /// Match expression for Option<T>.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class OptionMatch<TData, TResult> : Match<Option<TData>, TResult>
    {
        public OptionMatch(Option<TData> data) : base(data)
        {
        }

        public OptionMatch<TData, TResult> IfSome(Func<TData, TResult> f) 
            => With(x => x is Some<TData>, () => f(_data as Some<TData>)) as OptionMatch<TData, TResult>;

        public OptionMatch<TData, TResult> IfNone(Func<TResult> f) => With(x => x is None<TData>, f) as OptionMatch<TData, TResult>;

    }
}
