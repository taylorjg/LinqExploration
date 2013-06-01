using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Concatenation
{
    [TestFixture]
    internal class Concat
    {
        [Test]
        public void ConcatConcatenatesSequencesTogether()
        {
            var album1 = SampleData.Artists[0].Albums.First();
            var album2 = SampleData.Artists[1].Albums.First();
            var concatenatedAlbums = album1.Tracks.Concat(album2.Tracks);
            Assert.That(concatenatedAlbums.Count(), Is.EqualTo(album1.Tracks.Count() + album2.Tracks.Count()));
        }
    }
}
