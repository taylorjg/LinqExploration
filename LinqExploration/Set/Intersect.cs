using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Set
{
    [TestFixture]
    internal class Intersect
    {
        [Test]
        public void IntersectReturnsElementsInBothSequence1AndInSequence2()
        {
            var sequence1 = Enumerable.Range(1, 10); // 1..10
            var sequence2 = Enumerable.Range(5, 11); // 5..15
            var actual = sequence1.Intersect(sequence2);
            Assert.That(actual, Is.EquivalentTo(new[] {5, 6, 7, 8, 9, 10}));
        }
    }
}
