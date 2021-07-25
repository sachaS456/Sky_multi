using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    internal abstract class VlcEventManagerInstance : InteropObjectInstance
    {
        internal VlcEventManagerInstance(ref IntPtr pointer) : base(ref pointer)
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public static implicit operator IntPtr(VlcEventManagerInstance instance)
        {
            return instance.Pointer;
        }
    }
}