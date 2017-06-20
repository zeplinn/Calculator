using System.Collections.Generic;
using System.Data;

namespace Calculator.Tests.ExpressionEvaluation_Tests
{

    public class ExpressionEvaluation
    {
        public ExpressionEvaluation(string expr)
        {
            Expression = expr;
        }

        public string Expression { get; set; }
        internal int Index { get; private set; }
        internal char Symbol { get; set; }
        public Stack<decimal> ValueStack { get; } = new Stack<decimal>();
        public Stack<char> OperatorStack { get; } = new Stack<char>();
        public bool IsEmpty => Expression.Length == Index;
        public bool CanReturnEvaluation => ValueStack.Count == 1 && (OperatorStack.Count==0 || OperatorStack.Count == 1 && OperatorStack.Peek()=='-');


        public bool IsSymbolDigit => char.IsDigit(Symbol);
        public bool CanCalculate => ValueStack.Count >= 2 && OperatorStack.Count > 0;
        internal decimal PopValue => ValueStack.Pop();
        internal char PopOperator => OperatorStack.Pop();

        internal ExpressionEvaluation NewSymbol()
        {
            if (Index < Expression.Length)
            {
                Symbol = Expression[Index];

                Index++;
            }
            else Symbol = '\0';
            return this;
        }

        internal void PushValue(decimal toInt) => ValueStack.Push(toInt);

        internal void PushOperator(char symbol) => OperatorStack.Push(symbol);

        public decimal TryGetResult()
        {
            if (!CanReturnEvaluation) throw new EvaluateException("stacks not empty");
            return PopValue;
        }

        public static implicit operator decimal(ExpressionEvaluation expr) => expr.TryGetResult();
    }
}