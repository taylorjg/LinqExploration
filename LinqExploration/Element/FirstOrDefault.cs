using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

// ReSharper disable PossibleNullReferenceException

namespace LinqExploration.Element
{
    [TestFixture]
    internal class FirstOrDefault
    {
        [Test]
        public void FirstOrDefaultWithoutPredicate()
        {
            var actual = SampleData.Artists.FirstOrDefault();
            Assert.That(actual.Name, Is.EqualTo("Miles Davis"));
        }

        [Test]
        public void FirstOrDefaultWithPredicate()
        {
            var actual = SampleData.Artists.FirstOrDefault(artist => artist.Albums.Any(album => album.Tracks.Count() > 5));
            Assert.That(actual.Name, Is.EqualTo("Bill Evans"));
        }

        [Test]
        public void FirstOrDefaultWithoutPredicateReturnsDefaultIfTheSequenceIsEmpty()
        {
            var actual = Enumerable.Empty<Artist>().FirstOrDefault();
            Assert.That(actual, Is.SameAs(default(Artist)));
        }

        [Test]
        public void FirstOrDefaultWithPredicateReturnsDefaultIfNoElementIsFoundThatSatisfiesThePredicate()
        {
            var actual = SampleData.Artists.FirstOrDefault(artist => artist.Albums.Any(album => album.Tracks.Count() > 10));
            Assert.That(actual, Is.SameAs(default(Artist)));
        }
    }
}
