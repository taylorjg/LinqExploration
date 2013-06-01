using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Aggregation
{
    [TestFixture]
    internal class Min
    {
        [Test]
        public void MinWithoutSelector()
        {
            // Arrange
            // n:     10 9 8 7 6 5 4 3 2 1 20 19 18 17 16 15 14 13 12 11
            // index: 0  1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19
            var numbers = Enumerable.Range(11, 10)
                .Concat(Enumerable.Range(1, 10))
                .Reverse();

            // Act
            var actual = numbers.Min();

            // Assert
            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void MinWithSelector()
        {
            // Arrange
            var tracks = AlbumData.AlbumData.Artists.First().Albums.First().Tracks;

            // Act
            var actual = tracks.Min(t => t.LengthInSeconds);

            // Assert
            Assert.That(actual, Is.EqualTo(tracks.Single(t => t.TrackNumber == 3).LengthInSeconds));
        }
    }
}
