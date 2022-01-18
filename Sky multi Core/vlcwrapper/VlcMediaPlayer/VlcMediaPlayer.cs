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
using System.Runtime.InteropServices;
using System.Text;
using Sky_multi_Core.VlcWrapper.Core;
using System.Collections.Generic;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcMediaPlayer : IDisposable
    {
        private readonly VlcInstance myVlcInstance;
        private readonly VlcMediaPlayerInstance myMediaPlayerInstance;
        private VlcMedia myCurrentMedia;
        private HardwareAccelerationType HardwareAcceleration_ = HardwareAccelerationType.d3d11;

        public static string VlcVersion => Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_get_version());

        public VlcMediaPlayer(in DirectoryInfo dynamicLinkLibrariesPath, in string[] args)
        {
            VlcLibraryLoader.LoadLibVlc(in dynamicLinkLibrariesPath);

            lock (args)
            {
                IntPtr[] utf8Args = new IntPtr[args?.Length ?? 0];
                try
                {

                    for (int i = 0; i < utf8Args.Length; i++)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(args[i]);
                        IntPtr buffer = Marshal.AllocHGlobal(bytes.Length + 1);
                        Marshal.Copy(bytes, 0, buffer, bytes.Length);
                        Marshal.WriteByte(buffer, bytes.Length, 0);
                        utf8Args[i] = buffer;
                    }

                    lock (utf8Args)
                    {
                        myVlcInstance = new VlcInstance(VlcNative.libvlc_new(utf8Args.Length, utf8Args));
                        myMediaPlayerInstance = this.CreateMediaPlayer();
                    }
                }
                finally
                {
                    foreach (IntPtr arg in utf8Args)
                    {
                        if (arg != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(arg);
                        }
                    }
                }
            }

            RegisterEvents();
            Chapters = new ChapterManagement(myMediaPlayerInstance);
            SubTitles = new SubTitlesManagement(myMediaPlayerInstance);
            Video = new VideoManagement(myMediaPlayerInstance);
            Audio = new AudioManagement(myMediaPlayerInstance);
            Dialogs = new DialogsManagement(myMediaPlayerInstance, myVlcInstance);
        }

        public void Dispose()
        {
            if (myMediaPlayerInstance == IntPtr.Zero)
            {
                return;
            }

            UnregisterEvents();

            if (IsPlaying())
            {
                Stop();
            }

            myCurrentMedia?.Dispose();
            myMediaPlayerInstance.Dispose();
            myVlcInstance.Dispose();

            VlcLibraryLoader.ReleaseLoader();

            if (this.dialogCallbacksPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.dialogCallbacksPointer);
            }
        }

        internal void Free(in IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return;
            VlcNative.libvlc_free(instance);
        }

        internal VlcMediaPlayerInstance CreateMediaPlayer()
        {
            lock (myVlcInstance)
            {
                return new VlcMediaPlayerInstance(VlcNative.libvlc_media_player_new(myVlcInstance));
            }
        }

        public static Version VlcVersionNumber
        {
            get
            {
                string versionString = VlcMediaPlayer.VlcVersion;
                versionString = versionString.Split('-', ' ')[0];

                return new Version(versionString);
            }
        }

        #region Event

        private void RegisterEvents()
        {
            VlcMediaPlayerEventManagerInstance vlcEventManager = this.GetMediaPlayerEventManager(myMediaPlayerInstance);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerBackward, myOnMediaPlayerBackwardInternalEventCallback = OnMediaPlayerBackwardInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerBuffering, myOnMediaPlayerBufferingInternalEventCallback = OnMediaPlayerBufferingInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEncounteredError, myOnMediaPlayerEncounteredErrorInternalEventCallback = OnMediaPlayerEncounteredErrorInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEndReached, myOnMediaPlayerEndReachedInternalEventCallback = OnMediaPlayerEndReachedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerForward, myOnMediaPlayerForwardInternalEventCallback = OnMediaPlayerForwardInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerLengthChanged, myOnMediaPlayerLengthChangedInternalEventCallback = OnMediaPlayerLengthChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerMediaChanged, myOnMediaPlayerMediaChangedInternalEventCallback = OnMediaPlayerMediaChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerOpening, myOnMediaPlayerOpeningInternalEventCallback = OnMediaPlayerOpeningInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPausableChanged, myOnMediaPlayerPausableChangedInternalEventCallback = OnMediaPlayerPausableChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPaused, myOnMediaPlayerPausedInternalEventCallback = OnMediaPlayerPausedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPlaying, myOnMediaPlayerPlayingInternalEventCallback = OnMediaPlayerPlayingInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPositionChanged, myOnMediaPlayerPositionChangedInternalEventCallback = OnMediaPlayerPositionChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerScrambledChanged, myOnMediaPlayerScrambledChangedInternalEventCallback = OnMediaPlayerScrambledChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerSeekableChanged, myOnMediaPlayerSeekableChangedInternalEventCallback = OnMediaPlayerSeekableChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerSnapshotTaken, myOnMediaPlayerSnapshotTakenInternalEventCallback = OnMediaPlayerSnapshotTakenInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerStopped, myOnMediaPlayerStoppedInternalEventCallback = OnMediaPlayerStoppedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerTimeChanged, myOnMediaPlayerTimeChangedInternalEventCallback = OnMediaPlayerTimeChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerTitleChanged, myOnMediaPlayerTitleChangedInternalEventCallback = OnMediaPlayerTitleChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerVout, myOnMediaPlayerVideoOutChangedInternalEventCallback = OnMediaPlayerVideoOutChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEsAdded, myOnMediaPlayerEsAddedInternalEventCallback = OnMediaPlayerEsAddedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEsDeleted, myOnMediaPlayerEsDeletedInternalEventCallback = OnMediaPlayerEsDeletedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEsSelected, myOnMediaPlayerEsSelectedInternalEventCallback = OnMediaPlayerEsSelectedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerCorked, myOnMediaPlayerCorkedInternalEventCallback = OnMediaPlayerCorkedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerUncorked, myOnMediaPlayerUncorkedInternalEventCallback = OnMediaPlayerUncorkedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerMuted, myOnMediaPlayerMutedInternalEventCallback = OnMediaPlayerMutedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerUnmuted, myOnMediaPlayerUnmutedInternalEventCallback = OnMediaPlayerUnmutedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerAudioVolume, myOnMediaPlayerAudioVolumeInternalEventCallback = OnMediaPlayerAudioVolumeInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerAudioDevice, myOnMediaPlayerAudioDeviceInternalEventCallback = OnMediaPlayerAudioDeviceInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerChapterChanged, myOnMediaPlayerChapterChangedInternalEventCallback = OnMediaPlayerChapterChangedInternal);
            vlcEventManager.Dispose();
        }

        private void UnregisterEvents()
        {
            VlcMediaPlayerEventManagerInstance vlcEventManager = this.GetMediaPlayerEventManager(myMediaPlayerInstance);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerBackward, myOnMediaPlayerBackwardInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerBuffering, myOnMediaPlayerBufferingInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEncounteredError, myOnMediaPlayerEncounteredErrorInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEndReached, myOnMediaPlayerEndReachedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerForward, myOnMediaPlayerForwardInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerLengthChanged, myOnMediaPlayerLengthChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerMediaChanged, myOnMediaPlayerMediaChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerOpening, myOnMediaPlayerOpeningInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPausableChanged, myOnMediaPlayerPausableChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPaused, myOnMediaPlayerPausedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPlaying, myOnMediaPlayerPlayingInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPositionChanged, myOnMediaPlayerPositionChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerScrambledChanged, myOnMediaPlayerScrambledChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerSeekableChanged, myOnMediaPlayerSeekableChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerSnapshotTaken, myOnMediaPlayerSnapshotTakenInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerStopped, myOnMediaPlayerStoppedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerTimeChanged, myOnMediaPlayerTimeChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerTitleChanged, myOnMediaPlayerTitleChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerVout, myOnMediaPlayerVideoOutChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEsAdded, myOnMediaPlayerEsAddedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEsDeleted, myOnMediaPlayerEsDeletedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEsSelected, myOnMediaPlayerEsSelectedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerCorked, myOnMediaPlayerCorkedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerUncorked, myOnMediaPlayerUncorkedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerMuted, myOnMediaPlayerMutedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerUnmuted, myOnMediaPlayerUnmutedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerAudioVolume, myOnMediaPlayerAudioVolumeInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerAudioDevice, myOnMediaPlayerAudioDeviceInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerChapterChanged, myOnMediaPlayerChapterChangedInternalEventCallback);
            vlcEventManager.Dispose();
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

        internal VlcMediaPlayerEventManagerInstance GetMediaPlayerEventManager(in VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return new VlcMediaPlayerEventManagerInstance(VlcNative.libvlc_media_player_event_manager(mediaPlayerInstance));
        }

        #endregion

        #region Dialog

        private DialogCallbacks? dialogCallbacks;

        private IntPtr dialogCallbacksPointer;

        /// <summary>
        /// Register callbacks in order to handle VLC dialogs. LibVLC 3.0.0 and later.
        /// </summary>
        public void SetDialogCallbacks(DialogCallbacks? callbacks, IntPtr data)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(SetDialogCallbacks)}");
            }

            if (this.dialogCallbacks.HasValue)
            {
                Marshal.FreeHGlobal(this.dialogCallbacksPointer);
                this.dialogCallbacksPointer = IntPtr.Zero;
            }

            this.dialogCallbacks = callbacks;
            if (this.dialogCallbacks.HasValue)
            {
                this.dialogCallbacksPointer = Marshal.AllocHGlobal(MarshalHelper.SizeOf<DialogCallbacks>());
                Marshal.StructureToPtr(this.dialogCallbacks.Value, this.dialogCallbacksPointer, false);
            }

            VlcNative.libvlc_dialog_set_callbacks(this.myVlcInstance, this.dialogCallbacksPointer, data);
        }

        /// <summary>
        /// Associate an opaque pointer with the dialog id
        /// </summary>
        public void SetDialogContext(IntPtr dialogId, IntPtr data)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(SetDialogContext)}");
            }

            VlcNative.libvlc_dialog_set_context(dialogId, data);
        }


        /// <summary>
        /// Return the opaque pointer associated with the dialog id
        /// </summary>
        public IntPtr GetDialogContext(IntPtr dialogId)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(GetDialogContext)}");
            }

            return VlcNative.libvlc_dialog_get_context(dialogId);
        }

        /// <summary>
        /// Post a login answer
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        public int PostLogin(IntPtr dialogId, Utf8StringHandle username, Utf8StringHandle password, bool store)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(PostLogin)}");
            }

            return VlcNative.libvlc_dialog_post_login(dialogId, username?.DangerousGetHandle() ?? IntPtr.Zero, password?.DangerousGetHandle() ?? IntPtr.Zero, store);
        }

        /// <summary>
        /// Post a question answer
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        public int PostAction(IntPtr dialogId, int action)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(PostAction)}");
            }

            return VlcNative.libvlc_dialog_post_action(dialogId, action);
        }

        /// <summary>
        /// Dismiss a dialog
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        public int DismissDialog(IntPtr dialogId)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(DismissDialog)}");
            }

            return VlcNative.libvlc_dialog_dismiss(dialogId);
        }

        #endregion

        private void myMediaPlayerInstanceIsLoad()
        {
            if (myMediaPlayerInstance == IntPtr.Zero)
            {
                throw new ArgumentException("Media player instance is not initialized.");
            }
        }

        #region Property
        /// <summary>
        /// WARNING : USE AT YOUR OWN RISK!
        /// Gets the low-level interop manager that calls the methods on the libvlc library.
        /// This is useful if a higher-level API is missing.
        /// </summary>

        public IntPtr VideoHostControlHandle
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_get_hwnd(myMediaPlayerInstance); 
            }
            set 
            {
                myMediaPlayerInstanceIsLoad();
                VlcNative.libvlc_media_player_set_hwnd(myMediaPlayerInstance, value); 
            }
        }

        public HardwareAccelerationType HardwareAcceleration
        {
            get
            {
                return HardwareAcceleration_;
            }
            set
            {
                HardwareAcceleration_ = value;

                if (this.GetMedia() == null)
                {
                    return;
                }

                long time = this.Time;
                if (this.GetMedia().optionsAdded == null)
                {
                    this.Play(this.GetMedia().Mrl, new string[] { ConvertStringHardwareAccelerationType(in HardwareAcceleration_) });
                }
                else
                {
                    List<string> options = new List<string>(this.GetMedia().optionsAdded);
                    for (int index = 0; index < options.Count; index++)
                    {
                        if (options[index] == HardwareAccelerationTypeString.d3d11 || options[index] == HardwareAccelerationTypeString.dxva2 || options[index] == HardwareAccelerationTypeString.none)
                        {
                            options.RemoveAt(index);
                        }
                    }
                    options.Add(ConvertStringHardwareAccelerationType(in HardwareAcceleration_));
                    this.Play(this.GetMedia().Mrl, options.ToArray());
                }
                this.Time = time;
            }
        }

        private string ConvertStringHardwareAccelerationType(HardwareAccelerationType value)
        {
            return ConvertStringHardwareAccelerationType(in value);
        }

        private string ConvertStringHardwareAccelerationType(in HardwareAccelerationType value)
        {
            switch (value)
            {
                case HardwareAccelerationType.d3d11:
                    return HardwareAccelerationTypeString.d3d11;

                case HardwareAccelerationType.dxva2:
                    return HardwareAccelerationTypeString.dxva2;

                case HardwareAccelerationType.none:
                    return HardwareAccelerationTypeString.none;

                default:
                    throw new Exception("This encoding is unknown!");
            }
        }

        public int SetAudioOutput(string outputName)
        {
            myMediaPlayerInstanceIsLoad();
            using (Utf8StringHandle outputInterop = Utf8InteropStringConverter.ToUtf8StringHandle(in outputName))
            {
                return VlcNative.libvlc_audio_output_set(myMediaPlayerInstance, outputInterop);
            }
        }

        public VlcMedia SetMedia(FileInfo file, params string[] options)
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = ConvertStringHardwareAccelerationType(in HardwareAcceleration_);
            return SetMedia(new VlcMedia(myVlcInstance, file, options));
        }

        public VlcMedia SetMedia(Uri uri, params string[] options)
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = ConvertStringHardwareAccelerationType(in HardwareAcceleration_);
            return SetMedia(new VlcMedia(myVlcInstance, uri, options));
        }

        public VlcMedia SetMedia(string mrl, params string[] options)
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = ConvertStringHardwareAccelerationType(in HardwareAcceleration_);
            return SetMedia(new VlcMedia(myVlcInstance, mrl, options));
        }

        public VlcMedia SetMedia(Stream stream, params string[] options)
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = ConvertStringHardwareAccelerationType(in HardwareAcceleration_);
            return SetMedia(new VlcMedia(myVlcInstance, stream, options));
        }

        public void ResetMedia()
        {
            SetMedia((VlcMedia)null);
        }

        private VlcMedia SetMedia(VlcMedia media)
        {
            // If there is a previous media, dispose it.
            myCurrentMedia?.Dispose();

            // Set it to the media player.
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_set_media(myMediaPlayerInstance, media?.MediaInstance?.Pointer ?? IntPtr.Zero);

            // Register Events.
            media?.Initialize();
            myCurrentMedia = media;

            return media;
        }

        public VlcMedia GetMedia()
        {
            return myCurrentMedia;
        }

        public void Play()
        {
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_play(myMediaPlayerInstance);
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(System.IO.FileInfo,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="file">The file to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(FileInfo file, params string[] options)
        {
            this.SetMedia(file, options);
            this.Play();
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(System.Uri,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="uri">The uri to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(Uri uri, params string[] options)
        {
            this.SetMedia(uri, options);
            this.Play();
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(string,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="mrl">The mrl to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(string mrl, params string[] options)
        {
            this.SetMedia(mrl, options);
            this.Play();
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(System.IO.Stream,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="stream">The stream to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(Stream stream, params string[] options)
        {
            this.SetMedia(stream, options);
            this.Play();
        }

        /// <summary>
        /// Toggle pause (no effect if there is no media) 
        /// </summary>
        public void Pause()
        {
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_pause(myMediaPlayerInstance);
        }

        /// <summary>
        /// Pause or resume (no effect if there is no media) 
        /// </summary>
        /// <param name="doPause">If set to <c>true</c>, pauses the media, resumes if <c>false</c></param>
        public void SetPause(bool doPause)
        {
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_set_pause(myMediaPlayerInstance, doPause);
        }

        public void Stop()
        {
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_stop(myMediaPlayerInstance);
        }

        public bool IsPlaying()
        {
            if (myMediaPlayerInstance == IntPtr.Zero)
            {
                return false;
            }

            return VlcNative.libvlc_media_player_is_playing(myMediaPlayerInstance) == 1;
        }

        public bool IsPausable()
        {
            myMediaPlayerInstanceIsLoad();
            return VlcNative.libvlc_media_player_can_pause(myMediaPlayerInstance) == 1;
        }

        public void NextFrame()
        {
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_next_frame(myMediaPlayerInstance);
        }

        public IEnumerable<FilterModuleDescription> GetAudioFilters()
        {
            IntPtr module = VlcNative.libvlc_audio_filter_list_get(myVlcInstance);
            ModuleDescriptionStructure nextModule = MarshalHelper.PtrToStructure<ModuleDescriptionStructure>(in module);
            List<FilterModuleDescription> result = GetSubFilter(nextModule);

            if (module != IntPtr.Zero)
            {
                VlcNative.libvlc_module_description_list_release(module);
            }

            return result;
        }

        private List<FilterModuleDescription> GetSubFilter(ModuleDescriptionStructure module)
        {
            List<FilterModuleDescription> result = new List<FilterModuleDescription>();
            FilterModuleDescription filterModule = FilterModuleDescription.GetFilterModuleDescription(module);

            if (filterModule == null)
            {
                return result;
            }

            result.Add(filterModule);

            if (module.NextModule != IntPtr.Zero)
            {
                ModuleDescriptionStructure nextModule = MarshalHelper.PtrToStructure<ModuleDescriptionStructure>(in module.NextModule);
                List<FilterModuleDescription> data = GetSubFilter(nextModule);

                if (data.Count > 0)
                {
                    result.AddRange(data);
                }
            }

            return result;
        }

        public IEnumerable<FilterModuleDescription> GetVideoFilters()
        {
            IntPtr module = VlcNative.libvlc_video_filter_list_get(myVlcInstance);
            ModuleDescriptionStructure nextModule = MarshalHelper.PtrToStructure<ModuleDescriptionStructure>(in module);
            List<FilterModuleDescription> result = GetSubFilter(nextModule);

            if (module != IntPtr.Zero)
            {
                VlcNative.libvlc_module_description_list_release(module);
            }

            return result;
        }

        public float Position
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_get_position(myMediaPlayerInstance); 
            }
            set 
            {
                myMediaPlayerInstanceIsLoad();
                VlcNative.libvlc_media_player_set_position(myMediaPlayerInstance, value); 
            }
        }

        public bool CouldPlay
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_will_play(myMediaPlayerInstance) == 1; 
            }
        }

        public IChapterManagement Chapters { get; private set; }

        public float Rate
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_get_rate(myMediaPlayerInstance); 
            }
            set 
            {
                myMediaPlayerInstanceIsLoad();
                VlcNative.libvlc_media_player_set_rate(myMediaPlayerInstance, value); 
            }
        }

        public MediaStates State
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_get_state(myMediaPlayerInstance); 
            }
        }

        public float Getfps
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_get_fps(myMediaPlayerInstance); 
            }
        }

        public bool IsSeekable
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_is_seekable(myMediaPlayerInstance) == 1; 
            }
        }

        public void Navigate(NavigateModes navigateMode)
        {
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_navigate(myMediaPlayerInstance, navigateMode);
        }

        public ISubTitlesManagement SubTitles { get; }

        public IVideoManagement Video { get; }

        public IAudioManagement Audio { get; }

        public IDialogsManagement Dialogs { get; }

        public long Length
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_get_length(myMediaPlayerInstance); 
            }
        }

        public long Time
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_media_player_get_time(myMediaPlayerInstance); 
            }
            set 
            {
                myMediaPlayerInstanceIsLoad();
                VlcNative.libvlc_media_player_set_time(myMediaPlayerInstance, value); 
            }
        }

        public int Spu
        {
            get 
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_video_get_spu(myMediaPlayerInstance); 
            }
            set 
            {
                myMediaPlayerInstanceIsLoad();
                VlcNative.libvlc_video_set_spu(myMediaPlayerInstance, value); 
            }
        }

        public bool TakeSnapshot(FileInfo file)
        {
            return TakeSnapshot(file, 0, 0);
        }

        public bool TakeSnapshot(FileInfo file, uint width, uint height)
        {
            return TakeSnapshot(0, file.FullName, width, height);
        }

        public void SetVideoTitleDisplay(Position position, int timeout)
        {
            myMediaPlayerInstanceIsLoad();
            VlcNative.libvlc_media_player_set_video_title_display(myMediaPlayerInstance, position, timeout);
        }

        public float Scale
        {
            get
            {
                myMediaPlayerInstanceIsLoad();
                return VlcNative.libvlc_video_get_scale(myMediaPlayerInstance);
            }
            set
            {
                myMediaPlayerInstanceIsLoad();
                VlcNative.libvlc_video_set_scale(myMediaPlayerInstance, value);
            }
        }

        /// <summary>
        /// Take a snapshot of the current video window.
        /// </summary>
        /// <param name="outputNumber">The number of video output (typically 0 for the first/only one)</param>
        /// <param name="file">The path of a file or a folder to save the screenshot into</param>
        /// <param name="width">the snapshot's width</param>
        /// <param name="height">the snapshot's height</param>
        /// <returns>A boolean indicating whether the screenshot was sucessfully taken</returns>
        /// <remarks>
        /// If i_width AND i_height is 0, original size is used.
        /// If i_width XOR i_height is 0, original aspect-ratio is preserved.
        /// </remarks>
        public bool TakeSnapshot(uint outputNumber, string filePath, uint width, uint height)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (Utf8StringHandle filePathHandle = Utf8InteropStringConverter.ToUtf8StringHandle(in filePath))
            {
                return VlcNative.libvlc_video_take_snapshot(myMediaPlayerInstance, outputNumber, filePathHandle, width, height) == 0;
            }
        }

        #endregion
    }
}
