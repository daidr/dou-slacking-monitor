namespace DouSlackingMonitor.Models.MediaObserver
{
    public class MediaObserverEventArgs
    {
        private string _thumbnail;
        private string _title;
        private string _artist;

        public string Thumbnail { get { return _thumbnail; } }
        public string Title { get { return _title; } }
        public string Artist { get { return _artist; } }
        public MediaObserverEventArgs(string thumbnail, string title, string artist)
        {
            _thumbnail = thumbnail;
            _title = title;
            _artist = artist;
        }
    }
}