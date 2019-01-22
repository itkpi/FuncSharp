using FuncSharp.Extensions;
using FuncSharp.Extensions.PatternMatching;
using FuncSharp.Monads;
using System;
using System.Linq.Expressions;

using static FuncSharp.Extensions.FuncExtensions;
namespace FuncSharp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int b = 2;
            int c = 200;
            int d = 12;
            
            int a = (Match<int, int>) b
                | (x => x > c, () => 25 / 2)
                | (b > 0, () => 123);
            
            int qwe = Match<int>(ConsoleColor.Black)
                | (ConsoleColor.Black, () => a / 123)
                | (ConsoleColor.Blue, () => 0);

            var match = Match<int, int>(2) 
                       | (b > c, () => 123)
                       | (d == 12, () => 111)
                       | (c > d, () => 234);

            var test = "hello";

            int result = Match<string, int>(test)          
                  | ("Test", () => 1) | ("hello", () => 2) | ("world", () => 3);

            Option<int> opt = Option.Some(5);
            Option<int> some = 1;
            Option<int> none = Option.None;

            int res2 = Match<int, int>(opt) 
                | (opt is Some<int>, () => 1) 
                | (opt is None<int>, () => 2);

            var optResult = Match<int, Option<int>>(opt)
                .IfSome(x => x * 2)
                .IfNone(() => 0)
                .Result
                .Map(x => $"Option: {x}");
            

            Console.WriteLine(optResult);
        }
    }
}
