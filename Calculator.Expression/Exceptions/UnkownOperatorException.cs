using System;

namespace Calculator.Expression.Exceptions
{
    public class UnkownOperatorException : Exception
    {
        public UnkownOperatorException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}