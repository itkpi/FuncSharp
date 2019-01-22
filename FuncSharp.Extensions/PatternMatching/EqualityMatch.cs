using System;
using System.Collections.Generic;
using System.Text;

namespace FuncSharp.Extensions.PatternMatching
{
    public class EqualityMatch<TData, TResult> : Match<TData, TResult>
    {
        public EqualityMatch(TData data) : base(data)
        {
        }

        public void Add(TData onData, Func<TResult> execute) => Add(x => x.Equals(onData), execute);

        public EqualityMatch<TData, TResult> Case(TData onData, Func<TResult> execute)
        {
            Add(onData, execute);
            return this;
        }

        public static EqualityMatch<TData, TResult> operator |(EqualityMatch<TData, TResult> match,
            (TData on, Func<TResult> execute) matchCase) => match.Case(matchCase.on, matchCase.execute);
    }

    public class EnumMatch<TResult> : EqualityMatch<Enum, TResult>
    {
        public EnumMatch(Enum data) : base(data)
        {

        }
    }
}
