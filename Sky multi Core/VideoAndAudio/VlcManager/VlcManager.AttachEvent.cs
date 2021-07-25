using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal int AttachEvent(VlcEventManagerInstance eventManagerInstance, EventTypes eventType, EventCallback callback)
        {
            if (eventManagerInstance == IntPtr.Zero)
                throw new ArgumentException("Event manager instance is not initialized.");
            if (callback == null)
                throw new ArgumentException("Callback for event is not initialized.");
            return myLibraryLoader.GetInteropDelegate<AttachEvent>().Invoke(eventManagerInstance, eventType, callback, IntPtr.Zero);
        }
    }
}
