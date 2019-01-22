using System;
using System.Collections.Generic;
using System.Text;

namespace FuncSharp.Extensions.PatternMatching
{
    /// <summary>
    /// Match expression for for object equality.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class EqualityMatch<TData, TResult> : Match<TData, TResult>
    {
        public EqualityMatch(TData data) : base(data)
        {
        }

        public void Add(TData onData, Func<TResult> execute) => Add(x => x.Equals(onData), execute);

        public EqualityMatch<TData, TResult> With(TData onData, Func<TResult> execute)
        {
            Add(onData, execute);
            return this;
        }

        public static EqualityMatch<TData, TResult> operator |(EqualityMatch<TData, TResult> match,
            (TData on, Func<TResult> execute) matchCase) => match.With(matchCase.on, matchCase.execute);
    }

    public class EnumMatch<TResult> : EqualityMatch<Enum, TResult>
    {
        public EnumMatch(Enum data) : base(data)
        {

        }
    }
}
