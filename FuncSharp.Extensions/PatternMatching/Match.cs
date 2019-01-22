using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FuncSharp.Extensions.PatternMatching
{
    /// <summary>
    /// Base class for pattern matching expressions handling.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class Match<TData, TResult> : IEnumerable<MatchCase<TData, TResult>>
    {
        protected TData _data;
        protected Func<TResult> _funct;
        protected List<MatchCase<TData, TResult>> _matches = new List<MatchCase<TData, TResult>>();

        /// <summary>
        /// Indicates that Match has success result.
        /// </summary>
        public bool IsSuccess => _matches.FirstOrDefault(m => m.CanExecute(_data)) != null;

        /// <summary>
        /// Return pattern matching expression result.
        /// </summary>
        public TResult Result {
            get
            {
                var match = _matches.FirstOrDefault(m => m.CanExecute(_data));

                if (match != null)
                    return match.Execute(_data);

                throw new ArgumentException();
            }
        }

        public Match(TData data) => _data = data;

        /// <summary>
        /// Add new MatchCase to Match expression.
        /// </summary>
        /// <param name="on"></param>
        /// <param name="execute"></param>
        public virtual void Add(Func<TData, bool> on, Func<TResult> execute) 
            => _matches.Add(new MatchCase<TData, TResult>(on, execute));

        /// <summary>
        /// Add new MatchCase to Match expression.
        /// </summary>
        /// <param name="on"></param>
        /// <param name="execute"></param>
        /// <returns></returns>
        public virtual Match<TData, TResult> With(Func<TData, bool> on, Func<TResult> execute)
        {
            Add(on, execute);
            return this;
        }

        /// <summary>
        /// Add new MatchCase to Match expression.
        /// </summary>
        /// <param name="on"></param>
        /// <param name="execute"></param>
        /// <returns></returns>
        public Match<TData, TResult> With(bool on, Func<TResult> execute)
        {
            Add(_ => on, execute);
            return this;
        }

        /// <summary>
        /// Enumerates through match cases.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<MatchCase<TData, TResult>> GetEnumerator() => _matches.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static implicit operator Match<TData, TResult>(TData data) => new Match<TData, TResult>(data);

        public static implicit operator TResult(Match<TData, TResult> match) => match.Result;

        public static Match<TData, TResult> operator |(Match<TData, TResult> match,
            (Func<TData, bool> on, Func<TResult> execute) matchCase) => match.With(matchCase.on, matchCase.execute);

        public static Match<TData, TResult> operator |(Match<TData, TResult> match,
            (bool on, Func<TResult> execute) matchCase) => match.With(matchCase.on, matchCase.execute);

    }

}
