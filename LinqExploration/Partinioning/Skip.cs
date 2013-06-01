using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Partinioning
{
    [TestFixture]
    internal class Skip
    {
        [Test]
        public void SkipEnumeratesElementsInTheSequenceUntilTheCountIsReached()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.Skip(4);
            Assert.That(actual.First(), Is.EqualTo(5));
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));

            // We test for 4 + 1 because 4 is the number of elements that we skipped.
            // We then took the first element in the remainder of the sequence.
            // We have to do something with the remainder of the sequence in order
            // to get past the delayed execution of the query.
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(4 + 1));

            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
            Assert.That(actual, Is.EqualTo(new[] { 5, 6, 7, 8, 9, 10 }));
        }
    }
}
