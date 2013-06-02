using System.Collections.Generic;
using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

// ReSharper disable LoopCanBeConvertedToQuery

namespace LinqExploration.Projection
{
    [TestFixture]
    internal class SelectMany
    {
        private IEnumerable<Album> _albums;

        [SetUp]
        public void SetUp()
        {
            var artist1 = SampleData.Artists[0];
            var artist2 = SampleData.Artists[1];
            _albums = artist1.Albums.Concat(artist2.Albums);
        }

        [Test]
        public void SelectWithCollectionSelectorTest1()
        {
            // Arrange, Act
            var actual = _albums.SelectMany(
                album => album.Tracks
            ).ToList();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(5 + 7));

            Assert.That(actual[0].TrackNumber, Is.EqualTo(1));
            Assert.That(actual[1].TrackNumber, Is.EqualTo(2));
            Assert.That(actual[2].TrackNumber, Is.EqualTo(3));
            Assert.That(actual[3].TrackNumber, Is.EqualTo(4));
            Assert.That(actual[4].TrackNumber, Is.EqualTo(5));

            Assert.That(actual[5].TrackNumber, Is.EqualTo(1));
            Assert.That(actual[6].TrackNumber, Is.EqualTo(2));
            Assert.That(actual[7].TrackNumber, Is.EqualTo(3));
            Assert.That(actual[8].TrackNumber, Is.EqualTo(4));
            Assert.That(actual[9].TrackNumber, Is.EqualTo(5));
            Assert.That(actual[10].TrackNumber, Is.EqualTo(6));
            Assert.That(actual[11].TrackNumber, Is.EqualTo(7));
        }

        [Test]
        public void SelectWithCollectionSelectorTest2()
        {
            // Arrange, Act
            var actual = _albums.SelectMany(
                album => album.Tracks.Select(track => track.TrackNumber)
            ).ToList();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(5 + 7));

            Assert.That(actual[0], Is.EqualTo(1));
            Assert.That(actual[1], Is.EqualTo(2));
            Assert.That(actual[2], Is.EqualTo(3));
            Assert.That(actual[3], Is.EqualTo(4));
            Assert.That(actual[4], Is.EqualTo(5));

            Assert.That(actual[5], Is.EqualTo(1));
            Assert.That(actual[6], Is.EqualTo(2));
            Assert.That(actual[7], Is.EqualTo(3));
            Assert.That(actual[8], Is.EqualTo(4));
            Assert.That(actual[9], Is.EqualTo(5));
            Assert.That(actual[10], Is.EqualTo(6));
            Assert.That(actual[11], Is.EqualTo(7));
        }

        [Test]
        public void SelectWithCollectionSelectorTest3()
        {
            // Arrange, Act
            var actual = _albums.SelectMany(
                album => album.Tracks.Select(track => new { AlbumTitle = album.Title, TrackTitle = track.Title })
            ).ToList();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(5 + 7));

            Assert.That(actual[0], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "So What" }));
            Assert.That(actual[1], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "Freddy Freeloader" }));
            Assert.That(actual[2], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "Blue in Green" }));
            Assert.That(actual[3], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "All Blues" }));
            Assert.That(actual[4], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "Flamenco Sketches" }));

            Assert.That(actual[5], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "B Minor Waltz" }));
            Assert.That(actual[6], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "You Must Believe In Spring" }));
            Assert.That(actual[7], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "Gary's Theme" }));
            Assert.That(actual[8], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "We Will Meet Again" }));
            Assert.That(actual[9], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "The Peacocks" }));
            Assert.That(actual[10], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "Sometime Ago" }));
            Assert.That(actual[11], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "Theme From M*A*S*H" }));
        }

        [Test]
        // NOTE: This query produces exactly the same results as SelectWithCollectionSelectorTest3() above.
        public void SelectWithCollectionSelectorThatTakesAnIndexToo()
        {

            // Arrange, Act
            var actual = _albums.SelectMany(
                (album, index) => album.Tracks.Select(track => new {AlbumIndex = index, track.Title})
            ).ToList();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(5 + 7));

            Assert.That(actual[0], Is.EqualTo(new {AlbumIndex = 0, Title = "So What"}));
            Assert.That(actual[1], Is.EqualTo(new {AlbumIndex = 0, Title = "Freddy Freeloader"}));
            Assert.That(actual[2], Is.EqualTo(new {AlbumIndex = 0, Title = "Blue in Green"}));
            Assert.That(actual[3], Is.EqualTo(new {AlbumIndex = 0, Title = "All Blues"}));
            Assert.That(actual[4], Is.EqualTo(new {AlbumIndex = 0, Title = "Flamenco Sketches"}));

            Assert.That(actual[5], Is.EqualTo(new { AlbumIndex = 1, Title = "B Minor Waltz" }));
            Assert.That(actual[6], Is.EqualTo(new { AlbumIndex = 1, Title = "You Must Believe In Spring" }));
            Assert.That(actual[7], Is.EqualTo(new { AlbumIndex = 1, Title = "Gary's Theme" }));
            Assert.That(actual[8], Is.EqualTo(new { AlbumIndex = 1, Title = "We Will Meet Again" }));
            Assert.That(actual[9], Is.EqualTo(new { AlbumIndex = 1, Title = "The Peacocks" }));
            Assert.That(actual[10], Is.EqualTo(new { AlbumIndex = 1, Title = "Sometime Ago" }));
            Assert.That(actual[11], Is.EqualTo(new { AlbumIndex = 1, Title = "Theme From M*A*S*H" }));
        }

        [Test]
        public void SelectWithCollectionSelectorAndResultSelector()
        {
            // Arrange, Act
            var actual = _albums.SelectMany(
                album => album.Tracks,
                (album, track) => new {AlbumTitle = album.Title, TrackTitle = track.Title}
            ).ToList();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(5 + 7));

            Assert.That(actual[0], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "So What" }));
            Assert.That(actual[1], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "Freddy Freeloader" }));
            Assert.That(actual[2], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "Blue in Green" }));
            Assert.That(actual[3], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "All Blues" }));
            Assert.That(actual[4], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackTitle = "Flamenco Sketches" }));

            Assert.That(actual[5], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "B Minor Waltz" }));
            Assert.That(actual[6], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "You Must Believe In Spring" }));
            Assert.That(actual[7], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "Gary's Theme" }));
            Assert.That(actual[8], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "We Will Meet Again" }));
            Assert.That(actual[9], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "The Peacocks" }));
            Assert.That(actual[10], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "Sometime Ago" }));
            Assert.That(actual[11], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackTitle = "Theme From M*A*S*H" }));
        }

        [Test]
        public void SelectWithCollectionSelectorThatTakesAnIndexTooAndResultSelector()
        {
            // Arrange, Act
            var actual = _albums.SelectMany(
                (album, index) => album.Tracks.Select(track => new { AlbumIndex = index, track.Title }),
                (album, trackData) => new { AlbumTitle = album.Title, TrackData = trackData }
            ).ToList();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(5 + 7));

            Assert.That(actual[0], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackData = new { AlbumIndex = 0, Title = "So What" }}));
            Assert.That(actual[1], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackData = new { AlbumIndex = 0, Title = "Freddy Freeloader" } }));
            Assert.That(actual[2], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackData = new { AlbumIndex = 0, Title = "Blue in Green" } }));
            Assert.That(actual[3], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackData = new { AlbumIndex = 0, Title = "All Blues" } }));
            Assert.That(actual[4], Is.EqualTo(new { AlbumTitle = "Kind Of Blue", TrackData = new { AlbumIndex = 0, Title = "Flamenco Sketches" } }));

            Assert.That(actual[5], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackData = new { AlbumIndex = 1, Title = "B Minor Waltz" } }));
            Assert.That(actual[6], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackData = new { AlbumIndex = 1, Title = "You Must Believe In Spring" } }));
            Assert.That(actual[7], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackData = new { AlbumIndex = 1, Title = "Gary's Theme" } }));
            Assert.That(actual[8], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackData = new { AlbumIndex = 1, Title = "We Will Meet Again" } }));
            Assert.That(actual[9], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackData = new { AlbumIndex = 1, Title = "The Peacocks" } }));
            Assert.That(actual[10], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackData = new { AlbumIndex = 1, Title = "Sometime Ago" } }));
            Assert.That(actual[11], Is.EqualTo(new { AlbumTitle = "You Must Believe in Spring", TrackData = new { AlbumIndex = 1, Title = "Theme From M*A*S*H" } }));
        }
    }
}
