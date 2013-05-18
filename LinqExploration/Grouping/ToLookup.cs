using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Grouping
{
    [TestFixture]
    internal class ToLookup
    {
        [Test]
        public void ToLookupGivenABunchOfTracksGroupsThemAccordingToTheGivenKeySelectorFunc()
        {
            var tracks = AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
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
            var tracks = AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
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
            var tracks = AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks).ToList();
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
            var tracks = AlbumData.Artists1.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks).ToList();
            var comparer = new TrackLengthInSecondsEqualityComparer();
            var lookup = tracks.ToLookup(t => t.LengthInSeconds, t => t.Title, comparer);
            Assert.That(lookup[tracks.First().LengthInSeconds].Count(), Is.EqualTo(3));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "So What"));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "Freddy Freeloader"));
            Assert.That(lookup[tracks.First().LengthInSeconds], Has.Exactly(1).Matches<string>(s => s == "Flamenco Sketches"));
        }

        // This comparer considers two track lengths to be the same if they are within 30 seconds of each other.
        internal class TrackLengthInSecondsEqualityComparer : IEqualityComparer<int>
        {
            public bool Equals(int trackLengthInSeconds1, int trackLengthInSeconds2)
            {
                var difference = trackLengthInSeconds1 - trackLengthInSeconds2;
                return (Math.Abs(difference)) < 30;
            }

            public int GetHashCode(int trackLengthInSeconds)
            {
                // If Equals() regards x and y as equal, then GetHasCode() is meant to
                // return the same value for x and y. If we want x = 500 and y = 515 to be
                // considered to be equal, then GetHashCode() must return the same value
                // for an input of 500 and an input of 515. The easiest way to do this
                // is just to return a constant value. This will force ToLookup() to
                // use Equals() to test for equality. Put another way, if GetHashCode()
                // returns different values for inputs of 500 and 515 then ToLookup() already
                // knows that 500 and 515 are not equal so it doesn't need to call Equals().
                return 53;
            }
        }
    }
}
