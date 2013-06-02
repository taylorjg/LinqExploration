using System.Linq;
using NUnit.Framework;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace LinqExploration.Element
{
    [TestFixture]
    internal class Last
    {
        [Test]
        public void LastEnumeratesTheEntireSequence()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            enumerableSpy.Last();
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(10 + 1));
        }

        [Test]
        public void LastWithoutPredicate()
        {
            var actual = SampleData.Artists.Last();
            Assert.That(actual.Name, Is.EqualTo("Bill Evans"));
        }

        [Test]
        public void LastWithPredicate()
        {
            var actual = SampleData.Artists.Last(artist => artist.Albums.Any(album => album.Tracks.Count() > 3));
            Assert.That(actual.Name, Is.EqualTo("Bill Evans"));
        }

        [Test]
        public void LastThrowsExceptionIfNoElementInTheSequenceSatisfiesThePredicate()
        {
            Assert.Throws<System.InvalidOperationException>(() => SampleData.Artists.Last(artist => artist.Albums.Any(album => album.Tracks.Count() > 10)));
        }
    }
}
