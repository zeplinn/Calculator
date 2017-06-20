using System.Collections.Generic;
using System.Text;
using Calculator.Expression.Tokens;

namespace Calculator.Expression.StreamBuilder
{
    public static class TokenStreamBuilderIsNumberLinq
    {
        

        public static TokenStreamBuilder IsNumber(this TokenStreamBuilder ts)
        {
            if (!ts.IsSymbolDigit) return ts;
            var sb = new StringBuilder();
            do
            {
                sb.Append((char) ts.Symbol);
                ts.NewSymbol();
            } while (ts.IsSymbolDigit);
            if (ts.Symbol == '.')
            {
                sb.Append((char) ts.Symbol);
                ts.NewSymbol();
                do
                {
                    sb.Append((char) ts.Symbol);
                    ts.NewSymbol();
                } while (ts.IsSymbolDigit);
            }
            TermDecimal t = sb.ToNumber();
            ts.EnqueueToken(t);
            return ts;
        }

        public static string TokenStremToString(this IEnumerable<Token> e)
        {
            var sb = new StringBuilder();
            foreach (var token in e)
                sb.Append((object) token);
            return sb.ToString();
        }


        public static TokenStreamBuilder IsOperator(this TokenStreamBuilder ts)
        {
            switch (ts.Symbol)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    TermOperator o = ts.Symbol;
                    ts.EnqueueToken(o);
                    return ts;
            }

            return ts;
        }

    }
}