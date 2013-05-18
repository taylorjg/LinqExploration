namespace LinqExploration
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
                                                        new Track {Title = "So What", Length = "9:25"},
                                                        new Track {Title = "Freddy Freeloader", Length = "9:49"},
                                                        new Track {Title = "Blue in Green", Length = "5:37"},
                                                        new Track {Title = "All Blues", Length = "11:35"},
                                                        new Track {Title = "Flamenco Sketches", Length = "9:26"}
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
                                                        new Track {Title = "B Minor Waltz", Length = "3:13"},
                                                        new Track {Title = "You Must Believe In Spring", Length = "5:40"},
                                                        new Track {Title = "Gary's Theme", Length = "4:15"},
                                                        new Track {Title = "We Will Meet Again", Length = "3:59"},
                                                        new Track {Title = "The Peacocks", Length = "5:58"},
                                                        new Track {Title = "Sometime Ago", Length = "4:33"},
                                                        new Track {Title = "Theme From M*A*S*H", Length = "5:55"}
                                                    }
                                            }
                                    }
                            }
                    };
            }
        }
    }
}
