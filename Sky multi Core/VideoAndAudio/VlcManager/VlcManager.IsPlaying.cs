using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal bool IsPlaying(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero) return false;
            //This seems to be called multiple time
            //Eventually throwing an uncaught exception on close
            //An unhandled exception of type 'System.ArgumentException' occurred in Vlc.DotNet.Core.Interops.dll
            //Additional information: Media player instance is not initialized.
            //if (mediaPlayerInstance == IntPtr.Zero)
            //    throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<IsPlaying>().Invoke(mediaPlayerInstance) == 1;
        }
    }
}
