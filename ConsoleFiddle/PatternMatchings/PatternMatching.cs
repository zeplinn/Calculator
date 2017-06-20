using System;

namespace ConsoleFiddle.PatternMatchings
{
    public class PatternMatching
    {
        public static void Run()
        {
            Console.WriteLine("Can PatternMatch with interfaces:");
            Console.WriteLine($"\t Is {nameof(Pattern1)}= {_Match(new Pattern1())}");
            Console.WriteLine($"\t Is {nameof(Pattern2)} = {_Match(new Pattern2())}");



        }
        private static bool _Match(IPatternMatch pattern)
        {
            switch (pattern)
            {
                case Pattern1 p:
                    return p is Pattern1;
                case Pattern2 p:
                    return p is Pattern2;
                default: return false;
            }
        }

        private interface IPatternMatch { }

        private class Pattern1 : IPatternMatch { }

        private class Pattern2 : IPatternMatch { }
    }
    
}