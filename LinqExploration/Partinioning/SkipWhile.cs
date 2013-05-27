using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Partinioning
{
    [TestFixture]
    internal class SkipWhile
    {
        [Test]
        public void SkipWhileEnumeratesElementsInTheSequenceUntilThePredicateReturnsFalse()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.SkipWhile(n => n < 7);
            Assert.That(actual.First(), Is.EqualTo(7));
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));

            // I expected this to be 7 + 1.
            // We need to reach the 7th element in the sequence to find an
            // element for which the predicate returns false. Then, we take
            // the first element in the remainder of the sequence.
            // But, internally, SkipWhile uses a SkipWhileIterator which is
            // smart. As soon as it reaches the first element that doesn't
            // satisfy the predicate, it starts returning elements.
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(7));

            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }

        [Test]
        public void SkipWhileEnumeratesElementsInTheSequenceUntilThePredicateReturnsFalseThisTimeWithIndex()
        {
            // n:     10 9 8 7 6 5 4 3 2 1
            // index: 0  1 2 3 4 5 6 7 8 9
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10).Reverse());
            var actual = enumerableSpy.SkipWhile((n, index) => n > index);
            Assert.That(actual.First(), Is.EqualTo(5));
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(6));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }
    }
}
