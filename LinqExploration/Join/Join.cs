using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LinqExploration.Join
{
    [TestFixture]
    internal class Join
    {
        [Test]
        public void JoinUsingImplicitDefaultComparer()
        {
            var actual = AlbumData.AlbumData.Reviews1.Join(
                AlbumData.AlbumData.Artists1.SelectMany(x => x.Albums),
                ar1 => ar1.AlbumId,
                a1 => a1.AlbumId,
                (ar2, a2) => new {
                    a2.Title,
                    Review = ar2.ReviewText ?? ar2.ReviewUrl
                }).ToList();

            Assert.That(actual.Count, Is.EqualTo(4));

            Assert.That(actual[0].Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[0].Review, Is.EqualTo("http://www.bbc.co.uk/music/reviews/fr25"));

            Assert.That(actual[1].Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[1].Review, Is.EqualTo("Deeply cool stuff"));

            Assert.That(actual[2].Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[2].Review, Is.EqualTo("http://www.bbc.co.uk/music/reviews/fr25"));

            Assert.That(actual[3].Title, Is.EqualTo("You Must Believe in Spring"));
            Assert.That(actual[3].Review, Is.EqualTo("http://www.allaboutjazz.com/php/article.php?id=13726"));
        }

        [Test]
        public void JoinUsingExplicitDefaultComparer()
        {
            var actual = AlbumData.AlbumData.Reviews1.Join(
                AlbumData.AlbumData.Artists1.SelectMany(x => x.Albums),
                ar1 => ar1.AlbumId,
                a1 => a1.AlbumId,
                (ar2, a2) => new
                {
                    a2.Title,
                    Review = ar2.ReviewText ?? ar2.ReviewUrl
                },
                EqualityComparer<string>.Default).ToList();

            Assert.That(actual.Count, Is.EqualTo(4));

            Assert.That(actual[0].Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[0].Review, Is.EqualTo("http://www.bbc.co.uk/music/reviews/fr25"));

            Assert.That(actual[1].Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[1].Review, Is.EqualTo("Deeply cool stuff"));

            Assert.That(actual[2].Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[2].Review, Is.EqualTo("http://www.bbc.co.uk/music/reviews/fr25"));

            Assert.That(actual[3].Title, Is.EqualTo("You Must Believe in Spring"));
            Assert.That(actual[3].Review, Is.EqualTo("http://www.allaboutjazz.com/php/article.php?id=13726"));
        }
    }
}
