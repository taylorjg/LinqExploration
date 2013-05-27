using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Merging
{
    [TestFixture]
    internal class Zip
    {
        [Test]
        public void ZipMergesTwoSequencesTogether()
        {
            var enumerableSpy1 = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var enumerableSpy2 = new EnumerableSpy<string>(new[] {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"});
            var actual = enumerableSpy1.Zip(enumerableSpy2, (n, s) => new {Number = n, String = s});
            Assert.That(actual, Is.EquivalentTo(new[]
                {
                    new {Number = 1, String = "One"},
                    new {Number = 2, String = "Two"},
                    new {Number = 3, String = "Three"},
                    new {Number = 4, String = "Four"},
                    new {Number = 5, String = "Five"},
                    new {Number = 6, String = "Six"},
                    new {Number = 7, String = "Seven"},
                    new {Number = 8, String = "Eight"},
                    new {Number = 9, String = "Nine"},
                    new {Number = 10, String = "Ten"}
                }));
        }

        [Test]
        public void ZipMergesTwoSequencesTogetherButOnlyForTheLengthOfTheShortestSequenceVersion1()
        {
            var enumerableSpy1 = new EnumerableSpy<int>(Enumerable.Range(1, 5));
            var enumerableSpy2 = new EnumerableSpy<string>(new[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" });
            var actual = enumerableSpy1.Zip(enumerableSpy2, (n, s) => new { Number = n, String = s });
            Assert.That(actual, Is.EquivalentTo(new[]
                {
                    new {Number = 1, String = "One"},
                    new {Number = 2, String = "Two"},
                    new {Number = 3, String = "Three"},
                    new {Number = 4, String = "Four"},
                    new {Number = 5, String = "Five"}
                }));
        }

        [Test]
        public void ZipMergesTwoSequencesTogetherButOnlyForTheLengthOfTheShortestSequenceVersion2()
        {
            var enumerableSpy1 = new EnumerableSpy<int>(Enumerable.Range(1, 10));
            var enumerableSpy2 = new EnumerableSpy<string>(new[] {"One", "Two", "Three"});
            var actual = enumerableSpy1.Zip(enumerableSpy2, (n, s) => new { Number = n, String = s });
            Assert.That(actual, Is.EquivalentTo(new[]
                {
                    new {Number = 1, String = "One"},
                    new {Number = 2, String = "Two"},
                    new {Number = 3, String = "Three"}
                }));
        }
    }
}
