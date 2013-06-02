using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Aggregation
{
    [TestFixture, Ignore("These tests take 20-30 seconds to run.")]
    internal class LongCount
    {
        [Test]
        public void CountCanOverflow()
        {
            // Arrange
            var numbers = Enumerable.Range(1, int.MaxValue).Concat(Enumerable.Range(1, 10));

            // Act, Assert
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<System.OverflowException>(() => numbers.Count());
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        [Test]
        public void LongCountWithoutPredicate()
        {
            // Arrange
            var numbers = Enumerable.Range(1, int.MaxValue).Concat(Enumerable.Range(1, 10));

            // Act
            var actual = numbers.LongCount();

            // Assert
            Assert.That(actual, Is.GreaterThan(int.MaxValue));
        }
    }
}
