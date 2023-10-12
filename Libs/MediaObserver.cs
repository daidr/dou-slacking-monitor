using DouSlackingMonitor.Events;
using DouSlackingMonitor.Interfaces;
using DouSlackingMonitor.Models.MediaObserver;
using System;
using WindowsMediaController;
using Windows.Media.Control;
using Windows.Storage.Streams;
using static WindowsMediaController.MediaManager;
using SkiaSharp;
using Pastel;
using System.Drawing;

namespace DouSlackingMonitor.Libs
{
    public class MediaObserver : IMediaObserver
    {
        public event MediaObserverEventHandler OnMediaUpdate;
        public event MediaObserverTimeEventHandler OnTimeUpdate; 
        public event MediaObserverPlaybackEventHandler OnPlaybackUpdate;
        public MediaManager mediaManager;

        public MediaSession currentSession = null;
        long LastTimelineUpdateTimestamp = 0;

        public MediaObserver()
        {
            // create media manager
            mediaManager = new MediaManager();
            mediaManager.OnFocusedSessionChanged += MediaManager_OnFocusedSessionChanged;
            mediaManager.OnAnySessionClosed += MediaManager_OnAnySessionClosed;
            mediaManager.OnAnySessionOpened += MediaManager_OnAnySessionOpened;

            mediaManager.OnAnyMediaPropertyChanged += CurrentSession_OnMediaPropertyChanged;
            mediaManager.OnAnyPlaybackStateChanged += CurrentSession_OnPlaybackStateChanged;
            mediaManager.OnAnyTimelinePropertyChanged += CurrentSession_OnTimelinePropertyChanged;
        }

        private string currentSongTitle = "";

        private void MediaManager_OnAnySessionOpened(MediaSession session)
        {
            Console.WriteLine(("[Media Observer] Session opened: " + session.ControlSession.SourceAppUserModelId).Pastel(Color.DarkGray));
            if(currentSession == null)
            {
                MediaManager_OnFocusedSessionChanged(session);
                return;
            }
        }

        private void MediaManager_OnAnySessionClosed(MediaSession session)
        {
            Console.WriteLine(("[Media Observer] Session closed: " + session.ControlSession.SourceAppUserModelId).Pastel(Color.DarkGray));
            if(currentSession == null)
            {
                return;
            }
            if (currentSession.Id == session.Id)
            {
                MediaManager_OnFocusedSessionChanged(null);
            }
        }

        private void MediaManager_OnFocusedSessionChanged(MediaSession session)
        {

            if (session == null)
            {
                Console.WriteLine(("[Media Observer] Focused session changed: null").Pastel(Color.DarkGray));
                currentSession = null;
                OnMediaUpdate?.Invoke(new MediaObserverEventArgs("", "", ""));
                OnPlaybackUpdate?.Invoke(new MediaObserverPlaybackEventArgs(false));
                OnTimeUpdate?.Invoke(new MediaObserverTimeEventArgs("", ""));
                return;
            }

            Console.WriteLine(("[Media Observer] Focused session changed: " + session.ControlSession.SourceAppUserModelId).Pastel(Color.DarkGray));
            currentSession = session;
            // call event
            var songInfo = session.ControlSession.TryGetMediaPropertiesAsync().GetAwaiter().GetResult();
            var mediaProp = session.ControlSession.GetPlaybackInfo();
            if (songInfo == null)
            {
                CurrentSession_OnMediaPropertyChanged(session, songInfo);
                return;
            }
            if (mediaProp == null)
            {
                CurrentSession_OnPlaybackStateChanged(session, mediaProp);
                return;
            }
            var timelineInfo = session.ControlSession.GetTimelineProperties();
            if (timelineInfo == null)
            {
                CurrentSession_OnTimelinePropertyChanged(session, timelineInfo);
                return;
            }
        }

        private void CurrentSession_OnPlaybackStateChanged(MediaSession mediaSession, GlobalSystemMediaTransportControlsSessionPlaybackInfo playbackInfo = null)
        {
            if(currentSession == null)
            {
                return;
            }
            if(mediaSession.Id != currentSession.Id)
            {
                return;
            }

            var isPlaying = playbackInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;
            OnPlaybackUpdate?.Invoke(new MediaObserverPlaybackEventArgs(isPlaying));
        }

        private void CurrentSession_OnMediaPropertyChanged(MediaSession mediaSession, GlobalSystemMediaTransportControlsSessionMediaProperties mediaProperties)
        {
            if (currentSession == null)
            {
                return;
            }
            if (mediaSession.Id != currentSession.Id)
            {
                return;
            }
            var songInfo = mediaProperties;
            if (songInfo == null) {
                OnMediaUpdate?.Invoke(new MediaObserverEventArgs("", "", ""));
                return;
            }
            var thumbnail = GetThumbnailBase64(songInfo.Thumbnail);
            var title = songInfo.Title;
            var AlbumTitle = songInfo.AlbumTitle;
            var artist = songInfo.Artist;
            currentSongTitle = title;

            if (artist == null || artist == "")
            {
                artist = songInfo.AlbumArtist;
            }
            OnMediaUpdate?.Invoke(new MediaObserverEventArgs(thumbnail, title, artist));
        }

        private void CurrentSession_OnTimelinePropertyChanged(MediaSession mediaSession, GlobalSystemMediaTransportControlsSessionTimelineProperties timelineProperties)
        {
            if (currentSession == null)
            {
                return;
            }
            if (mediaSession.Id != currentSession.Id)
            {
                return;
            }
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - LastTimelineUpdateTimestamp < 2000)
            {
                return;
            }
            LastTimelineUpdateTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var totalTime = timelineProperties.EndTime.ToString(@"mm\:ss");
            var currentTime = timelineProperties.Position.ToString(@"mm\:ss");
            OnTimeUpdate?.Invoke(new MediaObserverTimeEventArgs(totalTime, currentTime));
        }



        void IMediaObserver.Start()
        {
            mediaManager.Start();
            var session = mediaManager.GetFocusedSession();
            if (session != null)
            {
                MediaManager_OnFocusedSessionChanged(session);
            }
        }

        private string GetThumbnailBase64(IRandomAccessStreamReference stream)
        {
            if (stream == null)
                return "";

            var imageStream = stream.OpenReadAsync().GetAwaiter().GetResult();
            byte[] fileBytes = new byte[imageStream.Size];
            using (DataReader reader = new DataReader(imageStream))
            {
                reader.LoadAsync((uint)imageStream.Size).GetAwaiter().GetResult();
                reader.ReadBytes(fileBytes);
            }

            // 转换成 64*64 的 webp
            // TODO: 配置文件
            var image = SKBitmap.FromImage(SKImage.FromEncodedData(fileBytes));
            var resizedImage = image.Resize(new SKImageInfo(64, 64), SKFilterQuality.Low);
            fileBytes = resizedImage.Encode(SKEncodedImageFormat.Webp, 70).ToArray();
            return "data:image/webp;base64," + Convert.ToBase64String(fileBytes);
        }
    }
}
