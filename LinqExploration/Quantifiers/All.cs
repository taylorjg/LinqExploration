using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Quantifiers
{
    [TestFixture]
    internal class All
    {
        [Test]
        public void AllReturnsTrueIfAllElementsInTheSequenceSatisfyThePredicate()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.All(n => n < 20);
            Assert.That(actual, Is.True);
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(10 + 1));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }

        [Test]
        public void AllReturnsFalseAndStopsEnumeratingAsSoonAsAnElementFailsToSatisfyThePredicate()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.All(n => n < 5);
            Assert.That(actual, Is.False);
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }
    }
}
