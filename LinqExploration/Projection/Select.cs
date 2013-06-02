using System.Collections.Generic;
using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

namespace LinqExploration.Projection
{
    [TestFixture]
    internal class Select
    {
        [Test]
        public void SelectWithoutIndex()
        {
            var actual = SampleData.Artists.Select(artist => artist.Name);
            Assert.That(actual.Count(), Is.EqualTo(SampleData.Artists.Count()));
        }

        [Test]
        public void SelectWithIndex()
        {
            var actual = SampleData.Artists.Select((artist, index) => new {artist.Name, Index = index});
            Assert.That(actual.Count(), Is.EqualTo(SampleData.Artists.Count()));
        }

        [Test]
        public void SelectNested()
        {
            var actual = SampleData.Artists.Select(artist => artist.Albums.Select(album => album.Tracks)).ToList();
            Assert.That(actual.First().Count(), Is.EqualTo(SampleData.Artists.First().Albums.Count()));
            Assert.That(actual.First().First().Count(), Is.EqualTo(SampleData.Artists.First().Albums.First().Tracks.Count()));
        }
    }
}
