using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Partinioning
{
    [TestFixture]
    internal class TakeWhile
    {
        [Test]
        public void TakeWhileStopsEnumeratingWhenThePredicateReturnsFalse()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.TakeWhile(n => n < 5);
            Assert.That(actual, Is.EquivalentTo(new[] { 1, 2, 3, 4 }));
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }

        [Test]
        public void TakeWhileStopsEnumeratingWhenThePredicateReturnsFalseThisTimeWithIndex()
        {
            // n:     10 9 8 7 6 5 4 3 2 1
            // index: 0  1 2 3 4 5 6 7 8 9
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10).Reverse());
            var actual = enumerableSpy.TakeWhile((n, index) => n > index);
            Assert.That(actual, Is.EquivalentTo(new[] { 10, 9, 8, 7, 6 }));
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(6));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }
    }
}
