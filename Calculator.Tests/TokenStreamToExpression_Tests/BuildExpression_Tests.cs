using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Expression.StreamBuilder;
using Calculator.Expression.Tokens;
using Calculator.Tests.StringBuilder_Ling_Tests;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Calculator.Tests.TokenStreamToExpression_Tests
{
    public class BuildExpression_Tests
    {
        [Theory, InlineAutoData("2+3+4"), InlineAutoData("5-3-2")
            , InlineAutoData("4*8*3"), InlineAutoData("7/3/4")]
        public void TurnTokenStreamIntoValidExpressionTreeSameOperatorStrength
            (string expected, Fixture fixture)
        {
            var actual = expected.ExpressionToTokenStream().BuildExpression();

            Assert.Equal(expected, actual.ToString());
        }
        [Theory, InlineAutoData("2*8+2+4*7*3","?")]
        public void TurnTokenStreamIntoValidExpressionTreeMixedOperatorsInExpression
            (string textExpr, string expected, Fixture fixture)
        {
            var actual = textExpr.ExpressionToTokenStream().BuildExpression();

            Assert.Equal(expected, actual.ToString());
        }
    }

    public class ExpressionTree : IEnumerable<Token>
    {
        public Node Head { get; internal set; }

        public IEnumerator<Token> GetEnumerator()
        {
            switch (Head)
            {
                case ExprNode n:
                    if (n.Left != null)
                        foreach (var token in Recusivechild(n.Left))
                            yield return token;
                    if (n.Right != null)
                        foreach (var token in Recusivechild(n.Left))
                            yield return token;
                    yield return Head.Value;
                    break;
                case Node n:
                    yield return n.Value;
                    break;
                case null: throw new ArgumentNullException("No Head To Loop through");
            }


            IEnumerable<Token> Recusivechild(Node node)
            {
                switch (node)
                {
                    case ExprNode n:
                        if (n.Left != null)
                            foreach (var token in Recusivechild(n.Left))
                                yield return token;
                        if (n.Right != null)
                            foreach (var token in Recusivechild(n.Left))
                                yield return token;
                        yield return n.Value;
                        break;
                    case Node n:
                        yield return n.Value;
                        break;
                    case null: throw new ArgumentNullException("No Head To Loop through");
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Push(Token token)
        {
            if (Head == null)
                Head = new Node(token);
            else
                switch (token)
                {
                    case TermDecimal dec:
                        this.SwitchHead(dec);
                        break;
                    case TermOperator op:
                        this.SwitchHead(op);
                        break;
                    case null: throw new ArgumentNullException();
                    default: throw new InvalidOperationException();
                }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var token in this)
            {
                sb.Append(token);
                //sb.Append((char)EOperator.Comma);
            }
            //sb.Length--;
            return sb.ToString();
        }
    } //class

    public static class BuildExpressionTreeLinq
    {
        public static ExpressionTree BuildExpression(this IEnumerable<Token> stream)
        {
            var tree = new ExpressionTree();
            foreach (var token in stream)
            {
                tree.Push(token);
            }
            return tree;
        }

        internal static void SwitchHead(this ExpressionTree tree, Token token)
        {

            var prev = tree.Head;
            tree.Head = new ExprNode(token) { Left = prev };
            //prev.Parent = tree.Head;
        }

    }//class
}
