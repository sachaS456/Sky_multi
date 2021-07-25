﻿using System;

namespace Sky_multi_Core
{
    public sealed class VlcMediaPlayerVideoOutChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerVideoOutChangedEventArgs(int newCount)
        {
            NewCount = newCount;
        }

        public int NewCount { get; private set; }
    }
}