using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    internal sealed class VlcInstance : InteropObjectInstance
    {
        private readonly VlcManager myManager;

        internal VlcInstance(VlcManager manager, IntPtr pointer) : base(ref pointer)
        {
            myManager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            if (Pointer != IntPtr.Zero)
                myManager.ReleaseInstance(this);
            base.Dispose(disposing);            
        }

        public static implicit operator IntPtr(VlcInstance instance)
        {
            if (instance == null)
                return IntPtr.Zero;

            return instance.Pointer;
        }
    }
}