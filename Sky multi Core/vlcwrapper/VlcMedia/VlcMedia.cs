using System;
using System.IO;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcMedia : IDisposable
    {
        private readonly VlcManager myVlcMediaPlayer;
        internal readonly string[] optionsAdded;

        internal VlcMedia(VlcManager player, ref FileInfo file, params string[] options)
            : this(player, player.CreateNewMediaFromPath(file.FullName).AddOptionToMedia(player, options))
        {
            optionsAdded = options;
        }

        internal VlcMedia(VlcManager player, ref Uri uri, params string[] options)
            : this(player, player.CreateNewMediaFromLocation(uri.AbsoluteUri).AddOptionToMedia(player, options))
        {
            optionsAdded = options;
        }
        
        internal VlcMedia(VlcManager player, ref string mrl, params string[] options)
            : this(player, player.CreateNewMediaFromLocation(ref mrl).AddOptionToMedia(player, options))
        {
            optionsAdded = options;
        }

        internal VlcMedia(VlcManager player, ref Stream stream, params string[] options)
            : this(player, player.CreateNewMediaFromStream(ref stream).AddOptionToMedia(player, options))
        {
            optionsAdded = options;
        }

        internal VlcMedia(VlcManager player, VlcMediaInstance mediaInstance)
        {
            MediaInstance = mediaInstance;
            myVlcMediaPlayer = player;
        }

        internal void Initialize()
        {
            RegisterEvents();
        }

        internal VlcMediaInstance MediaInstance { get; private set; }

        public string Mrl
        {
            get { return myVlcMediaPlayer.GetMediaMrl(MediaInstance); }
        }

        public MediaStates State
        {
            get { return myVlcMediaPlayer.GetMediaState(MediaInstance); }
        }

        public TimeSpan Duration
        {
            get { return TimeSpan.FromMilliseconds(myVlcMediaPlayer.GetMediaDuration(MediaInstance)); }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && MediaInstance != IntPtr.Zero)
            {
                UnregisterEvents();
                MediaInstance.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        ~VlcMedia()
        {
            Dispose(false);
        }

        public VlcMedia Clone()
        {
            var cloned = myVlcMediaPlayer.CloneMedia(MediaInstance);
            if (cloned != IntPtr.Zero)
                return new VlcMedia(myVlcMediaPlayer, cloned);
            return null;
        }

        public MediaStatsStructure Statistics
        {
            get { return myVlcMediaPlayer.GetMediaStats(MediaInstance); }
        }

        public MediaTrack[] Tracks
        {
            get { return myVlcMediaPlayer.GetMediaTracks(MediaInstance); }
        }

        [Obsolete("Use Tracks instead")]
        public MediaTrackInfosStructure[] TracksInformations
        {
            get { return myVlcMediaPlayer.GetMediaTracksInformations(MediaInstance); }
        }

        private void RegisterEvents()
        {
            var eventManager = myVlcMediaPlayer.GetMediaEventManager(MediaInstance);
            //myVlcMediaPlayer.Manager.AttachEvent(eventManager, EventTypes.MediaDurationChanged, myOnMediaDurationChangedInternalEventCallback = OnMediaDurationChangedInternal);
            myVlcMediaPlayer.AttachEvent(eventManager, EventTypes.MediaFreed, myOnMediaFreedInternalEventCallback = OnMediaFreedInternal);
            myVlcMediaPlayer.AttachEvent(eventManager, EventTypes.MediaMetaChanged, myOnMediaMetaChangedInternalEventCallback = OnMediaMetaChangedInternal);
            myVlcMediaPlayer.AttachEvent(eventManager, EventTypes.MediaParsedChanged, myOnMediaParsedChangedInternalEventCallback = OnMediaParsedChangedInternal);
            myVlcMediaPlayer.AttachEvent(eventManager, EventTypes.MediaStateChanged, myOnMediaStateChangedInternalEventCallback = OnMediaStateChangedInternal);
            myVlcMediaPlayer.AttachEvent(eventManager, EventTypes.MediaSubItemAdded, myOnMediaSubItemAddedInternalEventCallback = OnMediaSubItemAddedInternal);
            myVlcMediaPlayer.AttachEvent(eventManager, EventTypes.MediaSubItemTreeAdded, myOnMediaSubItemTreeAddedInternalEventCallback = OnMediaSubItemTreeAddedInternal);
            eventManager.Dispose();
        }

        private void UnregisterEvents()
        {
            var eventManager = myVlcMediaPlayer.GetMediaEventManager(MediaInstance);
            //myVlcMediaPlayer.Manager.DetachEvent(eventManager, EventTypes.MediaDurationChanged, myOnMediaDurationChangedInternalEventCallback);
            myVlcMediaPlayer.DetachEvent(eventManager, EventTypes.MediaFreed, myOnMediaFreedInternalEventCallback);
            myVlcMediaPlayer.DetachEvent(eventManager, EventTypes.MediaMetaChanged, myOnMediaMetaChangedInternalEventCallback);
            myVlcMediaPlayer.DetachEvent(eventManager, EventTypes.MediaParsedChanged, myOnMediaParsedChangedInternalEventCallback);
            myVlcMediaPlayer.DetachEvent(eventManager, EventTypes.MediaStateChanged, myOnMediaStateChangedInternalEventCallback);
            myVlcMediaPlayer.DetachEvent(eventManager, EventTypes.MediaSubItemAdded, myOnMediaSubItemAddedInternalEventCallback);
            myVlcMediaPlayer.DetachEvent(eventManager, EventTypes.MediaSubItemTreeAdded, myOnMediaSubItemTreeAddedInternalEventCallback);
            eventManager.Dispose();
        }
    }
}