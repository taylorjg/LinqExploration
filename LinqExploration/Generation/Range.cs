using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Generation
{
    [TestFixture]
    internal class Range
    {
        [Test]
        public void RangeGeneratesAGivenNumberOfConsecutiveNumbersStartingFromAGivenNumber()
        {
            var actual = Enumerable.Range(13, 5);
            Assert.That(actual, Is.EquivalentTo(new[]{13, 14, 15, 16, 17}));
        }
    }
}
