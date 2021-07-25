using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
	public sealed partial class VlcManager
    {
        internal void AddOptionToMedia(VlcMediaInstance mediaInstance, string option)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            if (string.IsNullOrEmpty(option))
                return;

            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(option))
            {
                myLibraryLoader.GetInteropDelegate<AddOptionToMedia>().Invoke(mediaInstance, handle);
            }
        }

        internal void AddOptionToMedia(VlcMediaInstance mediaInstance, string[] options)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            options = options ?? new string[0];
            foreach (var option in options)
            {
                AddOptionToMedia(mediaInstance, option);
            }
        }
    }
}
