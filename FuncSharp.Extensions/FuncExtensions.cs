using FuncSharp.Extensions.PatternMatching;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuncSharp.Extensions
{
    public static class FuncExtensions
    {
        public static EqualityMatch<TData, TResult> Match<TData, TResult>(TData data)
            => new EqualityMatch<TData, TResult>(data);
        
        public static EnumMatch<TResult> Match<TResult>(Enum en) => new EnumMatch<TResult>(en);
        
    }
}
