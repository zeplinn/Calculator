using System.Text;
using Calculator.Expression.Exceptions;
using Calculator.Expression.Tokens;

namespace Calculator.Expression.StreamBuilder
{
    public static class StringBuilderLinq
    {
        public static int ToInt(this StringBuilder sb)
        {

            {
                if (!int.TryParse(sb.ToString(), out int i)) throw new NotANumberException($"not a number: {sb}");
                sb.Clear();
                return i;
            }
        }

        public static decimal ToNumber(this StringBuilder sb)
        {
            if (!decimal.TryParse(sb.ToString(), out decimal dec)) throw new NotANumberException($"not a number: {sb}");
            sb.Clear();
            return dec;
        }
        public static Token ToNumberToken(this StringBuilder sb)
        {
            if (!decimal.TryParse(sb.ToString(), out decimal dec)) throw new NotANumberException($"not a number: {sb}");
            sb.Clear();
            return new TermDecimal(dec);
        }
    }//class
}//Namespace