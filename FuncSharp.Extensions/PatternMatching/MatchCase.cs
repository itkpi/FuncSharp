using System;
using System.Collections.Generic;
using System.Text;

namespace FuncSharp.Extensions.PatternMatching
{
    public class MatchCase<TData, TResult>
    {
        private Func<TData, bool> _predicate;
        private Func<TResult> _func;

        public MatchCase(Func<TData, bool> on, Func<TResult> execute)
        {
            _predicate = on;
            _func = execute;
        }

        public bool CanExecute(TData context) => _predicate(context);

        public TResult Execute(TData context) => _predicate(context)
            ? _func() : throw new ArgumentException();
    }
}
