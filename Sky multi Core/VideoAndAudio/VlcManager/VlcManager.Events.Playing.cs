﻿using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerPlayingInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPlayingEventArgs> Playing;

        private void OnMediaPlayerPlayingInternal(IntPtr ptr)
        {
            OnMediaPlayerPlaying();
        }

        public void OnMediaPlayerPlaying()
        {
            var del = Playing;
            if (del != null)
                del(this, new VlcMediaPlayerPlayingEventArgs());
        }
    }
}