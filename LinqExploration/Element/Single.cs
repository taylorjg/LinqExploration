using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace LinqExploration.Element

{
    [TestFixture]
    internal class Single
    {
        [Test]
        public void SingleWithoutPredicateGivenASequenceContainingASingleElementReturnsThatElement()
        {
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            var track = new[] { tracks.ElementAt(2) };
            var actual = track.Single();
            Assert.That(actual.TrackNumber, Is.EqualTo(3));
            Assert.That(actual, Is.SameAs(tracks.ElementAt(2)));
        }

        [Test]
        public void SingleWithoutPredicateGivenASequenceContainingMoreThanOneElementThrowsAnException()
        {
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            Assert.Throws<System.InvalidOperationException>(() => tracks.Single());
        }

        [Test]
        public void SingleWithPredicateEnumeratesTheEntireSequence()
        {
            // Arrange
            var tracks = SampleData.Artists.First().Albums.First().Tracks;
            var enumerableSpy = new EnumerableSpy<Track>(tracks);

            // Act
            var actual = enumerableSpy.Single(t => t.TrackNumber == 3);

            // Assert
            Assert.That(actual.TrackNumber, Is.EqualTo(3));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(5 + 1));
        }

        [Test]
        public void SingleWithoutPredicateThrowsAnExceptionIfTheSequenceIsEmpty()
        {
            Assert.Throws<System.InvalidOperationException>(() => Enumerable.Empty<int>().Single());
        }

        [Test]
        public void SingleWithPredicateThrowsAnExceptionIfTheSequenceIsEmpty()
        {
            Assert.Throws<System.InvalidOperationException>(() => Enumerable.Empty<int>().Single(n => n == 99));
        }

        [Test]
        public void SingleWithPredicateThrowsAnExceptionIfNoElementsSatisfyThePredicate()
        {
            // Arrange
            var numbers = new[] {1, 2, 3, 1, 2, 3, 1, 2, 3};
            var enumerableSpy = new EnumerableSpy<int>(numbers);

            // Act, Assert
            Assert.Throws<System.InvalidOperationException>(() => enumerableSpy.Single(n => n == 4));
        }

        [Test]
        public void SingleWithPredicateThrowsAnExceptionIfMoreThanOneElementSatisfiesThePredicate()
        {
            // Arrange
            var numbers = new[] {1, 2, 3, 1, 2, 3, 1, 2, 3};
            var enumerableSpy = new EnumerableSpy<int>(numbers);

            // Act, Assert
            Assert.Throws<System.InvalidOperationException>(() => enumerableSpy.Single(n => n == 3));

            // I expected Single() to only enumerate as far as the second occurrence of 3
            // i.e. 6 calls to MoveNext() but it actually enumerates the entire sequence!
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(9 + 1));
        }
    }
}
