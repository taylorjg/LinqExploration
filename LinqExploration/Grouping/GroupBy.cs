using System.Linq;
using LinqExploration.AlbumData;
using LinqExploration.AlbumData.EqualityComparers;
using NUnit.Framework;

namespace LinqExploration.Grouping
{
    [TestFixture]
    internal class GroupBy
    {
        [Test]
        public void GroupByWithKeySelector()
        {
            var tracks = AlbumData.AlbumData.Artists.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            var actual = enumerableSpy.GroupBy(t => t.TrackNumber);
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(0));

            var actualAsAList = actual.ToList();
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5 + 7 + 1));

            Assert.That(actualAsAList[0].Key, Is.EqualTo(1));
            Assert.That(actualAsAList[0].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[1].Key, Is.EqualTo(2));
            Assert.That(actualAsAList[1].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[2].Key, Is.EqualTo(3));
            Assert.That(actualAsAList[2].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[3].Key, Is.EqualTo(4));
            Assert.That(actualAsAList[3].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[4].Key, Is.EqualTo(5));
            Assert.That(actualAsAList[4].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[5].Key, Is.EqualTo(6));
            Assert.That(actualAsAList[5].Count(), Is.EqualTo(1));

            Assert.That(actualAsAList[6].Key, Is.EqualTo(7));
            Assert.That(actualAsAList[6].Count(), Is.EqualTo(1));
        }

        [Test]
        public void GroupByWithKeySelectorAndElementSelector()
        {
            var tracks = AlbumData.AlbumData.Artists.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            var actual = enumerableSpy.GroupBy(
                t => t.TrackNumber,
                t => t.Title);
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(0));

            var actualAsAList = actual.ToList();
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5 + 7 + 1));

            Assert.That(actualAsAList[0].Key, Is.EqualTo(1));
            Assert.That(actualAsAList[0].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[1].Key, Is.EqualTo(2));
            Assert.That(actualAsAList[1].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[2].Key, Is.EqualTo(3));
            Assert.That(actualAsAList[2].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[3].Key, Is.EqualTo(4));
            Assert.That(actualAsAList[3].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[4].Key, Is.EqualTo(5));
            Assert.That(actualAsAList[4].Count(), Is.EqualTo(2));

            Assert.That(actualAsAList[5].Key, Is.EqualTo(6));
            Assert.That(actualAsAList[5].Count(), Is.EqualTo(1));

            Assert.That(actualAsAList[6].Key, Is.EqualTo(7));
            Assert.That(actualAsAList[6].Count(), Is.EqualTo(1));
        }

        [Test]
        public void GroupByWithKeySelectorAndResultSelector()
        {
            var tracks = AlbumData.AlbumData.Artists.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            var actual = enumerableSpy.GroupBy(
                t => t.TrackNumber,
                (trackNumber, tracksWithThisTrackNumber) => new
                    {
                        TrackNumber = trackNumber,
                        TotalLengthInSeconds = tracksWithThisTrackNumber.Sum(t => t.LengthInSeconds)
                    });
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(0));

            var actualAsAList = actual.ToList();
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5 + 7 + 1));

            Assert.That(actualAsAList.Count(), Is.EqualTo(7));

            var tracks1 = AlbumData.AlbumData.Artists[0].Albums.First().Tracks;
            var tracks2 = AlbumData.AlbumData.Artists[1].Albums.First().Tracks;

            Assert.That(actualAsAList[0].TrackNumber, Is.EqualTo(1));
            Assert.That(actualAsAList[0].TotalLengthInSeconds, Is.EqualTo(tracks1[0].LengthInSeconds + tracks2[0].LengthInSeconds));

            Assert.That(actualAsAList[1].TrackNumber, Is.EqualTo(2));
            Assert.That(actualAsAList[1].TotalLengthInSeconds, Is.EqualTo(tracks1[1].LengthInSeconds + tracks2[1].LengthInSeconds));

            Assert.That(actualAsAList[2].TrackNumber, Is.EqualTo(3));
            Assert.That(actualAsAList[2].TotalLengthInSeconds, Is.EqualTo(tracks1[2].LengthInSeconds + tracks2[2].LengthInSeconds));

            Assert.That(actualAsAList[3].TrackNumber, Is.EqualTo(4));
            Assert.That(actualAsAList[3].TotalLengthInSeconds, Is.EqualTo(tracks1[3].LengthInSeconds + tracks2[3].LengthInSeconds));

            Assert.That(actualAsAList[4].TrackNumber, Is.EqualTo(5));
            Assert.That(actualAsAList[4].TotalLengthInSeconds, Is.EqualTo(tracks1[4].LengthInSeconds + tracks2[4].LengthInSeconds));

            Assert.That(actualAsAList[5].TrackNumber, Is.EqualTo(6));
            Assert.That(actualAsAList[5].TotalLengthInSeconds, Is.EqualTo(tracks2[5].LengthInSeconds));

            Assert.That(actualAsAList[6].TrackNumber, Is.EqualTo(7));
            Assert.That(actualAsAList[6].TotalLengthInSeconds, Is.EqualTo(tracks2[6].LengthInSeconds));
        }

        [Test]
        public void GroupByWithKeySelectorAndElementSelectorAndResultSelectorAndComparer()
        {
            var tracks = AlbumData.AlbumData.Artists.SelectMany(artist => artist.Albums).SelectMany(album => album.Tracks);
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            var actual = enumerableSpy.GroupBy(
                /* keySelector */ t => t.LengthInSeconds,
                /* elementSelector */ t => new {t.TrackNumber, t.Title, t.LengthInSeconds, t.Length},
                /* resultSelector */ (key, elements) =>
                    {
                        var elementsList = elements.ToList();
                        return new
                            {
                                MinLength = elementsList.First(e1 => e1.LengthInSeconds == elementsList.Min(e2 => e2.LengthInSeconds)).Length,
                                MaxLength = elementsList.First(e1 => e1.LengthInSeconds == elementsList.Max(e2 => e2.LengthInSeconds)).Length,
                                TrackNumbersAndTitles = string.Join("|", elementsList.Select(e => string.Format("{0}: {1}", e.TrackNumber, e.Title)))
                            };
                    },
                /* comparer */ new SimilarTrackLengthsInSecondsEqualityComparer(30)).ToList();

            Assert.That(actual.First().MinLength, Is.EqualTo("9:25"));
            Assert.That(actual.First().MaxLength, Is.EqualTo("9:49"));
            Assert.That(actual.First().TrackNumbersAndTitles, Is.EqualTo("1: So What|2: Freddy Freeloader|5: Flamenco Sketches"));
        }
    }
}
