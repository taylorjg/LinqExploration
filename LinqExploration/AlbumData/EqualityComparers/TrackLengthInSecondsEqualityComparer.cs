using System;
using System.Collections.Generic;

namespace LinqExploration.AlbumData.EqualityComparers
{
    // This comparer considers two track lengths to be the same if they are within 30 seconds of each other.
    internal class TrackLengthInSecondsEqualityComparer : IEqualityComparer<int>
    {
        public bool Equals(int trackLengthInSeconds1, int trackLengthInSeconds2)
        {
            var difference = trackLengthInSeconds1 - trackLengthInSeconds2;
            return (Math.Abs(difference)) < 30;
        }

        public int GetHashCode(int trackLengthInSeconds)
        {
            // If Equals() regards x and y as equal, then GetHasCode() is meant to
            // return the same value for x and y. If we want x = 500 and y = 515 to be
            // considered to be equal, then GetHashCode() must return the same value
            // for an input of 500 and an input of 515. The easiest way to do this
            // is just to return a constant value. This will force ToLookup() to
            // use Equals() to test for equality. Put another way, if GetHashCode()
            // returns different values for inputs of 500 and 515 then ToLookup() already
            // knows that 500 and 515 are not equal so it doesn't need to call Equals().
            return 53;
        }
    }
}
