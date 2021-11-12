using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.VlcWrapper.Core
{
    public enum VlcLogLevel : byte
    {
        Debug = 0,
        Notice = 2,
        Warning = 3,
        Error = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ModuleDescriptionStructure
    {
        public IntPtr Name;
        public IntPtr ShortName;
        public IntPtr LongName;
        public IntPtr Help;
        public IntPtr NextModule;
    }

    public delegate void EventCallback(IntPtr args);

    public enum EventTypes
      : int
    {
        MediaMetaChanged = 0,
        MediaSubItemAdded,
        MediaDurationChanged,
        MediaParsedChanged,
        MediaFreed,
        MediaStateChanged,
        MediaSubItemTreeAdded,

        MediaPlayerMediaChanged = 0x100,
        MediaPlayerNothingSpecial,
        MediaPlayerOpening,
        MediaPlayerBuffering,
        MediaPlayerPlaying,
        MediaPlayerPaused,
        MediaPlayerStopped,
        MediaPlayerForward,
        MediaPlayerBackward,
        MediaPlayerEndReached,
        MediaPlayerEncounteredError,
        MediaPlayerTimeChanged,
        MediaPlayerPositionChanged,
        MediaPlayerSeekableChanged,
        MediaPlayerPausableChanged,
        MediaPlayerTitleChanged,
        MediaPlayerSnapshotTaken,
        MediaPlayerLengthChanged,
        MediaPlayerVout,
        MediaPlayerScrambledChanged,
        MediaPlayerEsAdded,
        MediaPlayerEsDeleted,
        MediaPlayerEsSelected,
        MediaPlayerCorked,
        MediaPlayerUncorked,
        MediaPlayerMuted,
        MediaPlayerUnmuted,
        MediaPlayerAudioVolume,
        MediaPlayerAudioDevice,
        MediaPlayerChapterChanged,

        MediaListItemAdded = 0x200,
        MediaListWillAddItem,
        MediaListItemDeleted,
        MediaListWillDeleteItem,
        MediaListEndReached,

        MediaListViewItemAdded = 0x300,
        MediaListViewWillAddItem,
        MediaListViewItemDeleted,
        MediaListViewWillDeleteItem,

        MediaListPlayerPlayed = 0x400,
        MediaListPlayerNextItemSet,
        MediaListPlayerStopped,

        [Obsolete("Useless event, it will be triggered only when calling libvlc_media_discoverer_start()")]
        MediaDiscovererStarted = 0x500,
        [Obsolete("Useless event, it will be triggered only when calling libvlc_media_discoverer_stop()")]
        MediaDiscovererEnded,
        RendererDiscovererItemAdded,
        RendererDiscovererItemDeleted,

        VlmMediaAdded = 0x600,
        VlmMediaRemoved,
        VlmMediaChanged,
        VlmMediaInstanceStarted,
        VlmMediaInstanceStopped,
        VlmMediaInstanceStatusInit,
        VlmMediaInstanceStatusOpening,
        VlmMediaInstanceStatusPlaying,
        VlmMediaInstanceStatusPause,
        VlmMediaInstanceStatusEnd,
        VlmMediaInstanceStatusError
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VlcEventArg
    {
        public EventTypes type;

        public IntPtr p_obj;

        public VlcEventArgUnion eventArgsUnion;

        [StructLayout(LayoutKind.Explicit)]
        public struct VlcEventArgUnion
        {
            #region Media Descriptor

            [FieldOffset(0)]
            public MediaMetaChanged MediaMetaChanged;

            [FieldOffset(0)]
            public MediaSubItemAdded MediaSubItemAdded;

            [FieldOffset(0)]
            public MediaDurationChanged MediaDurationChanged;

            [FieldOffset(0)]
            public MediaParsedChanged MediaParsedChanged;

            [FieldOffset(0)]
            public MediaFreed MediaFreed;

            [FieldOffset(0)]
            public MediaStateChanged MediaStateChanged;

            [FieldOffset(0)]
            public MediaSubItemTreeAdded MediaSubItemTreeAdded;

            #endregion

            #region Media Instance

            [FieldOffset(0)]
            public MediaPlayerBuffering MediaPlayerBuffering;

            [FieldOffset(0)]
            public MediaPlayerChapterChanged MediaPlayerChapterChanged;

            [FieldOffset(0)]
            public MediaPlayerPositionChanged MediaPlayerPositionChanged;

            [FieldOffset(0)]
            public MediaPlayerTimeChanged MediaPlayerTimeChanged;

            [FieldOffset(0)]
            public MediaPlayerTitleChanged MediaPlayerTitleChanged;

            [FieldOffset(0)]
            public MediaPlayerSeekableChanged MediaPlayerSeekableChanged;

            [FieldOffset(0)]
            public MediaPlayerPausableChanged MediaPlayerPausableChanged;

            [FieldOffset(0)]
            public MediaPlayerScrambledChanged MediaPlayerScrambledChanged;

            [FieldOffset(0)]
            public MediaPlayerVideoOutChanged MediaPlayerVideoOutChanged;

            #endregion

            #region Media List

            [FieldOffset(0)]
            public MediaListItemAdded MediaListItemAdded;

            [FieldOffset(0)]
            public MediaListWillAddItem MediaListWillAddItem;

            [FieldOffset(0)]
            public MediaListItemDeleted MediaListItemDeleted;

            [FieldOffset(0)]
            public MediaListWillDeleteItem MediaListWillDeleteItem;

            #endregion

            [FieldOffset(0)]
            public MediaListPlayerNextItemSet MediaListPlayerNextItemSet;

            [FieldOffset(0)]
            public MediaPlayerSnapshotTaken MediaPlayerSnapshotTaken;

            [FieldOffset(0)]
            public MediaPlayerLengthChanged MediaPlayerLengthChanged;

            [FieldOffset(0)]
            public VlmMediaEvent VlmMediaEvent;

            [FieldOffset(0)]
            public MediaPlayerMediaChanged MediaPlayerMediaChanged;

            [FieldOffset(0)]
            public MediaPlayerEsChanged MediaPlayerEsChanged;

            [FieldOffset(0)]
            public MediaPlayerAudioVolume MediaPlayerAudioVolume;

            [FieldOffset(0)]
            public MediaPlayerAudioDevice MediaPlayerAudioDevice;

            [FieldOffset(0)]
            public RendererDiscovererItemAdded RendererDiscovererItemAdded;

            [FieldOffset(0)]
            public RendererDiscovererItemDeleted RendererDiscovererItemDeleted;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaMetaChanged
    {
        public MediaMetadatas MetaType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaSubItemAdded
    {
        public IntPtr NewChild;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaDurationChanged
    {
        public long NewDuration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaParsedChanged
    {
        public int NewStatus;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaFreed
    {
        public IntPtr MediaInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaStateChanged
    {
        public MediaStates NewState;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaSubItemTreeAdded
    {
        public IntPtr MediaInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerBuffering
    {
        public float NewCache;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerChapterChanged
    {
        public int NewChapter;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerPositionChanged
    {
        public float NewPosition;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerTimeChanged
    {
        public long NewTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerTitleChanged
    {
        public int NewTitle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerSeekableChanged
    {
        public int NewSeekable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerPausableChanged
    {
        public int NewPausable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerScrambledChanged
    {
        public int NewScrambled;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerVideoOutChanged
    {
        public int NewCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaListItemAdded
    {
        public IntPtr MediaInstance;
        public int Index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaListWillAddItem
    {
        public IntPtr MediaInstance;
        public int Index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaListItemDeleted
    {
        public IntPtr MediaInstance;
        public int Index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaListWillDeleteItem
    {
        public IntPtr MediaInstance;
        public int Index;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaListPlayerNextItemSet
    {
        public IntPtr MediaInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerSnapshotTaken
    {
        public IntPtr pszFilename;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerLengthChanged
    {
        public long NewLength;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VlmMediaEvent
    {
        public IntPtr pszMediaName;
        public IntPtr pszInstanceName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerMediaChanged
    {
        public IntPtr MediaInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerEsChanged
    {
        public MediaTrackTypes TrackType;
        public int Id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerAudioVolume
    {
        public float volume;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaPlayerAudioDevice
    {
        public IntPtr pszDevice;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RendererDiscovererItemAdded
    {
        public IntPtr item;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RendererDiscovererItemDeleted
    {
        public IntPtr item;
    }

    /// <summary>
    /// Callback prototype to open a custom bitstream input media.
    /// Note: The same media item can be opened multiple times. Each time, this callback is invoked.
    /// It should allocate and initialize any instance-specific resources, then store them in <paramref name="pData"/>.
    /// The instance resources can be freed in the <see cref="CallbackCloseMediaDelegate"/> callback.
    /// </summary>
    /// <param name="opaque">private pointer as passed to <see cref="CallbackOpenMediaDelegate"/></param>
    /// <param name="pData">storage space for a private data pointer [OUT]</param>
    /// <param name="szData">byte length of the bitstream or UINT64_MAX if unknown [OUT]</param>
    /// <returns>0 on success, non-zero on error. In case of failure, the other callbacks will not be invoked and any value stored in <paramref name="pData"/> and <paramref name="szData"/> is discarded.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int CallbackOpenMediaDelegate(IntPtr opaque, ref IntPtr pData, out ulong szData);

    /// <summary>
    /// Callback prototype to read data from a custom bitstream input media.
    /// Note: If no data is immediately available, then the callback should sleep.
    /// Warning: The application is responsible for avoiding deadlock situations. In particular, the callback should return an error if playback is stopped; if it does not return, then libvlc_media_player_stop() will never return.
    /// </summary>
    /// <param name="opaque">private pointer as set by the <see cref="CallbackOpenMediaDelegate"/> callback</param>
    /// <param name="buf">start address of the buffer to read data into</param>
    /// <param name="len">bytes length of the buffer</param>
    /// <returns>strictly positive number of bytes read, 0 on end-of-stream, or -1 on non-recoverable error</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int CallbackReadMediaDelegate(IntPtr opaque, IntPtr buf, uint len);

    /// <summary>
    /// Callback prototype to seek a custom bitstream input media.
    /// </summary>
    /// <param name="opaque">private pointer as set by the <see cref="CallbackOpenMediaDelegate"/> callback</param>
    /// <param name="offset">absolute byte offset to seek to</param>
    /// <returns>0 on success, -1 on error</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int CallbackSeekMediaDelegate(IntPtr opaque, ulong offset);

    /// <summary>
    /// Callback prototype to close a custom bitstream input media.
    /// </summary>
    /// <param name="opaque">private pointer as set by the <see cref="CallbackOpenMediaDelegate"/> callback</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void CallbackCloseMediaDelegate(IntPtr opaque);

    [StructLayout(LayoutKind.Sequential)]
    public struct MediaStatsStructure
    {
        /* Input */
        public int ReadBytes;
        public float InputBitrate;

        /* Demux */
        public int DemuxReadBytes;
        public float DemuxBitrate;
        public int DemuxCorrupted;
        public int DemuxDiscontinuity;

        /* Decoders */
        public int DecodedVideo;
        public int DecodedAudio;


        /* Video Output */
        public int DisplayedPictures;
        public int LostPictures;

        /* Audio output */
        public int PlayedAudioBuffers;
        public int LostAudioBuffers;

        /* Stream output */
        public int SentPackets;
        public int SentBytes;
        public float SendBitrate;
    }

    [Obsolete("Use GetMediaTracks instead")]
    [StructLayout(LayoutKind.Explicit)]
    public struct MediaTrackInfosStructure
    {
        /// <summary>
        /// Codec Value
        /// </summary>
        [FieldOffset(0)]
        public UInt32 CodecFourcc;

        /// <summary>
        /// Codec Id
        /// </summary>
        [FieldOffset(4)]
        public int Id;

        /// <summary>
        /// Type of Track
        /// </summary>
        [FieldOffset(8)]
        public MediaTrackTypes Type;

        /// <summary>
        /// Codec Profile
        /// </summary>
        [FieldOffset(12)]
        public int Profile;

        /// <summary>
        /// Codec Level
        /// </summary>
        [FieldOffset(16)]
        public int Level;

        /// <summary>
        /// Audio Track Info
        /// </summary>
        [FieldOffset(20)]
        public AudioStructure Audio;

        /// <summary>
        /// Video Track Info
        /// </summary>
        [FieldOffset(20)]
        public VideoStructure Video;

        /// <summary>
        /// Codec Abbreviation
        /// </summary>
        public string CodecName => FourCCConverter.FromFourCC(this.CodecFourcc);
    }

    /// <summary>
    /// Audio information of Media Track
    /// </summary>
    public struct AudioStructure
    {
        /// <summary>
        /// Number of Channels
        /// </summary>
        public uint Channels;

        /// <summary>
        /// Audio Sampling Rate
        /// </summary>
        public uint Rate;
    }

    /// <summary>
    /// Video information of Media Track
    /// </summary>
    public struct VideoStructure
    {
        /// <summary>
        /// Height of Video
        /// </summary>
        public uint Height;

        /// <summary>
        /// Width of Video
        /// </summary>
        public uint Width;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LibvlcMediaTrackT
    {
        public UInt32 Codec;
        public UInt32 OriginalFourCC;
        public int Id;
        public MediaTrackTypes Type;
        public int Profile;
        public int Level;
        public IntPtr TypedTrack;

        public uint Bitrate;
        public IntPtr Language;
        public IntPtr Description;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LibvlcAudioTrackT
    {
        public uint Channels;
        public uint Rate;
    }

    public enum VideoOrientation
    {
        /// <summary>
        /// Normal. Top line represents top, left column left.
        /// </summary>
        TopLeft,

        /// <summary>
        /// Flipped horizontally
        /// </summary>
        TopRight,

        /// <summary>
        /// Flipped vertically
        /// </summary>
        BottomLeft,

        /// <summary>
        /// Rotated 180 degrees
        /// </summary>
        BottomRight,

        /// <summary>
        /// Transposed
        /// </summary>
        LeftTop,

        /// <summary>
        /// Rotated 90 degrees clockwise (or 270 anti-clockwise)
        /// </summary>
        LeftBottom,

        /// <summary>
        /// Rotated 90 degrees anti-clockwise
        /// </summary>
        RightTop,

        /// <summary>
        /// Anti-transposed
        /// </summary>
        RightBottom
    }

    public enum VideoProjection
    {
        Rectangular,
        Equirectangular,

        CubemapLayoutStandard = 0x100
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VideoViewpoint
    {
        /// <summary>
        /// view point yaw in degrees  ]-180;180]
        /// </summary>
        public float Yaw;

        /// <summary>
        /// view point pitch in degrees  ]-90;90]
        /// </summary>
        public float Pitch;

        /// <summary>
        /// view point roll in degrees ]-180;180]
        /// </summary>
        public float Roll;

        /// <summary>
        /// field of view in degrees ]0;180[ (default 80.)
        /// </summary>
        public float FieldOfView;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LibvlcVideoTrackT
    {
        public uint Height;
        public uint Width;

        public uint SarNum;
        public uint SarDen;

        public uint FrameRateNum;
        public uint FrameRateDen;

        public VideoOrientation Orientation;
        public VideoProjection Projection;
        public VideoViewpoint Pose;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LibvlcSubtitleTrackT
    {
        public IntPtr Encoding;
    }

    public enum MediaMetadatas
    {
        Title = 0,
        Artist,
        Genre,
        Copyright,
        Album,
        TrackNumber,
        Description,
        Rating,
        Date,
        Setting,
        URL,
        Language,
        NowPlaying,
        Publisher,
        EncodedBy,
        ArtworkURL,
        TrackID,
    }

    public enum MediaStates
    {
        NothingSpecial = 0,
        Opening,
        Buffering,
        Playing,
        Paused,
        Stopped,
        Ended,
        Error
    }

    public enum MediaTrackTypes : int
    {
        Unknown = -1,
        Audio = 0,
        Video = 1,
        Text = 2
    }

    public enum AudioOutputChannels
    {
        Error = -1,
        Stereo = 1,
        RStereo = 2,
        Left = 3,
        Right = 4,
        Dolbys = 5,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LibvlcAudioOutputDeviceT
    {
        public IntPtr Next;
        public IntPtr DeviceIdentifier;
        public IntPtr Description;
    }

    public enum AudioOutputDeviceTypes
    {
        Error = -1,
        Mono = 1,
        Stereo = 2,
        _2F2R = 4,
        _3F2R = 5,
        _5_1 = 6,
        _6_1 = 7,
        _7_1 = 8,
        SPDIF = 10,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AudioOutputDescriptionStructureInternal
    {
        public IntPtr Name;
        public IntPtr Description;
        public IntPtr NextAudioOutputDescription;
    }

    public struct AudioOutputDescriptionStructure
    {
        public string Name;
        public string Description;
    }

    public enum NavigateModes
    {
        Activate = 0,
        Up,
        Down,
        Left,
        Right
    }

    public enum Position
    {
        Disable = -1,
        Center,
        Left,
        Right,
        Top,
        TopLeft,
        TopRight,
        Bottom,
        BottomLeft,
        BottomRight
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Top;
        public int Left;
        public int Bottom;
        public int Right;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TrackDescriptionStructure
    {
        public int Id;
        public IntPtr Name;
        public IntPtr NextTrackDescription;
    }

    public enum VideoAdjustOptions
    {
        Enable = 0,
        Contrast,
        Brightness,
        Hue,
        Saturation,
        Gamma
    }

    public enum VideoLogoOptions
    {
        Enable = 0,
        File,
        X,
        Y,
        Delay,
        Repeat,
        Opacity,
        Position
    }

    public enum VideoMarqueeOptions
    {
        Enable = 0,
        Text,
        Color,
        Opacity,
        Position,
        Refresh,
        Size,
        Timeout,
        X,
        Y
    }

    /// <summary>
    /// Callback prototype to configure picture buffers format.
    /// This callback gets the format of the video as output by the video decoder
    /// and the chain of video filters(if any). It can opt to change any parameter
    /// as it needs.In that case, LibVLC will attempt to convert the video format
    /// (rescaling and chroma conversion) but these operations can be CPU intensive.
    /// </summary>
    /// <param name="userData">
    /// pointer to the private pointer passed to <see cref="SetVideoCallbacks"/>.
    /// </param>
    /// <param name="chroma">
    /// A pointer to the 4 bytes video format identifier.
    /// </param>
    /// <param name="width">
    /// A reference to the pixel width.
    /// </param>
    /// <param name="height">
    /// A referebce to the pixel height.
    /// </param>
    /// <param name="pitches">
    /// A reference to the table of scanline pitches in bytes for each pixel plane
    /// (the table is allocated by LibVLC).
    /// </param>
    /// <param name="lines">
    /// A reference to the table of scanlines count for each plane.
    /// </param>
    /// <returns>
    /// The number of picture buffers allocated, 0 indicates failure.
    /// </returns>
    /// <remarks>
    /// For each pixels plane, the scanline pitch must be bigger than or equal to
    /// the number of bytes per pixel multiplied by the pixel width.
    /// Similarly, the number of scanlines must be bigger than of equal to
    /// the pixel height.
    /// Furthermore, we recommend that pitches and lines be multiple of 32
    /// to not break assumption that might be made by various optimizations
    /// in the video decoders, video filters and/or video converters.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint VideoFormatCallback(out IntPtr userData, IntPtr chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines);

    /// <summary>
    /// Callback prototype to configure picture buffers format.
    /// </summary>
    /// <param name="userData">
    /// A private pointer as passed to <see cref="SetVideoCallbacks"/>
    /// (and possibly modified by <see cref="VideoFormatCallback"/>
    /// </param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CleanupVideoCallback(ref IntPtr userData);

    /// <summary>
    /// Callback prototype to unlock a picture buffer.
    /// When the video frame decoding is complete, the unlock callback is invoked.
    /// This callback might not be needed at all.It is only an indication that the
    /// application can now read the pixel values if it needs to.
    /// </summary>
    /// <remarks>
    /// A picture buffer is unlocked after the picture is decoded,
    /// but before the picture is displayed.
    /// </remarks>
    /// <param name="userData">
    /// private pointer as passed to <see cref="SetVideoCallbacks"/>.
    /// </param>
    /// <param name="picture">
    /// private pointer returned from the <see cref="LockVideoCallback"/>
    /// callback
    /// </param>
    /// <param name="planes">
    /// pixel planes as defined by the <see cref="LockVideoCallback"/>
    /// callback (this parameter is only for convenience)
    /// 
    /// Its size is 
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UnlockVideoCallback(IntPtr userData, IntPtr picture, [MarshalAs(UnmanagedType.LPArray, SizeConst = 5)] IntPtr[] planes);

    /// <summary>
    /// Callback prototype to allocate and lock a picture buffer.
    /// </summary>
    /// <param name="userData">
    /// Private pointer as passed to <see cref="SetVideoCallbacks"/>.
    /// </param>
    /// <param name="planes">
    /// The pointer to the array of pointers to the pixel planes (LibVLC allocates the array
    /// of pointers, this callback must initialize the array)
    /// If you only need the first plane, then you have to initialize the address pointed by planes
    /// </param>
    /// <remarks>
    /// Whenever a new video frame needs to be decoded, the lock callback is
    /// invoked.Depending on the video chroma, one or three pixel planes of
    /// adequate dimensions must be returned via the second parameter.Those
    /// planes must be aligned on 32-bytes boundaries.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr LockVideoCallback(IntPtr userData, IntPtr planes);

    /// <summary>
    /// Callback prototype to display a picture.
    /// When the video frame needs to be shown, as determined by the media playback
    /// clock, the display callback is invoked.
    /// </summary>
    /// <param name="userData">
    /// Private pointer as passed to <see cref="SetVideoCallbacks"/>
    /// </param>
    /// <param name="picture">
    /// Private pointer returned from <see cref="LockVideoCallback"/>.
    /// </param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DisplayVideoCallback(IntPtr userData, IntPtr picture);

    /// <summary>
    /// The delegate type that represent logging functions
    /// </summary>
    /// <param name="data">The data given to libvlc_log_set. In our case, this value will always be <see langword="null" /></param>
    /// <param name="level">The log level</param>
    /// <param name="logContext">The address of the structure that contains information about the log event. <see cref="GetLogContext"/></param>
    /// <param name="format">The format of the log messages</param>
    /// <param name="args">The va_list of printf arguments.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogCallback(IntPtr data, VlcLogLevel level, IntPtr logContext, [MarshalAs(UnmanagedType.LPStr)] string format, IntPtr args);
}
