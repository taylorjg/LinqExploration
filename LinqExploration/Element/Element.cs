using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Element
{
    [TestFixture]
    internal class Element
    {
        [Test]
        public void ElementAtReturnsTheElementAtTheGivenIndex()
        {
            var album = AlbumData.AlbumData.Artists1.First().Albums.First();
            var element = album.Tracks.ElementAt(3);
            Assert.That(element, Is.SameAs(album.Tracks[3]));
        }

        [Test]
        public void ElementAtOrDefaultReturnsTheElementAtTheGivenIndex()
        {
            var album = AlbumData.AlbumData.Artists1.First().Albums.First();
            var element = album.Tracks.ElementAtOrDefault(3);
            Assert.That(element, Is.SameAs(album.Tracks[3]));
        }

        [Test]
        public void ElementAtOrDefaultReturnsNullWhenTheGivenIndexIsBeyondTheEndOfTheSequence()
        {
            var album = AlbumData.AlbumData.Artists1.First().Albums.First();
            var element = album.Tracks.ElementAtOrDefault(99);
            Assert.That(element, Is.Null);
        }
    }
}
