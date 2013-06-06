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
            var album = SampleData.Artists.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(0).Take(3);
            var actual = tracks1.SequenceEqual(tracks2);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SequenceEqualsReturnsTrueWhenGivenTwoSeqencesThatContainTheSameObjectsUsingAEqualityComparerThatComparesTrackLengthOnly()
        {
            var album = SampleData.Artists.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(0).Take(3);
            var comparer = new TrackLengthEqualityComparer();
            var actual = tracks1.SequenceEqual(tracks2, comparer);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SequenceEqualsReturnsFalseWhenGivenTwoSeqencesOfTheSameLengthButContainingDifferentObjects()
        {
            var album = SampleData.Artists.First().Albums.First();
            var tracks1 = album.Tracks.Skip(0).Take(3);
            var tracks2 = album.Tracks.Skip(1).Take(3);
            var actual = tracks1.SequenceEqual(tracks2);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void SequenceEqualGivenTwoEquivalentSequencesOfAnonymousTypesReturnsTrue()
        {
            // Arrange
            var sequence1 = new[] {
                    new { Prop1 = "ABC", Prop2 = 123 },
                    new { Prop1 = "XYZ", Prop2 = 456 }
                };
            var sequence2 = new[] {
                    new { Prop1 = "ABC", Prop2 = 123 },
                    new { Prop1 = "XYZ", Prop2 = 456 }
                };

            // Act
            var actual = sequence1.SequenceEqual(sequence2);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void SequenceEqualGivenTwoSequencesOfEquivalentAnonymousTypesButWithElementsInADifferentOrderReturnsFalse()
        {
            // Arrange
            var sequence1 = new[] {
                    new { Prop1 = "ABC", Prop2 = 123 },
                    new { Prop1 = "XYZ", Prop2 = 456 }
                };
            var sequence2 = new[] {
                    new { Prop1 = "XYZ", Prop2 = 456 },
                    new { Prop1 = "ABC", Prop2 = 123 }
                };

            // Act
            var actual = sequence1.SequenceEqual(sequence2);

            // Assert
            Assert.That(actual, Is.False);
        }
    }
}
