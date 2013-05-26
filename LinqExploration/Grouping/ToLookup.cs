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
        public void ToLookupGivenABunchOfTracksGroupsThemAccordingToTheGivenKeySelectorFunc()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var lookup = tracks.ToLookup(t => t.TrackNumber);
            Assert.That(lookup[1].Count(), Is.EqualTo(2));
            Assert.That(lookup[2].Count(), Is.EqualTo(2));
            Assert.That(lookup[3].Count(), Is.EqualTo(2));
            Assert.That(lookup[4].Count(), Is.EqualTo(2));
            Assert.That(lookup[5].Count(), Is.EqualTo(2));
            Assert.That(lookup[6].Count(), Is.EqualTo(1));
            Assert.That(lookup[7].Count(), Is.EqualTo(1));
        }

        [Test]
        public void ToLookupGivenABunchOfTracksGroupsThemAccordingToTheGivenKeySelectorFuncAndThisTimeWithAnElementSelector()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var lookup = tracks.ToLookup(t => t.TrackNumber, t => t.Title);
            Assert.That(lookup[1].Count(), Is.EqualTo(2));
            Assert.That(lookup[2].Count(), Is.EqualTo(2));
            Assert.That(lookup[3].Count(), Is.EqualTo(2));
            Assert.That(lookup[4].Count(), Is.EqualTo(2));
            Assert.That(lookup[5].Count(), Is.EqualTo(2));
            Assert.That(lookup[6].Count(), Is.EqualTo(1));
            Assert.That(lookup[7].Count(), Is.EqualTo(1));
        }

        [Test]
        public void ToLookupGivenABunchOfTracksGroupsThemAccordingToTheGivenKeySelectorFuncUsingTheGivenComparer()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks).ToList();
            var comparer = new TrackLengthInSecondsEqualityComparer();
            var lookup = tracks.ToLookup(t => t.LengthInSeconds, comparer);
            Assert.That(lookup[tracks.First().LengthInSeconds].Count(), Is.EqualTo(3));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<Track>(t => t.Title == "So What"));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<Track>(t => t.Title == "Freddy Freeloader"));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<Track>(t => t.Title == "Flamenco Sketches"));
        }

        [Test]
        public void ToLookupGivenABunchOfTracksGroupsThemAccordingToTheGivenKeySelectorFuncUsingTheGivenComparerAndThisTimeWithAnElementSelector()
        {
            var tracks = AlbumData.AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks).ToList();
            var comparer = new TrackLengthInSecondsEqualityComparer();
            var lookup = tracks.ToLookup(t => t.LengthInSeconds, t => t.Title, comparer);
            Assert.That(lookup[tracks.First().LengthInSeconds].Count(), Is.EqualTo(3));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "So What"));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "Freddy Freeloader"));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "Flamenco Sketches"));
        }
    }
}
