using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Projection
{
    [TestFixture]
    internal class Select
    {
        [Test]
        public void SelectWithoutIndex()
        {
            var actual = SampleData.Artists.Select(artist => artist.Name).ToList();
            Assert.That(actual.Count, Is.EqualTo(SampleData.Artists.Count()));
            Assert.That(actual[0], Is.EqualTo("Miles Davis"));
            Assert.That(actual[1], Is.EqualTo("Bill Evans"));
        }

        [Test]
        public void SelectWithIndex()
        {
            var actual = SampleData.Artists.Select((artist, index) => new {artist.Name, Index = index}).ToList();
            Assert.That(actual.Count, Is.EqualTo(SampleData.Artists.Count()));
            Assert.That(actual[0].Name, Is.EqualTo("Miles Davis"));
            Assert.That(actual[0].Index, Is.EqualTo(0));
            Assert.That(actual[1].Name, Is.EqualTo("Bill Evans"));
            Assert.That(actual[1].Index, Is.EqualTo(1));
        }

        [Test]
        public void SelectNested()
        {
            var actual = SampleData.Artists.Select(artist => artist.Albums.Select(album => album.Tracks)).ToList();
            Assert.That(actual.First().Count(), Is.EqualTo(SampleData.Artists.First().Albums.Count()));
            Assert.That(actual.First().First().Count(), Is.EqualTo(SampleData.Artists.First().Albums.First().Tracks.Count()));
            Assert.That(actual.First().First().ElementAt(0).TrackNumber, Is.EqualTo(1));
            Assert.That(actual.First().First().ElementAt(1).TrackNumber, Is.EqualTo(2));
            Assert.That(actual.First().First().ElementAt(2).TrackNumber, Is.EqualTo(3));
            Assert.That(actual.First().First().ElementAt(3).TrackNumber, Is.EqualTo(4));
            Assert.That(actual.First().First().ElementAt(4).TrackNumber, Is.EqualTo(5));
        }
    }
}
