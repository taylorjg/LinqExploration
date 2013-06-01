using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Aggregation
{
    [TestFixture]
    internal class Count
    {
        [Test]
        public void CountWithoutPredicate()
        {
            // Arrange
            var numbers = Enumerable.Range(1, 5);

            // Act
            var actual = numbers.Count();

            // Assert
            Assert.That(actual, Is.EqualTo(5));
        }

        [Test]
        public void CountWithPredicate()
        {
            // Arrange
            var tracks = SampleData.Artists.First().Albums.First().Tracks;

            // Act
            var actual = tracks.Count(t => t.Title.Contains("Blue") || t.Title.Contains("Green"));

            // Assert
            Assert.That(actual, Is.EqualTo(2));
        }
    }
}
