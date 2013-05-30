using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Generation
{
    [TestFixture]
    internal class Repeat
    {
        [Test]
        public void RepeatRepeatsAGivenItemAGivenNumberOfTimes()
        {
            var actual = Enumerable.Repeat(53, 7);
            Assert.That(actual, Is.EquivalentTo(new[] {53, 53, 53, 53, 53, 53, 53}));
        }
    }
}
