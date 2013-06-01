using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Set
{
    [TestFixture]
    internal class Except
    {
        [Test]
        public void ExceptReturnsElementsInSequence1ThatAreNotInSequence2()
        {
            var sequence1 = Enumerable.Range(1, 10); // 1..10
            var sequence2 = Enumerable.Range(5, 11); // 5..15
            var actual = sequence1.Except(sequence2);
            Assert.That(actual, Is.EqualTo(new[] { 1, 2, 3, 4 }));
        }
    }
}
