using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public class VlcMediaMetaChangedEventArgs : EventArgs
    {
        public VlcMediaMetaChangedEventArgs(MediaMetadatas metaType)
        {
            MetaType = metaType;
        }

        public MediaMetadatas MetaType { get; private set; }
    }
}