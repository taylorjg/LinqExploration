using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Filtering
{
    [TestFixture]
    internal class Where
    {
        [Test]
        public void WhereReturnsElementsThatMatchTheGivenPredicate()
        {
            // Arange
            var tracks = AlbumData.AlbumData.Artists.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);

            // Act
            var actual = tracks.Where(t => t.Title.Contains("Blue") || t.Title.Contains("Green")).Select(t => t.TrackNumber);

            // Assert
            Assert.That(actual, Is.EqualTo(new[] {3, 4}));
        }

        [Test]
        public void WhereReturnsElementsThatMatchTheGivenPredicateThisTimeWithIndex()
        {
            // Arrange
            // n:     10 9 8 7 6 5 4 3 2 1 20 19 18 17 16 15 14 13 12 11
            // index: 0  1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19
            //        *  * * * *           *  *  *  *  *
            var numbers = Enumerable.Range(11, 10)
                .Concat(Enumerable.Range(1, 10))
                .Reverse();

            // Act
            var actual = numbers.Where((n, index) => n > index);

            // Assert
            Assert.That(actual, Is.EqualTo(new[] {10, 9, 8, 7, 6, 20, 19, 18, 17, 16}));
        }
    }
}
