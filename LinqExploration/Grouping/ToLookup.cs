using System.Linq;
using LinqExploration.AlbumData;
using LinqExploration.AlbumData.EqualityComparers;
using NUnit.Framework;

namespace LinqExploration.Grouping
{
    [TestFixture]
    internal class ToLookup
    {
        [Test]
        public void ToLookupWithKeySelector()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            var actual = enumerableSpy.ToLookup(t => t.TrackNumber);
            // The 'tracks' sequence will have been fully enumerated now
            // even though we have not accessed 'actual' yet.
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5 + 7 + 1));

            Assert.That(actual[1].Count(), Is.EqualTo(2));
            Assert.That(actual[2].Count(), Is.EqualTo(2));
            Assert.That(actual[3].Count(), Is.EqualTo(2));
            Assert.That(actual[4].Count(), Is.EqualTo(2));
            Assert.That(actual[5].Count(), Is.EqualTo(2));
            Assert.That(actual[6].Count(), Is.EqualTo(1));
            Assert.That(actual[7].Count(), Is.EqualTo(1));
        }

        [Test]
        public void ToLookupWithKeySelectorAndElementSelector()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var actual = tracks.ToLookup(t => t.TrackNumber, t => t.Title);
            Assert.That(actual[1].Count(), Is.EqualTo(2));
            Assert.That(actual[2].Count(), Is.EqualTo(2));
            Assert.That(actual[3].Count(), Is.EqualTo(2));
            Assert.That(actual[4].Count(), Is.EqualTo(2));
            Assert.That(actual[5].Count(), Is.EqualTo(2));
            Assert.That(actual[6].Count(), Is.EqualTo(1));
            Assert.That(actual[7].Count(), Is.EqualTo(1));
        }

        [Test]
        public void ToLookupWithKeySelectorAndComparer()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks).ToList();
            var comparer = new SimilarTrackLengthsInSecondsEqualityComparer(30);
            var actual = tracks.ToLookup(t => t.LengthInSeconds, comparer);
            Assert.That(actual[tracks.First().LengthInSeconds].Count(), Is.EqualTo(3));
            Assert.That(actual[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<Track>(t => t.Title == "So What"));
            Assert.That(actual[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<Track>(t => t.Title == "Freddy Freeloader"));
            Assert.That(actual[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<Track>(t => t.Title == "Flamenco Sketches"));
        }

        [Test]
        public void ToLookupWithKeySelectorAndElementSelectorAndComparer()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks).ToList();
            var comparer = new SimilarTrackLengthsInSecondsEqualityComparer(30);
            var actual = tracks.ToLookup(t => t.LengthInSeconds, t => t.Title, comparer);
            Assert.That(actual[tracks.First().LengthInSeconds].Count(), Is.EqualTo(3));
            Assert.That(actual[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "So What"));
            Assert.That(actual[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "Freddy Freeloader"));
            Assert.That(actual[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "Flamenco Sketches"));
        }
    }
}
