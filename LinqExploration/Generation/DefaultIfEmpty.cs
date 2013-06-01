using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Generation
{
    [TestFixture]
    internal class DefaultIfEmpty
    {
        [Test]
        public void DefaultIfEmptyWithoutDefaultValue()
        {
            var actual = new int[] {}.DefaultIfEmpty();
            Assert.That(actual, Is.EqualTo(new[] {0}));
        }

        [Test]
        public void DefaultIfEmptyWithDefaultValue()
        {
            var actual = new int[] { }.DefaultIfEmpty(76);
            Assert.That(actual, Is.EqualTo(new[] { 76 }));
        }

        [Test]
        public void DefaultIfEmptyThrowsArgumentNullExceptionWhenSourceIsNull()
        {
// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<System.ArgumentNullException>(() => Enumerable.DefaultIfEmpty((IEnumerable<int>) null));
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
// ReSharper restore AssignNullToNotNullAttribute
// ReSharper restore InvokeAsExtensionMethod
        }
    }
}
