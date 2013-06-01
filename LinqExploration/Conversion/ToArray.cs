using System.Collections.Generic;
using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

namespace LinqExploration.Conversion
{
    [TestFixture]
    internal class ToArray
    {
        [Test]
        public void ToArrayEnumeratesTheEntireCollectionImmediately()
        {
            // Arrange
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            // Act 1
            var trackNumbersQuery = enumerableSpy.Select(t => t.TrackNumber);

            // Assert 1
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(0));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(0));

            // Act 2
            var trackNumbersArray = trackNumbersQuery.ToArray();

            // Assert 2
            Assert.That(trackNumbersArray, Is.InstanceOf<int[]>());
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5 + 1));
        }
    }
}
