using System;
using System.Collections.Generic;
using Calculator.Expression.Tokens;

namespace Calculator.Expression.StreamBuilder
{
    public class TokenStreamBuilder
    {
        private readonly Queue<Token> currentTokenStream;

        public TokenStreamBuilder(string expression)
        {
            currentTokenStream = new Queue<Token>();
            Expression = expression;
        }

        public string Expression { get; set; }
        internal int Index { get; private set; }
        internal char Symbol { get; set; }
        public Stack<decimal> ValueStack { get; } = new Stack<decimal>();
        public Stack<char> OperatorStack { get; } = new Stack<char>();
        public bool IsEmpty => Expression.Length == Index;
        public bool CanReturnEvaluation => ValueStack.Count == 1 && (OperatorStack.Count == 0 || OperatorStack.Count == 1 && OperatorStack.Peek() == '-');


        public bool IsSymbolDigit => char.IsDigit(Symbol);
        public bool CanCalculate => ValueStack.Count >= 2 && OperatorStack.Count > 0;
        internal decimal PopValue => ValueStack.Pop();
        internal char PopOperator => OperatorStack.Pop();
        internal bool IsCurrentStreamAlive => currentTokenStream.Count > 0;
        internal TokenStreamBuilder NewSymbol()
        {
            if (Index < Expression.Length)
            {
                Symbol = Expression[Index];

                Index++;
            }
            else Symbol = '\0';
            return this;
        }

        internal void EnqueueToken(Token value)
            => currentTokenStream.Enqueue(value);

        internal Token DequeueNextToken() => currentTokenStream.Dequeue();

        public bool WhileIsExpression()
        {
            bool b = Index < Expression.Length;
            NewSymbol();
            return b; ;
        }

        public bool WhileSymbolIs(Func<bool> func)
            => func?.Invoke().Equals(WhileIsExpression())
               ?? throw new ArgumentNullException();
    }
}