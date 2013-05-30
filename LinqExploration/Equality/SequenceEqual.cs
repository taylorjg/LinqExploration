using System.Linq;
using LinqExploration.AlbumData.EqualityComparers;
using NUnit.Framework;

namespace LinqExploration.Equality
{
    [TestFixture]
    class SequenceEqual
    {
        [Test]
        public void SequenceEqualsReturnsTrueWhenGivenTwoSeqencesThatContainTheSameObjectsUsingTheDefaultEqualityComparer()
        {
            var album = AlbumData.AlbumData.Artists.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(0).Take(3);
            var actual = tracks1.SequenceEqual(tracks2);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SequenceEqualsReturnsTrueWhenGivenTwoSeqencesThatContainTheSameObjectsUsingAEqualityComparerThatComparesTrackLengthOnly()
        {
            var album = AlbumData.AlbumData.Artists.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(0).Take(3);
            var comparer = new TrackLengthEqualityComparer();
            var actual = tracks1.SequenceEqual(tracks2, comparer);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SequenceEqualsReturnsFalseWhenGivenTwoSeqencesOfTheSameLengthButContainingDifferentObjects()
        {
            var album = AlbumData.AlbumData.Artists.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(1).Take(3);
            var actual = tracks1.SequenceEqual(tracks2);
            Assert.That(actual, Is.False);
        }
    }
}
