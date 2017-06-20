using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Calculator.Expression.StreamBuilder;
using Calculator.Expression.Tokens;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Calculator.Tests.TokenStreamLinqTests
{
    public class StringToTokenStream_Tests
    {
        [Theory, InlineAutoData(6.434), InlineAutoData(444.3333)
            , InlineAutoData(4), InlineAutoData(23424)]
        public void SingleStringValueToTokenStream(string expected, Fixture fixture)
        {
            var actual = expected.ExpressionToTokenStream();
            Assert.Equal(expected, actual.TokenStremToString());
        }

        [Theory, InlineAutoData('+'), InlineAutoData('-')
            , InlineAutoData('/'), InlineAutoData('*')]
        public void SingleOperatorToTokenStream(string expected, Fixture fixture)
        {
            var actual = expected.ExpressionToTokenStream();
            Assert.Equal(expected, actual.TokenStremToString());
        }

        [Theory, InlineAutoData("2+4"),
            InlineAutoData("7+342+242-242*342")]
        public void GetMultipleValuesFromTokenStream(string expected, Fixture fixture)
        {
            var actual = expected.ExpressionToTokenStream();
            Assert.Equal(expected, actual.TokenStremToString());
        }
    }
}
