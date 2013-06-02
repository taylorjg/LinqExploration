using System.Linq;
using NUnit.Framework;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace LinqExploration.Element
{
    [TestFixture]
    internal class First
    {
        [Test]
        public void FirstOnlyEnumeratesTheFirstElementInTheSequence()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            enumerableSpy.First();
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(1));
        }

        [Test]
        public void FirstWithoutPredicate()
        {
            var actual = SampleData.Artists.First();
            Assert.That(actual.Name, Is.EqualTo("Miles Davis"));
        }

        [Test]
        public void FirstWithPredicate()
        {
            var actual = SampleData.Artists.First(artist => artist.Albums.Any(album => album.Tracks.Count() > 5));
            Assert.That(actual.Name, Is.EqualTo("Bill Evans"));
        }

        [Test]
        public void FirstThrowsExceptionIfNoElementInTheSequenceSatisfiesThePredicate()
        {
            Assert.Throws<System.InvalidOperationException>(() => SampleData.Artists.First(artist => artist.Albums.Any(album => album.Tracks.Count() > 10)));
        }
    }
}
