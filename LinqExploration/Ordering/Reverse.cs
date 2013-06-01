using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Ordering
{
    [TestFixture]
    internal class Reverse
    {
        [Test]
        public void ReverseReversesTheOrderOfElementsInASequence()
        {
            var actual = Enumerable.Range(14, 6).Reverse();
            Assert.That(actual, Is.EqualTo(new[] { 19, 18, 17, 16, 15, 14 }));
        }
    }
}
