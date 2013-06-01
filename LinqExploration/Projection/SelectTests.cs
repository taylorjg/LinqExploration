using System.Collections.Generic;
using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

namespace LinqExploration.Projection
{
    [TestFixture]
    internal class SelectTests
    {
        [Test]
        public void SimpleSelectToGetArtistNames()
        {
            var artistNames = from artist in SampleData.Artists select artist.Name;
            var numArtists = SampleData.Artists.Count();
            Assert.That(artistNames, Is.AssignableTo<IEnumerable<string>>());
            Assert.That(artistNames.Count(), Is.EqualTo(numArtists));
        }

        [Test]
        public void NestedSelectsToGetAllTracksViaAllAlbumsOfAllArtists()
        {
            var tracks = (from artist in SampleData.Artists select from album in artist.Albums select album.Tracks).ToList();

            Assert.That(tracks, Is.AssignableTo<IEnumerable<IEnumerable<IEnumerable<Track>>>>());

            Assert.That(tracks.First(), Is.AssignableTo<IEnumerable<IEnumerable<Track>>>());
            Assert.That(tracks.First().Count(), Is.EqualTo(SampleData.Artists.First().Albums.Count()));

            Assert.That(tracks.First().First(), Is.AssignableTo<IEnumerable<Track>>());
            Assert.That(tracks.First().First().Count(), Is.EqualTo(SampleData.Artists.First().Albums.First().Tracks.Count()));
        }
    }
}
