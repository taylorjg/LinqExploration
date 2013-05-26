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
            var artistNames = from artist in AlbumData.AlbumData.Artists1 select artist.Name;
            var numArtists = AlbumData.AlbumData.Artists1.Count();
            Assert.That(artistNames, Is.AssignableTo<IEnumerable<string>>());
            Assert.That(artistNames.Count(), Is.EqualTo(numArtists));
        }

        [Test]
        public void NestedSelectsToGetAllTracksViaAllAlbumsOfAllArtists()
        {
            var tracks = (from artist in AlbumData.AlbumData.Artists1 select from album in artist.Albums select album.Tracks).ToList();

            Assert.That(tracks, Is.AssignableTo<IEnumerable<IEnumerable<IEnumerable<Track>>>>());

            Assert.That(tracks.First(), Is.AssignableTo<IEnumerable<IEnumerable<Track>>>());
            Assert.That(tracks.First().Count(), Is.EqualTo(AlbumData.AlbumData.Artists1.First().Albums.Count()));

            Assert.That(tracks.First().First(), Is.AssignableTo<IEnumerable<Track>>());
            Assert.That(tracks.First().First().Count(), Is.EqualTo(AlbumData.AlbumData.Artists1.First().Albums.First().Tracks.Count()));
        }
    }
}
