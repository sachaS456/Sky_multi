using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        /// <summary>
        /// Get codec description from media elementary stream.
        ///  LibVLC 3.0.0 and later.
        /// </summary>
        /// <param name="type">The media track type</param>
        /// <param name="codec">The codec 4CC</param>
        /// <returns>The codec description</returns>
        public string GetCodecDescription(MediaTrackTypes type, UInt32 codec)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(GetCodecDescription)}");
            }
            
            var ptr = VlcNative.libvlc_media_get_codec_description(type, codec);
            return Utf8InteropStringConverter.Utf8InteropToString(ptr);
        }
    }
}