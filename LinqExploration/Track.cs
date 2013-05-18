namespace LinqExploration
{
    internal class Track
    {
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public string Length { get; set; }

        public int LengthInSeconds
        {
            get
            {
                var parts = Length.Split(':');
                var minutes = int.Parse(parts[0].Trim());
                var seconds = int.Parse(parts[1].Trim());
                return minutes * 60 + seconds;
            }
        }
    }
}
