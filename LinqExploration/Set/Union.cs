using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Set
{
    [TestFixture]
    internal class Union
    {
        [Test]
        public void UnionReturnsElementsInEitherSequence1OrInSequence2()
        {
            var sequence1 = Enumerable.Range(1, 10); // 1..10
            var sequence2 = Enumerable.Range(5, 11); // 5..15
            var actual = sequence1.Union(sequence2);
            Assert.That(actual, Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }));
        }
    }
}
