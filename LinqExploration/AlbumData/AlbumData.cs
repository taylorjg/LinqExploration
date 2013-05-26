namespace LinqExploration.AlbumData
{
    internal static class AlbumData
    {
        public static Artist[] Artists1 {
            get
            {
                return new[]
                    {
                        new Artist
                            {
                                Name = "Miles Davis",
                                Albums = new[]
                                    {
                                        new Album
                                            {
                                                Title = "Kind Of Blue",
                                                Artists = new[] {"Miles Davis", "Bill Evans"},
                                                Tracks = new[]
                                                    {
                                                        new Track {Title = "So What", Length = "9:25", TrackNumber = 1},
                                                        new Track {Title = "Freddy Freeloader", Length = "9:49", TrackNumber = 2},
                                                        new Track {Title = "Blue in Green", Length = "5:37", TrackNumber = 3},
                                                        new Track {Title = "All Blues", Length = "11:35", TrackNumber = 4},
                                                        new Track {Title = "Flamenco Sketches", Length = "9:26", TrackNumber = 5}
                                                    }
                                            }
                                    }
                            },
                        new Artist
                            {
                                Name = "Bill Evans",
                                Albums = new[]
                                    {
                                        new Album
                                            {
                                                Title = "You Must Believe in Spring",
                                                Artists = new[] {"Bill Evans"},
                                                Tracks = new[]
                                                    {
                                                        new Track {Title = "B Minor Waltz", Length = "3:13", TrackNumber = 1},
                                                        new Track {Title = "You Must Believe In Spring", Length = "5:40", TrackNumber = 2},
                                                        new Track {Title = "Gary's Theme", Length = "4:15", TrackNumber = 3},
                                                        new Track {Title = "We Will Meet Again", Length = "3:59", TrackNumber = 4},
                                                        new Track {Title = "The Peacocks", Length = "5:58", TrackNumber = 5},
                                                        new Track {Title = "Sometime Ago", Length = "4:33", TrackNumber = 6},
                                                        new Track {Title = "Theme From M*A*S*H", Length = "5:55", TrackNumber = 7}
                                                    }
                                            }
                                    }
                            }
                    };
            }
        }
    }
}
