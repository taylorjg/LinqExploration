using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

// ReSharper disable PossibleNullReferenceException

namespace LinqExploration.Element
{
    [TestFixture]
    internal class LastOrDefault
    {
        [Test]
        public void LastOrDefaultWithoutPredicate()
        {
            var actual = SampleData.Artists.LastOrDefault();
            Assert.That(actual.Name, Is.EqualTo("Bill Evans"));
        }

        [Test]
        public void LastOrDefaultWithPredicate()
        {
            var actual = SampleData.Artists.LastOrDefault(artist => artist.Albums.Any(album => album.Tracks.Count() > 3));
            Assert.That(actual.Name, Is.EqualTo("Bill Evans"));
        }

        [Test]
        public void LastOrDefaultWithoutPredicateReturnsDefaultIfTheSequenceIsEmpty()
        {
            var actual = Enumerable.Empty<Artist>().LastOrDefault();
            Assert.That(actual, Is.SameAs(default(Artist)));
        }

        [Test]
        public void LastOrDefaultWithPredicateReturnsDefaultIfNoElementIsFoundThatSatisfiesThePredicate()
        {
            var actual = SampleData.Artists.LastOrDefault(artist => artist.Albums.Any(album => album.Tracks.Count() > 10));
            Assert.That(actual, Is.SameAs(default(Artist)));
        }
    }
}
