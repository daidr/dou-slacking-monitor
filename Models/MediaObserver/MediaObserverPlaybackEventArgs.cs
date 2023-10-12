namespace DouSlackingMonitor.Models.MediaObserver
{
    public class MediaObserverPlaybackEventArgs
    {
        private bool _isPlaying;

        public bool isPlaying { get { return _isPlaying; } }
        public MediaObserverPlaybackEventArgs(bool isPlaying)
        {
            _isPlaying = isPlaying;
        }
    }
}