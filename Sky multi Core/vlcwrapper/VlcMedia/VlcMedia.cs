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
using System.IO;
using Sky_multi_Core.VlcWrapper.Core;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcMedia : IDisposable
    {
        internal readonly string[] optionsAdded;
        private readonly VlcInstance VlcInstance;

        internal VlcMedia(in VlcInstance vlcInstance, in FileInfo file, params string[] options)
            : this(CreateNewMediaFromPath(vlcInstance, file.FullName).AddOptionToMedia(options), vlcInstance)
        {
            optionsAdded = options;
        }

        internal VlcMedia(in VlcInstance vlcInstance, in Uri uri, params string[] options)
            : this(CreateNewMediaFromLocation(vlcInstance, uri.AbsoluteUri).AddOptionToMedia(options), vlcInstance)
        {
            optionsAdded = options;
        }
        
        internal VlcMedia(in VlcInstance vlcInstance, in string mrl, params string[] options)
            : this(CreateNewMediaFromLocation(vlcInstance, mrl).AddOptionToMedia(options), vlcInstance)
        {
            optionsAdded = options;
        }

        internal VlcMedia(in VlcInstance vlcInstance, in Stream stream, params string[] options)
            : this(CreateNewMediaFromStream(vlcInstance, stream).AddOptionToMedia(options), vlcInstance)
        {
            optionsAdded = options;
        }

        internal VlcMedia(in VlcMediaInstance mediaInstance, in VlcInstance vlcInstance)
        {
            MediaInstance = mediaInstance;
            VlcInstance = vlcInstance;
        }

        internal void Initialize()
        {
            RegisterEvents();
        }

        internal VlcMediaInstance MediaInstance { get; private set; }

        private void MediaInstanceIsLoad()
        {
            if (MediaInstance == IntPtr.Zero)
            {
                throw new ArgumentException("Media instance is not initialized.");
            }
        }

        public string Mrl
        {
            get 
            {
                MediaInstanceIsLoad();
                return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_media_get_mrl(MediaInstance));
            }
        }

        public MediaStates State
        {
            get 
            {
                MediaInstanceIsLoad();
                return VlcNative.libvlc_media_get_state(MediaInstance); 
            }
        }

        public TimeSpan Duration
        {
            get 
            {
                MediaInstanceIsLoad();
                return TimeSpan.FromMilliseconds(VlcNative.libvlc_media_get_duration(MediaInstance)); 
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && MediaInstance != IntPtr.Zero)
            {
                UnregisterEvents();
                MediaInstance.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        ~VlcMedia()
        {
            Dispose(false);
        }

        public VlcMedia Clone()
        {
            MediaInstanceIsLoad();
            VlcMediaInstance cloned = VlcMediaInstance.New(VlcNative.libvlc_media_duplicate(MediaInstance));

            if (cloned != IntPtr.Zero)
            {
                return new VlcMedia(cloned, VlcInstance);
            }

            return null;
        }

        public MediaStatsStructure Statistics
        {
            get 
            {
                MediaInstanceIsLoad();
                MediaStatsStructure result;
                VlcNative.libvlc_media_get_stats(MediaInstance, out result);
                return result; 
            }
        }

        public MediaTrack[] Tracks
        {
            get 
            {
                MediaInstanceIsLoad();

                uint cpt = VlcNative.libvlc_media_tracks_get(MediaInstance, out IntPtr fullBuffer);
                if (cpt <= 0)
                    return new MediaTrack[0];
                try
                {
                    MediaTrack[] result = new MediaTrack[cpt];
                    for (int index = 0; index < cpt; index++)
                    {
                        LibvlcMediaTrackT current = MarshalHelper.PtrToStructure<LibvlcMediaTrackT>(Marshal.ReadIntPtr(fullBuffer, index * MarshalHelper.SizeOf<IntPtr>()));

                        TrackInfo trackInfo = null;

                        switch (current.Type)
                        {
                            case MediaTrackTypes.Audio:
                                LibvlcAudioTrackT audio = MarshalHelper.PtrToStructure<LibvlcAudioTrackT>(in current.TypedTrack);
                                trackInfo = new AudioTrack
                                {
                                    Channels = audio.Channels,
                                    Rate = audio.Rate
                                };
                                break;
                            case MediaTrackTypes.Video:
                                LibvlcVideoTrackT video = MarshalHelper.PtrToStructure<LibvlcVideoTrackT>(in current.TypedTrack);
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
                                LibvlcSubtitleTrackT text = MarshalHelper.PtrToStructure<LibvlcSubtitleTrackT>(in current.TypedTrack);
                                trackInfo = new SubtitleTrack
                                {
                                    Encoding = Utf8InteropStringConverter.Utf8InteropToString(in text.Encoding)
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
                            Language = Utf8InteropStringConverter.Utf8InteropToString(in current.Language),
                            Description = Utf8InteropStringConverter.Utf8InteropToString(in current.Description)
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

        private void RegisterEvents()
        {
            MediaInstanceIsLoad();
            VlcMediaEventManagerInstance eventManager = new VlcMediaEventManagerInstance(VlcNative.libvlc_media_event_manager(MediaInstance));
            //myVlcMediaPlayer.Manager.AttachEvent(eventManager, EventTypes.MediaDurationChanged, myOnMediaDurationChangedInternalEventCallback = OnMediaDurationChangedInternal);
            AttachEvent(eventManager, EventTypes.MediaFreed, myOnMediaFreedInternalEventCallback = OnMediaFreedInternal);
            AttachEvent(eventManager, EventTypes.MediaMetaChanged, myOnMediaMetaChangedInternalEventCallback = OnMediaMetaChangedInternal);
            AttachEvent(eventManager, EventTypes.MediaParsedChanged, myOnMediaParsedChangedInternalEventCallback = OnMediaParsedChangedInternal);
            AttachEvent(eventManager, EventTypes.MediaStateChanged, myOnMediaStateChangedInternalEventCallback = OnMediaStateChangedInternal);
            AttachEvent(eventManager, EventTypes.MediaSubItemAdded, myOnMediaSubItemAddedInternalEventCallback = OnMediaSubItemAddedInternal);
            AttachEvent(eventManager, EventTypes.MediaSubItemTreeAdded, myOnMediaSubItemTreeAddedInternalEventCallback = OnMediaSubItemTreeAddedInternal);
            eventManager.Dispose();
        }

        private void UnregisterEvents()
        {
            VlcMediaEventManagerInstance eventManager = new VlcMediaEventManagerInstance(VlcNative.libvlc_media_event_manager(MediaInstance));
            //myVlcMediaPlayer.Manager.DetachEvent(eventManager, EventTypes.MediaDurationChanged, myOnMediaDurationChangedInternalEventCallback);
            DetachEvent(eventManager, EventTypes.MediaFreed, myOnMediaFreedInternalEventCallback);
            DetachEvent(eventManager, EventTypes.MediaMetaChanged, myOnMediaMetaChangedInternalEventCallback);
            DetachEvent(eventManager, EventTypes.MediaParsedChanged, myOnMediaParsedChangedInternalEventCallback);
            DetachEvent(eventManager, EventTypes.MediaStateChanged, myOnMediaStateChangedInternalEventCallback);
            DetachEvent(eventManager, EventTypes.MediaSubItemAdded, myOnMediaSubItemAddedInternalEventCallback);
            DetachEvent(eventManager, EventTypes.MediaSubItemTreeAdded, myOnMediaSubItemTreeAddedInternalEventCallback);
            eventManager.Dispose();
        }

        internal int AttachEvent(in VlcEventManagerInstance eventManagerInstance, EventTypes eventType, EventCallback callback)
        {
            if (eventManagerInstance == IntPtr.Zero)
                throw new ArgumentException("Event manager instance is not initialized.");
            if (callback == null)
                throw new ArgumentException("Callback for event is not initialized.");
            return VlcNative.libvlc_event_attach(eventManagerInstance, eventType, callback, IntPtr.Zero);
        }

        internal void DetachEvent(in VlcEventManagerInstance eventManagerInstance, EventTypes eventType, EventCallback callback)
        {
            if (eventManagerInstance == IntPtr.Zero)
                throw new ArgumentException("Event manager is not initialized.");
            if (callback == null)
                return;
            VlcNative.libvlc_event_detach(eventManagerInstance, eventType, callback, IntPtr.Zero);
        }

        private static VlcMediaPlayerInstance CreateMediaPlayerFromMedia(in VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return new VlcMediaPlayerInstance(VlcNative.libvlc_media_player_new_from_media(mediaInstance));
        }

        private static VlcMediaInstance CreateNewMediaFromFileDescriptor(in VlcInstance VlcInstance, in int fileDescriptor)
        {
            return VlcMediaInstance.New(VlcNative.libvlc_media_new_fd(VlcInstance, fileDescriptor));
        }

        private static VlcMediaInstance CreateNewMediaFromLocation(in VlcInstance VlcInstance, in string mrl)
        {
            using (Utf8StringHandle handle = Utf8InteropStringConverter.ToUtf8StringHandle(in mrl))
            {
                return VlcMediaInstance.New(VlcNative.libvlc_media_new_location(VlcInstance, handle));
            }
        }

        private static VlcMediaInstance CreateNewMediaFromPath(in VlcInstance VlcInstance, in string mrl)
        {
            using (Utf8StringHandle handle = Utf8InteropStringConverter.ToUtf8StringHandle(in mrl))
            {
                return VlcMediaInstance.New(VlcNative.libvlc_media_new_path(VlcInstance, handle));
            }
        }

        private static readonly CallbackOpenMediaDelegate CallbackOpenMediaDelegate = CallbackOpenMedia;
        private static readonly CallbackReadMediaDelegate CallbackReadMediaDelegate = CallbackReadMedia;
        private static readonly CallbackSeekMediaDelegate CallbackSeekMediaDelegate = CallbackSeekMedia;
        private static readonly CallbackCloseMediaDelegate CallbackCloseMediaDelegate = CallbackCloseMedia;

        private static readonly ConcurrentDictionary<IntPtr, StreamData> DicStreams = new ConcurrentDictionary<IntPtr, StreamData>();

        private static int streamIndex = 0;

        private static VlcMediaInstance CreateNewMediaFromStream(in VlcInstance VlcInstance, in Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (VlcMediaPlayer.VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException("You need VLC version 3.0 or higher to be able to use CreateNewMediaFromStream");
            }

            IntPtr opaque = AddStream(stream);

            if (opaque == IntPtr.Zero)
                return null;

            VlcMediaInstance result = VlcMediaInstance.New(VlcNative.libvlc_media_new_callbacks(
                VlcInstance,
                CallbackOpenMediaDelegate,
                CallbackReadMediaDelegate,
                stream.CanSeek ? CallbackSeekMediaDelegate : null,
                CallbackCloseMediaDelegate,
                opaque
                ));

            return result;
        }

        private static int CallbackOpenMedia(IntPtr opaque, ref IntPtr pData, out ulong szData)
        {
            pData = opaque;

            try
            {
                StreamData streamData = GetStream(opaque);

                try
                {
                    szData = (ulong)streamData.Stream.Length;
                }
                catch (Exception)
                {
                    // byte length of the bitstream or UINT64_MAX if unknown
                    szData = ulong.MaxValue;
                }

                if (streamData.Stream.CanSeek)
                {
                    streamData.Stream.Seek(0L, SeekOrigin.Begin);
                }

                return 0;
            }
            catch (Exception)
            {
                szData = 0UL;
                return -1;
            }
        }

        private static int CallbackReadMedia(IntPtr opaque, IntPtr ipbuf, uint len)
        {
            try
            {
                StreamData streamData = GetStream(opaque);
                int read;

                lock (streamData)
                {
                    int canRead = Math.Min((int)len, streamData.Buffer.Length);
                    read = streamData.Stream.Read(streamData.Buffer, 0, canRead);
                    Marshal.Copy(streamData.Buffer, 0, ipbuf, read);
                }

                return read;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private static int CallbackSeekMedia(IntPtr opaque, UInt64 offset)
        {
            try
            {
                StreamData streamData = GetStream(opaque);
                streamData.Stream.Seek((long)offset, SeekOrigin.Begin);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private static void CallbackCloseMedia(IntPtr opaque)
        {
            try
            {
                RemoveStream(opaque);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static IntPtr AddStream(in Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IntPtr handle;

            lock (DicStreams)
            {
                streamIndex++;

                handle = new IntPtr(streamIndex);
                DicStreams[handle] = new StreamData()
                {
                    Buffer = new byte[0x100_0000],
                    Handle = handle,
                    Stream = stream
                };
            }

            return handle;
        }

        private static StreamData GetStream(IntPtr handle)
        {
            StreamData result;

            if (!DicStreams.TryGetValue(handle, out result))
            {
                return null;
            }

            return result;
        }

        private static void RemoveStream(IntPtr handle)
        {
            StreamData result;
            DicStreams.TryRemove(handle, out result);
        }
    }
}