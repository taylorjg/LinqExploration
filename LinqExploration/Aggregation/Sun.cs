using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Aggregation
{
    [TestFixture]
    internal class Sun
    {
        [Test]
        public void SumWithoutSelector()
        {
            // Arrange
            var numbers = Enumerable.Range(1, 5);

            // Act
            var actual = numbers.Sum();

            // Assert
            Assert.That(actual, Is.EqualTo(1 + 2 + 3 + 4 + 5));
        }

        [Test]
        public void SumWithSelector()
        {
            // Arrange
            var tracks = SampleData.Artists.First().Albums.First().Tracks;

            // Act
            var actual = tracks.Sum(t => t.LengthInSeconds);

            // Assert
            Assert.That(actual, Is.EqualTo(
                tracks[0].LengthInSeconds +
                tracks[1].LengthInSeconds +
                tracks[2].LengthInSeconds +
                tracks[3].LengthInSeconds +
                tracks[4].LengthInSeconds));
        }
    }
}
