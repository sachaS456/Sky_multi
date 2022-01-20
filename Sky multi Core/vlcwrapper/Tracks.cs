/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public class MediaTrack
    {
        public UInt32 CodecFourcc { get; set; }
        public UInt32 OriginalFourcc { get; set; }
        public int Id { get; set; }
        public MediaTrackTypes Type { get; set; }
        public int Profile { get; set; }
        public int Level { get; set; }
        public TrackInfo TrackInfo { get; set; }

        public uint Bitrate { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
    }

    public abstract class TrackInfo
    {
    }

    public class AudioTrack : TrackInfo
    {
        public uint Channels { get; set; }
        public uint Rate { get; set; }
    }

    public class VideoTrack : TrackInfo
    {
        public uint Height { get; set; }
        public uint Width { get; set; }

        public uint SarNum { get; set; }
        public uint SarDen { get; set; }

        public uint FrameRateNum { get; set; }
        public uint FrameRateDen { get; set; }

        public VideoOrientation Orientation { get; set; }
        public VideoProjection Projection { get; set; }
        public VideoViewpoint Pose { get; set; }
    }

    public class SubtitleTrack : TrackInfo
    {
        public string Encoding { get; set; }
    }
}