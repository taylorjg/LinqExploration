using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Quantifiers
{
    [TestFixture]
    internal class Any
    {
        [Test]
        public void AnyOnlyEnumeratesTheSequenceAsFarAsTheFirstMatch()
        {
            var numbers = Enumerable.Range(1, 10);
            var enumerableWrapper = new EnumerableWrapper<int>(numbers);
            var actual = enumerableWrapper.Any(n => n >= 5);
            Assert.That(actual, Is.True);
            Assert.That(enumerableWrapper.NumCallsToMoveNext, Is.EqualTo(5));
        }
    }
}
