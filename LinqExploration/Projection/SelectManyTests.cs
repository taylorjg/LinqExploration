using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable LoopCanBeConvertedToQuery

namespace LinqExploration.Projection
{
    [TestFixture]
    internal class SelectManyTests
    {
        [Test]
        public void SelectManyToGetTheTracksOfAllAlbums()
        {
            var tracks = (AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks)).ToList();

            var totalNumberOfTracks = 0;
            foreach (var artist in AlbumData.Artists1)
            {
                foreach (var album in artist.Albums)
                {
                    totalNumberOfTracks += album.Tracks.Count();
                }
            }

            Assert.That(tracks, Is.InstanceOf<List<Track>>());
            Assert.That(tracks.Count(), Is.EqualTo(totalNumberOfTracks));
        }
    }
}
