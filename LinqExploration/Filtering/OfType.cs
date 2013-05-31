using System.Collections.Generic;
using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

namespace LinqExploration.Filtering
{
    [TestFixture]
    internal class OfType
    {
        [Test]
        public void OfTypeGivenASequenceContainingDifferentTypesReturnsASequenceContainingJustTheGivenType()
        {
            // Arrange
            var albums = AlbumData.AlbumData.Artists.SelectMany(a => a.Albums).ToList();
            var tracks = albums.SelectMany(album => album.Tracks).ToList();
            var objects = new List<object>();
            objects.AddRange(albums);
            objects.AddRange(tracks);
            objects.AddRange(AlbumData.AlbumData.Reviews);

            // Act
            var actual1 = objects.OfType<Album>();
            var actual2 = objects.OfType<Track>();
            var actual3 = objects.OfType<AlbumReview>();

            // Assert
            Assert.That(actual1, Is.All.TypeOf<Album>());
            Assert.That(actual2, Is.All.TypeOf<Track>());
            Assert.That(actual3, Is.All.TypeOf<AlbumReview>());
        }
    }
}
