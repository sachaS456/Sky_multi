using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal sealed class VlcMediaPlayerInstance : InteropObjectInstance
    {
        private readonly VlcManager myManager;

        internal VlcMediaPlayerInstance(VlcManager manager, IntPtr pointer) : base(ref pointer)
        {
            myManager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            if (Pointer != IntPtr.Zero)
                myManager.ReleaseMediaPlayer(this);
            base.Dispose(disposing);
        }

        public static implicit operator IntPtr(VlcMediaPlayerInstance instance)
        {
            return instance != null
                ? instance.Pointer
                : IntPtr.Zero;
        }
    }
}