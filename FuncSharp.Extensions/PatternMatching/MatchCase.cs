using System;
using System.Collections.Generic;
using System.Text;

namespace FuncSharp.Extensions.PatternMatching
{
    /// <summary>
    /// Base class for match expression case.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class MatchCase<TData, TResult>
    {
        private Func<TData, bool> _predicate;
        private Func<TResult> _func;

        public MatchCase(Func<TData, bool> on, Func<TResult> execute)
        {
            _predicate = on;
            _func = execute;
        }

        /// <summary>
        /// Indicates that match case predicate is true.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool CanExecute(TData context) => _predicate(context);

        public TResult Execute(TData context) => _predicate(context)
            ? _func() : throw new ArgumentException();
    }
}
