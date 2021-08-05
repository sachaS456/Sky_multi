﻿using System;

namespace Sky_multi_Core
{
    public sealed class VlcMediaPlayerSnapshotTakenEventArgs : EventArgs
    {
        public VlcMediaPlayerSnapshotTakenEventArgs(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }
    }
}