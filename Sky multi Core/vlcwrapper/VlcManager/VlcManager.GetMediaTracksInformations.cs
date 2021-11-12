using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        [Obsolete("Use GetMediaTracks instead")]
        internal MediaTrackInfosStructure[] GetMediaTracksInformations(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            IntPtr fullBuffer;
            var cpt = VlcNative.libvlc_media_get_tracks_info(mediaInstance, out fullBuffer);
            if (cpt <= 0)
                return new MediaTrackInfosStructure[0];
            var result = new MediaTrackInfosStructure[cpt];
            var buffer = fullBuffer;
            for (int index = 0; index < cpt; index++)
            {
                result[index] = MarshalHelper.PtrToStructure<MediaTrackInfosStructure>(ref buffer);
                buffer = IntPtr.Size == 4
                    ? new IntPtr(buffer.ToInt32() + MarshalHelper.SizeOf<MediaTrackInfosStructure>())
                    : new IntPtr(buffer.ToInt64() + MarshalHelper.SizeOf<MediaTrackInfosStructure>());
            }
            Free(fullBuffer);
            return result;
        }
    }
}
