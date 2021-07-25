using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void DetachEvent(VlcEventManagerInstance eventManagerInstance, EventTypes eventType, EventCallback callback)
        {
            if (eventManagerInstance == IntPtr.Zero)
                throw new ArgumentException("Event manager is not initialized.");
            if (callback == null)
                return;
            myLibraryLoader.GetInteropDelegate<DetachEvent>().Invoke(eventManagerInstance, eventType, callback, IntPtr.Zero);
        }
    }
}
