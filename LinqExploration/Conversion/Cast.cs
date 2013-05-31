using System.Collections.Generic;
using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

namespace LinqExploration.Conversion
{
    [TestFixture]
    internal class Cast
    {
        [Test]
        public void CastCastsEachElementInASequence()
        {
            // Arrange
            var albums = AlbumData.AlbumData.Artists.SelectMany(a => a.Albums).ToList();
            var objects = new List<object>();
            objects.AddRange(albums);

            // Act
            IList<Album> actual = objects.Cast<Album>().ToList();

            // Assert
            Assert.That(actual, Is.All.TypeOf<Album>());
        }

        [Test]
        public void CastThrowsInvalidCastExceptionWhenAskedToCastToWrongType()
        {
            // Arrange
            var albums = AlbumData.AlbumData.Artists.SelectMany(a => a.Albums).ToList();
            var objects = new List<object>();
            objects.AddRange(albums);

            // Act, Assert
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<System.InvalidCastException>(() => objects.Cast<Track>().ToList());
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }
    }
}
