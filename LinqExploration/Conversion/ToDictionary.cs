using System.Linq;
using LinqExploration.AlbumData.EqualityComparers;
using NUnit.Framework;

namespace LinqExploration.Conversion
{
    [TestFixture]
    internal class ToDictionary
    {
        [Test]
        public void ToDictionaryWithKeySelector()
        {
            // Arrange
            var tracks = AlbumData.AlbumData.Artists.First().Albums.First().Tracks;

            // Act
            var actual = tracks.ToDictionary(t => t.Title);

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(5));
            Assert.That(actual["So What"], Is.SameAs(tracks.ElementAt(0)));
            Assert.That(actual["Freddy Freeloader"], Is.SameAs(tracks.ElementAt(1)));
            Assert.That(actual["Blue in Green"], Is.SameAs(tracks.ElementAt(2)));
            Assert.That(actual["All Blues"], Is.SameAs(tracks.ElementAt(3)));
            Assert.That(actual["Flamenco Sketches"], Is.SameAs(tracks.ElementAt(4)));
        }

        [Test]
        public void ToDictionaryWithKeySelectorAndElementSelector()
        {
            // Arrange
            var tracks = AlbumData.AlbumData.Artists.First().Albums.First().Tracks;

            // Act
            var actual = tracks.ToDictionary(t => t.Title, t => t.TrackNumber);

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(5));
            Assert.That(actual["So What"], Is.EqualTo(1));
            Assert.That(actual["Freddy Freeloader"], Is.EqualTo(2));
            Assert.That(actual["Blue in Green"], Is.EqualTo(3));
            Assert.That(actual["All Blues"], Is.EqualTo(4));
            Assert.That(actual["Flamenco Sketches"], Is.EqualTo(5));
        }

        [Test]
        public void ToDictionaryWithKeySelectorAndComparer()
        {
            // Arrange
            var tracks = AlbumData.AlbumData.Artists.First().Albums.First().Tracks;

            // Act
            var comparer = new TrackLengthEqualityComparer();
            var actual = tracks.ToDictionary(t => t, comparer);

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(5));
            Assert.That(actual[tracks.ElementAt(0)], Is.SameAs(tracks.ElementAt(0)));
            Assert.That(actual[tracks.ElementAt(1)], Is.SameAs(tracks.ElementAt(1)));
            Assert.That(actual[tracks.ElementAt(2)], Is.SameAs(tracks.ElementAt(2)));
            Assert.That(actual[tracks.ElementAt(3)], Is.SameAs(tracks.ElementAt(3)));
            Assert.That(actual[tracks.ElementAt(4)], Is.SameAs(tracks.ElementAt(4)));
        }

        [Test]
        public void ToDictionaryWithKeySelectorAndElementSelectorAndComparer()
        {
            // Arrange
            var tracks = AlbumData.AlbumData.Artists.First().Albums.First().Tracks;

            // Act
            var comparer = new TrackLengthEqualityComparer();
            var actual = tracks.ToDictionary(t => t, t => t.TrackNumber, comparer);

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(5));
            Assert.That(actual[tracks.ElementAt(0)], Is.EqualTo(1));
            Assert.That(actual[tracks.ElementAt(1)], Is.EqualTo(2));
            Assert.That(actual[tracks.ElementAt(2)], Is.EqualTo(3));
            Assert.That(actual[tracks.ElementAt(3)], Is.EqualTo(4));
            Assert.That(actual[tracks.ElementAt(4)], Is.EqualTo(5));
        }
    }
}
