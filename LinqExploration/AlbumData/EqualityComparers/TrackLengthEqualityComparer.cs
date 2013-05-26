using System.Collections.Generic;

namespace LinqExploration.AlbumData.EqualityComparers
{
    internal class TrackLengthEqualityComparer : IEqualityComparer<Track>
    {
        public bool Equals(Track track1, Track track2)
        {
            return track1.Length == track2.Length;
        }

        public int GetHashCode(Track track)
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + track.Length.GetHashCode();
                hash = hash * 23 + track.Title.GetHashCode();
                return hash;
            }
        }
    }
}
