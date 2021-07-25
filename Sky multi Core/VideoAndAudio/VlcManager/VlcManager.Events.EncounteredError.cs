﻿using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerEncounteredErrorInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEncounteredErrorEventArgs> EncounteredError;

        private void OnMediaPlayerEncounteredErrorInternal(IntPtr ptr)
        {
            OnMediaPlayerEncounteredError();
        }

        public void OnMediaPlayerEncounteredError()
        {
            var del = EncounteredError;
            if (del != null)
                del(this, new VlcMediaPlayerEncounteredErrorEventArgs());
        }
    }
}