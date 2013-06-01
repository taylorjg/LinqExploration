using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

namespace LinqExploration.Generation
{
    [TestFixture]
    internal class Empty
    {
        [Test]
        public void EmptyReturnsAnEmptyEnumerable()
        {
            // Arrange, Act
            var actual = Enumerable.Empty<int>();

            // Assert
            Assert.That(actual, Is.EqualTo(new int[] { }));
        }

        [Test]
        public void EmptyReturnsTheSameObjectInstanceEachTimeItIsCalledForAGivenType()
        {
            // Arrange, Act
            var actual1 = Enumerable.Empty<int>();
            var actual2 = Enumerable.Empty<int>();
            var actual3 = Enumerable.Empty<int>();

            // Assert
            Assert.That(actual1, Is.SameAs(actual2));
            Assert.That(actual1, Is.SameAs(actual3));
        }

        [Test]
        public void EmptyReturnsTheSameObjectInstanceEachTimeItIsCalledForAGivenTypeVersion2()
        {
            // Arrange, Act
            var actual = Enumerable.Range(1, 10).Select(n => Enumerable.Empty<Track>());

            // Assert
            Assert.That(actual, Has.All.SameAs(actual.First()));
        }
    }
}
