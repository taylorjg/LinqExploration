using System.Collections.Generic;
using System.Linq;
using LinqExploration.AlbumData;
using NUnit.Framework;

namespace LinqExploration.Join
{
    [TestFixture]
    internal class GroupJoin
    {
        [Test]
        public void GroupJoinUsingImplicitDefaultComparer()
        {
            var albums = SampleData.Artists.SelectMany(x => x.Albums);
            var reviews = SampleData.Reviews;

            albums = albums.Concat(new[] {new Album
                {
                    AlbumId = "123",
                    Title = "An Album With No Reviews"
                }});

            var actual = albums.GroupJoin(
                reviews,
                album => album.AlbumId,
                albumReview => albumReview.AlbumId,
                (album, albumReviews) => new
                    {
                        Album = album,
                        AlbumReviews = albumReviews
                    }).ToList();

            Assert.That(actual.Count, Is.EqualTo(3));

            Assert.That(actual[0].Album.Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[0].AlbumReviews.Count(), Is.EqualTo(3));

            Assert.That(actual[1].Album.Title, Is.EqualTo("You Must Believe in Spring"));
            Assert.That(actual[1].AlbumReviews.Count(), Is.EqualTo(1));

            Assert.That(actual[2].Album.Title, Is.EqualTo("An Album With No Reviews"));
            Assert.That(actual[2].AlbumReviews.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GroupJoinUsingExplicitDefaultComparer()
        {
            var albums = SampleData.Artists.SelectMany(x => x.Albums);
            var reviews = SampleData.Reviews;

            albums = albums.Concat(new[] {new Album
                {
                    AlbumId = "123",
                    Title = "An Album With No Reviews"
                }});

            var actual = albums.GroupJoin(
                reviews,
                album => album.AlbumId,
                albumReview => albumReview.AlbumId,
                (album, albumReviews) => new
                {
                    Album = album,
                    AlbumReviews = albumReviews
                },
                EqualityComparer<string>.Default).ToList();

            Assert.That(actual.Count, Is.EqualTo(3));

            Assert.That(actual[0].Album.Title, Is.EqualTo("Kind Of Blue"));
            Assert.That(actual[0].AlbumReviews.Count(), Is.EqualTo(3));

            Assert.That(actual[1].Album.Title, Is.EqualTo("You Must Believe in Spring"));
            Assert.That(actual[1].AlbumReviews.Count(), Is.EqualTo(1));

            Assert.That(actual[2].Album.Title, Is.EqualTo("An Album With No Reviews"));
            Assert.That(actual[2].AlbumReviews.Count(), Is.EqualTo(0));
        }
    }
}
