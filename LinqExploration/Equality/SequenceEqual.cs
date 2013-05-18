using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Equality
{
    [TestFixture]
    class SequenceEqual
    {
        [Test]
        public void SequenceEqualsReturnsTrueWhenGivenTwoSeqencesThatContainTheSameObjectsUsingTheDefaultEqualityComparer()
        {
            var album = AlbumData.Artists1.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(0).Take(3);
            var actual = tracks1.SequenceEqual(tracks2);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SequenceEqualsReturnsTrueWhenGivenTwoSeqencesThatContainTheSameObjectsUsingAEqualityComparerThatComparesTrackLengthOnly()
        {
            var album = AlbumData.Artists1.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(0).Take(3);
            IEqualityComparer<Track> comparer = new TrackLengthEqualityComparer();
            var actual = tracks1.SequenceEqual(tracks2, comparer);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SequenceEqualsReturnsFalseWhenGivenTwoSeqencesOfTheSameLengthButContainingDifferentObjects()
        {
            var album = AlbumData.Artists1.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(1).Take(3);
            var actual = tracks1.SequenceEqual(tracks2);
            Assert.That(actual, Is.False);
        }

        internal class TrackLengthEqualityComparer : IEqualityComparer<Track>
        {
            public bool Equals(Track track1, Track track2)
            {
                return track1.Length == track2.Length;
            }

            public int GetHashCode(Track track)
            {
                unchecked
                {
                    var hash = 17;
                    hash = hash * 23 + track.Length.GetHashCode();
                    hash = hash * 23 + track.Title.GetHashCode();
                    return hash;
                }
            }
        }
    }
}
