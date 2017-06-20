using Calculator.Tests.StringBuilder_Ling_Tests;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Calculator.Tests.ExpressionEvaluation_Tests
{
    public class ExpressionEvaluationisNumberTests
    {
        [Theory
			/*ints*/, InlineAutoData(6), InlineAutoData(213712928.00)
         /*decimal*/, InlineAutoData(5.3333),InlineAutoData(3243208492.34324234)
			,AutoData]
        public void GetSinglePositiveNumbers(decimal expected, Fixture fixture)
        {
            string expr = expected.ToString();
            var actual = expr.EvaluateExpression();

            Assert.Equal(expected, actual,Mother.Precsion10);
        }
        [Theory
         /*ints*/, InlineAutoData(-6), InlineAutoData(-213712928.00)
         /*decimal*/, InlineAutoData(-5.3333), InlineAutoData(-3243208492.34324234)]
        public void GetSingleNegativeNumbers(decimal expected, Fixture fixture)
        {
            string expr = expected.ToString();
            var actual = expr.EvaluateExpression();

            Assert.Equal(expected, actual, Mother.Precsion10);
        }

    }
}