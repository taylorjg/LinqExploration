using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Element
{
    [TestFixture]
    internal class ElementAt
    {
        [Test]
        public void ElementAtReturnsTheElementAtTheGivenIndex()
        {
            var album = SampleData.Artists.First().Albums.First();
            var element = album.Tracks.ElementAt(3);
            Assert.That(element, Is.SameAs(album.Tracks[3]));
        }
    }
}
