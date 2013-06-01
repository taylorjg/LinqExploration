using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.That(actual.Comparer, Is.EqualTo(EqualityComparer<string>.Default));
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
            var actual = tracks.ToDictionary(t => t.TrackNumber, t => t.Title);

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(5));
            Assert.That(actual.Comparer, Is.EqualTo(EqualityComparer<int>.Default));
            Assert.That(actual[1], Is.EqualTo("So What"));
            Assert.That(actual[2], Is.EqualTo("Freddy Freeloader"));
            Assert.That(actual[3], Is.EqualTo("Blue in Green"));
            Assert.That(actual[4], Is.EqualTo("All Blues"));
            Assert.That(actual[5], Is.EqualTo("Flamenco Sketches"));
        }

        [Test]
        public void ToDictionaryWithKeySelectorAndComparer()
        {
            // Arrange
            var tracks = AlbumData.AlbumData.Artists.First().Albums.First().Tracks;

            // Act
            var actual = tracks.ToDictionary(t => t.Title, StringComparer.CurrentCultureIgnoreCase);

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(5));
            Assert.That(actual.Comparer, Is.EqualTo(StringComparer.CurrentCultureIgnoreCase));
            Assert.That(actual["so what"], Is.SameAs(tracks.ElementAt(0)));
            Assert.That(actual["freddy freeloader"], Is.SameAs(tracks.ElementAt(1)));
            Assert.That(actual["blue in green"], Is.SameAs(tracks.ElementAt(2)));
            Assert.That(actual["all blues"], Is.SameAs(tracks.ElementAt(3)));
            Assert.That(actual["flamenco sketches"], Is.SameAs(tracks.ElementAt(4)));
        }

        [Test]
        public void ToDictionaryWithKeySelectorAndElementSelectorAndComparer()
        {
            // Arrange
            var tracks = AlbumData.AlbumData.Artists.First().Albums.First().Tracks;

            // Act
            var actual = tracks.ToDictionary(t => t.Title, t => t.TrackNumber, StringComparer.CurrentCultureIgnoreCase);

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(5));
            Assert.That(actual.Comparer, Is.EqualTo(StringComparer.CurrentCultureIgnoreCase));
            Assert.That(actual["so what"], Is.EqualTo(1));
            Assert.That(actual["freddy freeloader"], Is.EqualTo(2));
            Assert.That(actual["blue in green"], Is.EqualTo(3));
            Assert.That(actual["all blues"], Is.EqualTo(4));
            Assert.That(actual["flamenco sketches"], Is.EqualTo(5));
        }
    }
}
