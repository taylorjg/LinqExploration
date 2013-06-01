using System.Linq;
using LinqExploration.AlbumData;
using LinqExploration.AlbumData.EqualityComparers;
using NUnit.Framework;

namespace LinqExploration.Quantifiers
{
    [TestFixture]
    internal class Contains
    {
        [Test]
        public void ContainsReturnsTrueIfTheSequenceContainsTheElementAndAlsoStopsEnumeratingWhenTheElementIsFound()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.Contains(7);
            Assert.That(actual, Is.True);
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(7));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }

        [Test]
        public void ContainsReturnsTrueIfTheSequenceContainsTheElementAndAlsoStopsEnumeratingWhenTheElementIsFoundNowWithAnEqualityComparer()
        {
            var enumerableSpy = new EnumerableSpy<Track>(SampleData.Artists.First().Albums.First().Tracks);
            var value = new Track {Length = "5:37"};
            var actual = enumerableSpy.Contains(value, new TrackLengthEqualityComparer());
            Assert.That(actual, Is.True);
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(3));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }
    }
}
