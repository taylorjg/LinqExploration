using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Generation
{
    [TestFixture]
    internal class Empty
    {
        [Test]
        public void EmptyReturnsAnEmptyEnumerable()
        {
            var actual = Enumerable.Empty<int>();
            Assert.That(actual, Is.EquivalentTo(new int[] {}));
        }
    }
}
