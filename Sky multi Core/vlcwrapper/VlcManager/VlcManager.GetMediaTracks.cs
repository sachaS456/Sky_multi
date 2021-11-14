/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.InteropServices;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal MediaTrack[] GetMediaTracks(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");

            var cpt = VlcNative.libvlc_media_tracks_get(mediaInstance, out var fullBuffer);
            if (cpt <= 0)
                return new MediaTrack[0];
            try
            {
                var result = new MediaTrack[cpt];
                for (int index = 0; index < cpt; index++)
                {
                    var current = MarshalHelper.PtrToStructure<LibvlcMediaTrackT>(Marshal.ReadIntPtr(fullBuffer, index * MarshalHelper.SizeOf<IntPtr>()));

                    TrackInfo trackInfo = null;

                    switch (current.Type)
                    {
                        case MediaTrackTypes.Audio:
                            var audio = MarshalHelper.PtrToStructure<LibvlcAudioTrackT>(ref current.TypedTrack);
                            trackInfo = new AudioTrack
                            {
                                Channels = audio.Channels,
                                Rate = audio.Rate
                            };
                            break;
                        case MediaTrackTypes.Video:
                            var video = MarshalHelper.PtrToStructure<LibvlcVideoTrackT>(ref current.TypedTrack);
                            trackInfo = new VideoTrack
                            {
                                Height = video.Height,
                                Width = video.Width,
                                SarNum = video.SarNum,
                                SarDen = video.SarDen,
                                FrameRateNum = video.FrameRateNum,
                                FrameRateDen = video.FrameRateDen,
                                Orientation = video.Orientation,
                                Projection = video.Projection,
                                Pose = video.Pose
                            };
                            break;
                        case MediaTrackTypes.Text:
                            var text = MarshalHelper.PtrToStructure<LibvlcSubtitleTrackT>(ref current.TypedTrack);
                            trackInfo = new SubtitleTrack
                            {
                                Encoding = Utf8InteropStringConverter.Utf8InteropToString(text.Encoding)
                            };
                            break;
                    }

                    result[index] = new MediaTrack
                    {
                        CodecFourcc = current.Codec,
                        OriginalFourcc = current.OriginalFourCC,
                        Id = current.Id,
                        Type = current.Type,
                        Profile = current.Profile,
                        Level = current.Level,
                        TrackInfo = trackInfo,
                        Bitrate = current.Bitrate,
                        Language = Utf8InteropStringConverter.Utf8InteropToString(current.Language),
                        Description = Utf8InteropStringConverter.Utf8InteropToString(current.Description)
                    };
                }
                return result;
            }
            finally
            {
                VlcNative.libvlc_media_tracks_release(fullBuffer, cpt);
            }
        }
    }
}
