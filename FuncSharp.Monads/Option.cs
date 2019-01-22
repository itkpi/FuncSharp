using System;

namespace FuncSharp.Monads
{
    /// <summary>
    /// Dicscriminated union type which can be in 2 states: 
    /// 
    ///     1. Some<T>
    ///     2. None
    /// 
    /// </summary>
    public static class Option
    {
        public static Option<T> Some<T>(T value) => value;
        public static None None => None.Instance;
    }

    public abstract class Option<T>
    {
        /// <summary>
        /// Projects Option value to another type if it is not None.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract Option<R> Map<R>(Func<T, R> f); 

        /// <summary>
        /// Projects Option to Option of another type if it is not None.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract Option<R> Bind<R>(Func<T, Option<R>> f);

        /// <summary>
        /// Projects Option value to another type if it is not None.
        /// If Option is None - call ifEmpty.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="ifEmpty"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract R Fold<R>(Func<R> ifEmpty, Func<T, R> f);

        /// <summary>
        /// Get Option value if it is not None or call that.
        /// </summary>
        /// <param name="defaultF"></param>
        /// <returns></returns>
        public abstract T GetOrElse(Func<T> defaultF);

        /// <summary>
        /// Return this if it is not None or call that.
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public abstract Option<T> OrElse(Func<Option<T>> that);

        /// <summary>
        /// Return this if it is not None and hold predicate or return None.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public abstract Option<T> Where(Func<T, bool> predicate);

        public static implicit operator Option<T>(T value) => new Some<T>(value);
        public static implicit operator Option<T>(None none) => new None<T>();
    }

    /// <summary>
    /// Holds Option<T> value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Some<T> : Option<T>
    {
        public T Value { get; }

        internal Some(T value)
        {
            Value = value;
        }

        public override Option<R> Map<R>(Func<T, R> f) => f(Value);
        public override Option<R> Bind<R>(Func<T, Option<R>> f) => f(Value);
        public override R Fold<R>(Func<R> ifEmpty, Func<T, R> f) => f(Value);
        public override T GetOrElse(Func<T> defaultF) => this;
        public override Option<T> OrElse(Func<Option<T>> that) => this;
        public override Option<T> Where(Func<T, bool> predicate) 
            => predicate(this) ? this as Option<T> : Option.None;

        public static implicit operator T(Some<T> some) => some.Value;
        public static implicit operator Some<T>(T value) => new Some<T>(value);

        public override string ToString() => $"{nameof(Some<T>)}({Value})";
        
    }

    /// <summary>
    /// Indicates that Option<T> has no value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class None<T> : Option<T>
    {
        public override Option<R> Bind<R>(Func<T, Option<R>> f) => Option.None;
        public override R Fold<R>(Func<R> ifEmpty, Func<T, R> f) => ifEmpty();
        public override T GetOrElse(Func<T> defaultF) => defaultF();
        public override Option<R> Map<R>(Func<T, R> f) => Option.None;
        public override Option<T> OrElse(Func<Option<T>> that) => that();
        public override Option<T> Where(Func<T, bool> predicate) => Option.None;

        public override string ToString() => Option.None.ToString();
    }

    public class None
    {
        private static None _instance;

        public static None Instance => _instance ?? (_instance = new None());

        private None(){}
        public override string ToString() => nameof(None);
    }
}
