using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Expression.StreamBuilder;
using Calculator.Expression.Tokens;


namespace Calculator.Tests.StringBuilder_Ling_Tests
{
    public class Mother
    {
        public static int Precsion10 => 10;
        private static int seed = 13;

        
        public static string GetRandomStringExpr(int length)
        {
            bool switchToken = false;
            var sb = new StringBuilder();
            var rand = new Random(seed);
            seed++;

            for (int i = 0; i < length; i++)
            {
                //append operator
                if (switchToken)
                {
                    var val = (EOperator)rand.Next(42, 47);
                    switch (val)
                    {
                        case EOperator.Dot:
                        case EOperator.Comma:
                            i--;
                            break;
                        default:
                            sb.Append(val);
                            switchToken = false;
                            break;
                    }

                }
                //append value
                else
                {
                    sb.Append(rand.NextDecimal());
                    switchToken = true;
                }
            }

            return sb.ToString();

        }

        public static object[] getRandomTokenStream(int length)
            => new object[]{ GetRandomStringExpr(length).ExpressionToTokenStream()};
    }

    public enum EOperator
    {
        Times = 42,
        Plus,
        Comma,
        Minus,
        Dot,
        Divide

    }
    //class

    public static class RandomDecimalLinq
    {
        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(),
                rng.NextInt32(),
                rng.NextInt32(),
                sign,
                scale);
        }
        private static int NextInt32(this Random rng)
        {
            unchecked
            {
                int firstBits = rng.Next(0, 1 << 4) << 28;
                int lastBits = rng.Next(0, 1 << 28);
                return firstBits | lastBits;
            }
        }
    }
}