using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Aggregation
{
    [TestFixture]
    internal class Aggregate
    {
        private const int SumOfDigits1To10 = 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10;

        [Test]
        public void AggregateWithFunc()
        {
            var actual = Enumerable.Range(1, 10)
                .Aggregate(
                    (soFar, next) => soFar + next);

            Assert.That(actual, Is.EqualTo(SumOfDigits1To10));
        }

        [Test]
        public void AggregateWithSeedAndFunc()
        {
            var actual = Enumerable.Range(1, 10)
                .Aggregate(
                    50,
                    (soFar, next) => soFar + next);

            Assert.That(actual, Is.EqualTo(50 + SumOfDigits1To10));
        }

        [Test]
        public void AggregateWithSeedAndFuncAndResultSelector()
        {
            var actual = Enumerable.Range(1, 10)
                .Aggregate(
                    50,
                    (soFar, next) => soFar + next,
                    result => result + 200);

            Assert.That(actual, Is.EqualTo(50 + SumOfDigits1To10 + 200));
        }
    }
}
