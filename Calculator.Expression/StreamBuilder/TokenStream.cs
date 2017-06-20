using System.Collections.Generic;
using Calculator.Expression.Tokens;

namespace Calculator.Expression.StreamBuilder
{
    public static class TokenStream
    {
        public static IEnumerable<Token> ExpressionToTokenStream(this string expression)
        {
            var ts = new TokenStreamBuilder(expression);
            while (ts.WhileIsExpression())
            {
                TokenStreamBuilderIsNumberLinq.IsOperator(ts.IsNumber());
                while (ts.IsCurrentStreamAlive)
                    yield return ts.DequeueNextToken();
            }

        }
    }
}