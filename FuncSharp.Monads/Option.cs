using System;

namespace FuncSharp.Monads
{
    public static class Option
    {
        public static Option<T> Some<T>(T value) => value;
        public static None None => None.Instance;
    }

    public abstract class Option<T>
    {
        public abstract Option<R> Map<R>(Func<T, R> f);
        public abstract Option<R> Bind<R>(Func<T, Option<R>> f);
        public abstract R Fold<R>(Func<R> ifEmpty, Func<T, R> f);

        public static implicit operator Option<T>(T value) => new Some<T>(value);
        public static implicit operator Option<T>(None none) => new None<T>();
    }

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


        public static implicit operator T(Some<T> some) => some.Value;
        public static implicit operator Some<T>(T value) => new Some<T>(value);

        public override string ToString() => $"{nameof(Some<T>)}({Value})";
    }

    public class None<T> : Option<T>
    {
        public override Option<R> Bind<R>(Func<T, Option<R>> f) => Option.None;
        public override R Fold<R>(Func<R> ifEmpty, Func<T, R> f) => ifEmpty();
        public override Option<R> Map<R>(Func<T, R> f) => Option.None;

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
