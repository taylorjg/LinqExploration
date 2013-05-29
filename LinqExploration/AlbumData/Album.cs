namespace LinqExploration.AlbumData
{
    internal class Album
    {
        public string AlbumId { get; set; }
        public string Title { get; set; }
        public string[] Artists { get; set; }
        public Track[] Tracks { get; set; }
    }
}
