using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expression.Tokens
{

    public abstract class Token { }

    public class TermDecimal : Token
    {
        public TermDecimal(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }
        public override string ToString() => Value.ToString();
        public static implicit operator decimal(TermDecimal t) => t.Value;
        public static implicit operator TermDecimal(decimal t) => new TermDecimal(t);
    }

    public class TermOperator : Token
    {
        public TermOperator(char value)
        {
            Value = value;
        }
        public char Value { get; }
        public override string ToString() => Value.ToString();
        public static implicit operator char(TermOperator t) => t.Value;
        public static implicit operator TermOperator(char t) => new TermOperator(t);

    }
    
    public class Node
    {
        public Node(Token token)
        {
            Value = token;
        }
        public Node Parent { get; set; }
        public Token Value { get; }
    }//class
    public class ExprNode:Node
    {

        public ExprNode(Token token):base(token)
        {
        }
        public Node Left { get; set; }
        public Node Right { get; set; }
        
        public static implicit operator string(ExprNode v) => v.ToString();
    }//class

    public static class ExprLinq
    {
        //public static IEnumerable<Token> Each(this IEnumerable<Token> tokens)
        //{
        //    return tokens;
        //}
    }

}
