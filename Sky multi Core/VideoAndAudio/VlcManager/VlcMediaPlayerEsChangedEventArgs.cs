using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public class VlcMediaPlayerEsChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerEsChangedEventArgs(MediaTrackTypes trackType, int id)
        {
            this.TrackType = trackType;
            this.Id = id;
        }

        public MediaTrackTypes TrackType;
        public int Id;
    }
}