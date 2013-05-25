using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Set
{
    [TestFixture]
    internal class Distinct
    {
        [Test]
        public void DistinctReturnsElementsInASequenceWithoutDuplicates()
        {
            var sequence1 = new[] {1, 2, 3, 4, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10};
            var actual = sequence1.Distinct();
            Assert.That(actual, Is.EquivalentTo(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}));
        }
    }
}
