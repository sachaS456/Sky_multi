﻿using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal float GetFramesPerSecond(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetFramesPerSecond>().Invoke(mediaPlayerInstance);
        }
    }
}
