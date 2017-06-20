using System.Data;
using System.Text;
using Calculator.Expression.Exceptions;
using Calculator.Expression.StreamBuilder;
using Calculator.Tests.StringBuilder_Ling_Tests;

namespace Calculator.Tests.ExpressionEvaluation_Tests
{

    public static class ExpressionEvaluator
    {
        public static ExpressionEvaluation EvaluateExpression(this string expr)
        {
            var ee = new ExpressionEvaluation(expr);
            while (!ee.IsEmpty)
            {
                ee.NewSymbol()
                    .IsNumber()
                    .IsOperator()
                    .IntermediateEvaluation();
            }
            return ee;
        }

        public static ExpressionEvaluation IsNumber(this ExpressionEvaluation ee)
        {
            if (!ee.IsSymbolDigit) return ee;
            var sb = new StringBuilder();
            do
            {
                sb.Append(ee.Symbol);
                ee.NewSymbol();
            } while (ee.IsSymbolDigit);
            if (ee.Symbol == '.')
            {
                sb.Append(ee.Symbol);
                ee.NewSymbol();
                do
                {
                    sb.Append(ee.Symbol);
                    ee.NewSymbol();
                } while (ee.IsSymbolDigit);
            } 
            ee.PushValue(sb.ToNumber());
            return ee;
        }
        public static ExpressionEvaluation IsOperator(this ExpressionEvaluation ee)
        {
            switch (ee.Symbol)
            {
                case '+':
                    ee.PushOperator(ee.Symbol);
                    ee.NewSymbol();
                    break;
                case '\0':
                    return ee;
                default: throw new UnkownOperatorException($"Value \"{ee.Symbol}\"");
            }
            return ee;
        }
        public static void IntermediateEvaluation(this ExpressionEvaluation ee)
        {
            // if (!(values.Count >= 2 && operators.Count > 0)) return;
            if (!ee.CanCalculate) return;
            //if (ee.CanInvertValue)
                ee.PushValue(decimal.Negate(ee.PopValue));
            ee.PushValue(ee.Calculate(ee.PopValue, ee.PopValue));
        }
        public static decimal Calculate(this ExpressionEvaluation ee, decimal right, decimal left)
        {
            char op = ee.PopOperator;
            switch (op)
            {
                case '+':
                    return left + right;
                case '-':
                    return left - right;
                default: throw new UnkownOperatorException($"Value \"{op}\"");
            }
        }

        public static decimal Invert(this decimal value) => 0-value;

    }
}