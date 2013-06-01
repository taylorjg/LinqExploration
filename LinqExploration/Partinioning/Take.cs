using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Partinioning
{
    [TestFixture]
    internal class Take
    {
        [Test]
        public void TakeEnumeratesTheGivenNumberOfElements()
        {
            var enumerableSpy = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var actual = enumerableSpy.Take(3);
            Assert.That(actual, Is.EqualTo(new[] { 1, 2, 3 }));
            Assert.That(enumerableSpy.NumCallsToGetEnumerator, Is.EqualTo(1));
            Assert.That(enumerableSpy.NumCallsToMoveNext, Is.EqualTo(3));
            Assert.That(enumerableSpy.NumCallsToDispose, Is.EqualTo(1));
        }
    }
}
