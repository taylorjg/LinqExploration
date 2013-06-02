using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable PossibleNullReferenceException

namespace LinqExploration.Element

{
    [TestFixture]
    internal class SingleOrDefault
    {
        [Test]
        public void SingleOrDefaultWithoutPredicateGivenASequenceContainingASingleElementReturnsThatElement()
        {
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            var track = new[] {tracks.ElementAt(2)};
            var actual = track.SingleOrDefault();
            Assert.That(actual.TrackNumber, Is.EqualTo(3));
            Assert.That(actual, Is.SameAs(tracks.ElementAt(2)));
        }

        [Test]
        public void SingleOrDefaultWithoutPredicateGivenASequenceContainingMoreThanOneElementThrowsAnException()
        {
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            Assert.Throws<System.InvalidOperationException>(() => tracks.SingleOrDefault());
        }

        [Test]
        public void SingleOrDefaultWithPredicateEnumeratesTheEntireSequence()
        {
            // Arrange
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            // Act
            var actual = enumerableSpy.SingleOrDefault(t => t.TrackNumber == 3);

            // Assert
            Assert.That(actual.TrackNumber, Is.EqualTo(3));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5 + 1));
        }

        [Test]
        public void SingleOrDefaultWithoutPredicateReturnsDefaultIfTheSequenceIsEmpty()
        {
            var actual = Enumerable.Empty<Track>().SingleOrDefault();
            Assert.That(actual, Is.SameAs(default(Track)));
        }

        [Test]
        public void SingleOrDefaultWithPredicateReturnsDefaultIfTheSequenceIsEmpty()
        {
            var actual = Enumerable.Empty<Track>().SingleOrDefault(t => t.TrackNumber == 10);
            Assert.That(actual, Is.SameAs(default(Track)));
        }

        [Test]
        public void SingleOrDefaultWithPredicateReturnsDefaultIfNoElementsSatisfyThePredicate()
        {
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            var actual = tracks.SingleOrDefault(t => t.TrackNumber == 10);
            Assert.That(actual, Is.SameAs(default(Track)));
        }

        [Test]
        public void SingleOrDefaultWithPredicateThrowsAnExceptionIfMoreThanOneElementSatisfiesThePredicate()
        {
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            Assert.Throws<System.InvalidOperationException>(() => tracks.SingleOrDefault(t => t.Title.Contains("Blue")));
        }
    }
}
