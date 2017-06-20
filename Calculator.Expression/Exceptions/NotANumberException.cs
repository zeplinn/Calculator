using System;

namespace Calculator.Expression.Exceptions
{
    public class NotANumberException : Exception
    {
        public NotANumberException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}