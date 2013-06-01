using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Element
{
    [TestFixture]
    internal class ElementAtOrDefault
    {
        [Test]
        public void ElementAtOrDefaultReturnsTheElementAtTheGivenIndex()
        {
            var album = SampleData.Artists.First().Albums.First();
            var element = album.Tracks.ElementAtOrDefault(3);
            Assert.That(element, Is.SameAs(album.Tracks[3]));
        }

        [Test]
        public void ElementAtOrDefaultReturnsNullWhenTheGivenIndexIsBeyondTheEndOfTheSequence()
        {
            var album = SampleData.Artists.First().Albums.First();
            var element = album.Tracks.ElementAtOrDefault(99);
            Assert.That(element, Is.Null);
        }
    }
}
