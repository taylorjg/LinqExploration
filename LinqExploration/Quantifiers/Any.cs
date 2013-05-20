using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Quantifiers
{
    [TestFixture]
    internal class Any
    {
        [Test]
        public void AnyWithoutAPredicateOnlyEnumeratesTheFirstItemInTheSequence()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.Any();
            Assert.That(actual, Is.True);
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }

        [Test]
        public void AnyWithAPredicateOnlyEnumeratesTheSequenceAsFarAsTheFirstMatch()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.Any(n => n >= 5);
            Assert.That(actual, Is.True);
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }

        [Test]
        public void AnyWithAPredicateEnumeratesTheEntireSequenceIfNoMatchFound()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var itemCount = enumerableSpy.Count();
            enumerableSpy.ResetCallCounts();
            var actual = enumerableSpy.Any(n => n >= 99);
            Assert.That(actual, Is.False);
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));

            // The 11th call to MoveNext() returns false because we have reached the end of the sequence.
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(itemCount + 1));

            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }
    }
}
